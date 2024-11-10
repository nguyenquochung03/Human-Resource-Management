using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Factory;
using HumanResourceManagement.Pattern.Factory.CalculateTax;
using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;
using HumanResourceManagement.Pattern.Singleton;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HumanResourceManagement.Pattern.Observer.ChamCongObserver
{
    public class LuongConcrete : IChamCongObserver
    {
        private readonly HumanResourceManagementDbContext context;
        private readonly IUniqueIdGenerator<Luong> _uniqueIdGenerator;
        private readonly IIncomeCalculationFactory incomeCalculationFactory;

        public LuongConcrete(HumanResourceManagementDbContext context, IUniqueIdGenerator<Luong> uniqueIdGenerator, IIncomeCalculationFactory incomeCalculationFactory)
        {
            this.context = context;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.incomeCalculationFactory = incomeCalculationFactory;
        }

        public async Task<bool> NotifyNewChamCongAdded(string maNhanVien, int thang, int nam)
        {
            NhanVien? nhanVien = await context.NhanViens
            .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(maNhanVien));

            if (nhanVien == null) { return false; }

            Luong? luongNhanVien = await context.Luongs
            .SingleOrDefaultAsync(lg => lg.ThangTinhLuong == thang
            && lg.NamTinhLuong == nam
            && lg.NhanVienId == nhanVien.MaNhanVien);

            DateTime date = new DateTime(nam, thang, 1);

            ThueDTO thueDTO = CreateThueDTOSingleton.GetInstance().createThueDTOFrom(nhanVien, date);
            var calculateTaxFactory = new CalculateTaxFactory(context, thueDTO, incomeCalculationFactory);
            ICalculateTax calculateTax = calculateTaxFactory.CreateCalculateTax();
            ICalculateIncomeService calculateIncomeService = incomeCalculationFactory.CreateCalculateIncomeService();

            if (luongNhanVien == null)
            {
                Luong luong = new Luong()
                {
                    MaLuong = await _uniqueIdGenerator.GetUniqueID("LG", 8, "MaLuong"),
                    LuongCoBan = await calculateTax.BasicSalaryCalculation(),
                    TongTienPhuCap = await calculateIncomeService.CalculateAllowance(nhanVien.MaNhanVien),
                    TongTienThuong = await calculateIncomeService.CalculateBonus(nhanVien.MaNhanVien, thang, nam),
                    TongTien = await calculateTax.TotalSalaryCalculation(),
                    CacKhoanKhauTru = await calculateTax.SalaryDetuctionsCalculation(),
                    ThuNhapRong = await calculateTax.SalaryReceived(),
                    ThangTinhLuong = thang,
                    NamTinhLuong = nam,
                    NhanVien = nhanVien,
                };

                await context.Luongs.AddAsync(luong);
                await context.SaveChangesAsync();
            }
            else
            {
                luongNhanVien.LuongCoBan = await calculateTax.BasicSalaryCalculation();
                luongNhanVien.TongTienPhuCap = await calculateIncomeService.CalculateAllowance(nhanVien.MaNhanVien);
                luongNhanVien.TongTienThuong = await calculateIncomeService.CalculateBonus(nhanVien.MaNhanVien, luongNhanVien.ThangTinhLuong, luongNhanVien.NamTinhLuong);
                luongNhanVien.TongTien = await calculateTax.TotalSalaryCalculation();
                luongNhanVien.CacKhoanKhauTru = await calculateTax.SalaryDetuctionsCalculation();
                luongNhanVien.ThuNhapRong = await calculateTax.SalaryReceived();

                await context.SaveChangesAsync();
            }

            return true;
        }
    }
}
