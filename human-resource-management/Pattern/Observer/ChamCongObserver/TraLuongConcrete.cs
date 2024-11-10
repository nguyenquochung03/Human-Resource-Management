using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Observer.ChamCongObserver
{
    public class TraLuongConcrete : IChamCongObserver
    {
        private readonly HumanResourceManagementDbContext context;
        private readonly IUniqueIdGenerator<TraLuong> _uniqueIdGenerator;

        public TraLuongConcrete(HumanResourceManagementDbContext context, IUniqueIdGenerator<TraLuong> uniqueIdGenerator)
        {
            this.context = context;
            _uniqueIdGenerator = uniqueIdGenerator;
        }

        public async Task<bool> NotifyNewChamCongAdded(string maNhanVien, int thang, int nam)
        {
            NhanVien? nhanVien = await context.NhanViens
            .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(maNhanVien));

            DotTraLuong? dotTraLuong = await context.DotTraLuongs
           .SingleOrDefaultAsync(dtl => dtl.ThangDotTraLuong == thang
            && dtl.NamDotTraLuong == nam);

            if (dotTraLuong == null || nhanVien == null) return false;

            Luong? luongNhanVien = await context.Luongs
                .SingleOrDefaultAsync(lg => lg.ThangTinhLuong == dotTraLuong.ThangDotTraLuong
                && lg.NamTinhLuong == dotTraLuong.NamDotTraLuong
                && lg.NhanVienId == nhanVien.MaNhanVien);

            if (luongNhanVien == null) return false;

            TraLuong? checkTraLuong = await context.TraLuongs
                .SingleOrDefaultAsync(tl => tl.DotTraLuongId.Equals(dotTraLuong.MaDotTraLuong) && tl.NhanVienId.Equals(nhanVien.MaNhanVien));

            if (checkTraLuong == null)
            {
                TraLuong traLuong = new TraLuong()
                {
                    MaTraLuong = await _uniqueIdGenerator.GetUniqueID("TL", 8, "MaTraLuong"),
                    GhiChu = "Trả lương cho nhân viên",
                    NgayTraLuong = DateTime.Now,
                    NhanVien = nhanVien,
                    DotTraLuong = dotTraLuong,
                    TrangThai = TrangThaiTraLuong.DangNoLuong,
                    SoTienCanPhaiTra = luongNhanVien.ThuNhapRong,
                    SoTienDaTra = 0,
                    SoTienConPhaiTra = luongNhanVien.ThuNhapRong
                };

                await context.TraLuongs.AddAsync(traLuong);
            }
            else
            {
                checkTraLuong.SoTienCanPhaiTra = luongNhanVien.ThuNhapRong;
                checkTraLuong.SoTienConPhaiTra = luongNhanVien.ThuNhapRong - checkTraLuong.SoTienDaTra;
                if (Math.Ceiling(checkTraLuong.SoTienConPhaiTra) == 0 || Math.Floor(checkTraLuong.SoTienConPhaiTra) == 0)
                {
                    checkTraLuong.TrangThai = TrangThaiTraLuong.DaTraLuong;
                }
                else
                {
                    checkTraLuong.TrangThai = TrangThaiTraLuong.DangNoLuong;
                }
            }

            await context.SaveChangesAsync();

            return true;
        }
    }
}
