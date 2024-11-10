using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Factory;
using HumanResourceManagement.Pattern.Factory.CalculateTax;
using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;
using HumanResourceManagement.Pattern.Singleton;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Observer.ChamCongObserver
{
    public class ThueConcrete : IChamCongObserver
    {
        private readonly HumanResourceManagementDbContext context;
        private readonly IUniqueIdGenerator<Thue> _uniqueIdGenerator;
        private readonly IIncomeCalculationFactory incomeCalculationFactory;

        public ThueConcrete(HumanResourceManagementDbContext context, IUniqueIdGenerator<Thue> uniqueIdGenerator, IIncomeCalculationFactory incomeCalculationFactory)
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

            DateTime date = new DateTime(nam, thang, 1);

            ThueDTO thueDTO = CreateThueDTOSingleton.GetInstance().createThueDTOFrom(nhanVien, date);
            var calculateTaxFactory = new CalculateTaxFactory(context, thueDTO, incomeCalculationFactory);
            ICalculateTax calculateTax = calculateTaxFactory.CreateCalculateTax();

            Thue? thueNhanVien = await context.Thues
                .SingleOrDefaultAsync(th => th.ThangTinhThue == thang
                && th.NamTinhThue == nam
                && th.NhanVienId == nhanVien.MaNhanVien);

            if (thueNhanVien == null)
            {
                double luongCoBan = await calculateTax.BasicSalaryCalculation();
                Thue thue = new Thue()
                {
                    MaSoThue = await _uniqueIdGenerator.GetUniqueID("TH", 8, "MaSoThue"),
                    BaoHiemXaHoi = luongCoBan * 0.08,
                    BaoHienYTe = luongCoBan * 0.015,
                    BaoHiemThatNghiep = luongCoBan * 0.01,
                    ThangTinhThue = thueDTO.ThoiGianTinh.Month,
                    NamTinhThue = thueDTO.ThoiGianTinh.Year,
                    NhanVien = thueDTO.NhanVien
                };
                thue.MucThuNhapChiuThue = await calculateTax.TaxableIncomeCalculation();
                thue.ThueSuat = await calculateTax.TaxRateCalculation();
                thue.ThueThuNhapCaNhanThucTe = await calculateTax.PersionalIncomeTaxCalculation();

                await context.Thues.AddAsync(thue);
                await context.SaveChangesAsync();
            }
            else
            {
                double luongCoBan = await calculateTax.BasicSalaryCalculation();

                thueNhanVien.BaoHiemXaHoi = luongCoBan * 0.08;
                thueNhanVien.BaoHienYTe = luongCoBan * 0.015;
                thueNhanVien.BaoHiemThatNghiep = luongCoBan * 0.01;
                thueNhanVien.NhanVien = thueDTO.NhanVien;
                thueNhanVien.MucThuNhapChiuThue = await calculateTax.TaxableIncomeCalculation();
                thueNhanVien.ThueSuat = await calculateTax.TaxRateCalculation();
                thueNhanVien.ThueThuNhapCaNhanThucTe = await calculateTax.PersionalIncomeTaxCalculation();

                await context.SaveChangesAsync();
            }

            return true;
        }
    }
}
