using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Adapter.Update_DotTraLuong;
using HumanResourceManagement.Pattern.Singleton;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HumanResourceManagement.Pattern.Observer.ChamCongObserver
{
    public class DotTraLuongConcrete : IChamCongObserver
    {
        private readonly HumanResourceManagementDbContext context;
        private readonly IUniqueIdGenerator<DotTraLuong> _uniqueIdGenerator;
        private readonly IUpdateDotTraLuongTarget updateDotTraLuongTarget;

        public DotTraLuongConcrete(HumanResourceManagementDbContext context, IUniqueIdGenerator<DotTraLuong> uniqueIdGenerator, IUpdateDotTraLuongTarget updateDotTraLuongTarget)
        {
            this.context = context;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.updateDotTraLuongTarget = updateDotTraLuongTarget;
        }

        public async Task<bool> NotifyNewChamCongAdded(string maNhanVien, int thang, int nam)
        {
            DotTraLuong? checkDotTraLuong = await context.DotTraLuongs
            .SingleOrDefaultAsync(dtl
                => dtl.ThangDotTraLuong.Equals(thang) && dtl.NamDotTraLuong.Equals(nam)
                );

            if (checkDotTraLuong == null)
            {
                List<Luong> luongs = context.Luongs.Where(l => l.ThangTinhLuong == thang &&
                l.NamTinhLuong == nam).ToList();

                DotTraLuong dotTraLuong = new DotTraLuong()
                {
                    MaDotTraLuong = await _uniqueIdGenerator.GetUniqueID("DTL", 8, "MaDotTraLuong"),
                    TenDotTraLuong = $"Tháng {thang} / {nam}",
                    ThangDotTraLuong = thang,
                    TongNhanVienDaTra = 0,
                    TongTienDaTra = 0,
                    NamDotTraLuong = nam,
                    GhiChu = $"Đợt trả lương tháng {thang} / {nam}",
                    NguoiTraLuong = "Nguyễn Quốc Hưng"
                };

                dotTraLuong.TongNhanVienCanPhaiTra = luongs.Count;
                dotTraLuong.TongNhanVienConPhaiTra = luongs.Count;
                dotTraLuong.TongTienCanPhaiTra = luongs.Sum(l => l.ThuNhapRong);
                dotTraLuong.TongTienConPhaiTra = luongs.Sum(l => l.ThuNhapRong);

                await context.DotTraLuongs.AddAsync(dotTraLuong);
                await context.SaveChangesAsync();
            }
            else
            {
                await updateDotTraLuongTarget.UpdateDotTraLuong(checkDotTraLuong);

            }

            return true;
        }
    }
}
