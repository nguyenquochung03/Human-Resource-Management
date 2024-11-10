using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter.Create_ChamcongObserver;
using Microsoft.EntityFrameworkCore;
using HumanResourceManagement.Exceptions;

namespace HumanResourceManagement.Pattern.Visitor
{
    public class RecalculateVisitor : INhanVienVisitor
    {
        private readonly HumanResourceManagementDbContext context;
        private readonly ICreateChamCongObserverTarget createChamCongObserverTarget;

        public RecalculateVisitor(HumanResourceManagementDbContext context, ICreateChamCongObserverTarget createChamCongObserverTarget)
        {
            this.context = context;
            this.createChamCongObserverTarget = createChamCongObserverTarget;
        }

        public async Task<bool> Visit(INhanVienVisitable visitable)
        {
            DateTime currentTime = DateTime.Now;

            if (visitable is NhanVien nhanVien)
            {
                Luong? luong = await context.Luongs
                        .Include(lg => lg.NhanVien)
                        .SingleOrDefaultAsync(lg =>
                            lg.NhanVienId.Equals(nhanVien.MaNhanVien)
                        && (lg.ThangTinhLuong >= currentTime.Month && lg.NamTinhLuong >= currentTime.Year));

                if (luong != null)
                {
                    await createChamCongObserverTarget.CreateChamCongObserver(nhanVien.MaNhanVien, luong.ThangTinhLuong, luong.NamTinhLuong);
                    return true;
                }
            }
            return false;
        }
    }
}
