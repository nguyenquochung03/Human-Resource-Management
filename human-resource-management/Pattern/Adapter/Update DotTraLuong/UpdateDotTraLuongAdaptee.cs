using HumanResourceManagement.Constants;
using HumanResourceManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Pattern.Singleton
{
    public class UpdateDotTraLuongAdaptee
    {
        private readonly HumanResourceManagementDbContext _dbContext;

        public UpdateDotTraLuongAdaptee(HumanResourceManagementDbContext context)
        {
            this._dbContext = context;
        }

        public async Task<bool> UpdateDotTraLuong(DotTraLuong checkDotTraLuong)
        {
            List<Luong> luongs = _dbContext.Luongs.Where(l => l.ThangTinhLuong == checkDotTraLuong.ThangDotTraLuong &&
            l.NamTinhLuong == checkDotTraLuong.NamDotTraLuong).ToList();

            List<TraLuong> traLuongs = _dbContext.TraLuongs.Where(tl => tl.DotTraLuongId.Equals(checkDotTraLuong.MaDotTraLuong)).ToList();

            List<Luong> luongsNotInTraLuongs = luongs.Where(l => !traLuongs.Any(tl => tl.NhanVienId == l.NhanVienId)).ToList();
            List<TraLuong> traLuongsWithUnpaidSalary = traLuongs.Where(tl => tl.TrangThai.Equals(TrangThaiTraLuong.DangNoLuong) && tl.SoTienConPhaiTra > 0).ToList();
            List<TraLuong> traLuongsWithPaidSalary = traLuongs.Where(tl => tl.TrangThai.Equals(TrangThaiTraLuong.DaTraLuong)).ToList();

            int tongNhanVienConPhaiTra = luongsNotInTraLuongs.Count + traLuongsWithUnpaidSalary.Count;
            int tongNhanVienDaTra = traLuongsWithPaidSalary.Count;
            double tongTienConPhaiTra = luongsNotInTraLuongs.Sum(l => l.ThuNhapRong) + traLuongsWithUnpaidSalary.Sum(tl => tl.SoTienConPhaiTra);
            double tongTienDaTra = traLuongs.Sum(tl => tl.SoTienDaTra);

            checkDotTraLuong.TongNhanVienCanPhaiTra = luongs.Count;
            checkDotTraLuong.TongNhanVienDaTra = tongNhanVienDaTra;
            checkDotTraLuong.TongNhanVienConPhaiTra = tongNhanVienConPhaiTra;
            checkDotTraLuong.TongTienCanPhaiTra = luongs.Sum(lg => lg.ThuNhapRong);
            checkDotTraLuong.TongTienConPhaiTra = tongTienConPhaiTra;
            checkDotTraLuong.TongTienDaTra = tongTienDaTra;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
