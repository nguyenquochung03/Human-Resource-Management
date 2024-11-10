using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter.Create_ChamcongObserver;
using Microsoft.EntityFrameworkCore;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Constants;

namespace HumanResourceManagement.Pattern.Visitor
{
    public class RecalculateBasedOnAllowanceAndBonusAdaptee
    {
        private readonly HumanResourceManagementDbContext context;
        private readonly INhanVienVisitor nhanVienVisitor;

        public RecalculateBasedOnAllowanceAndBonusAdaptee(HumanResourceManagementDbContext context, INhanVienVisitor nhanVienVisitor) 
        { 
            this.context = context;
            this.nhanVienVisitor = nhanVienVisitor;
        }

        public async Task<bool> RecalculateBasedOnAllowanceAndBonus(string id, string loaiTinhLaiSoLieu)
        {
            List<NhanVien> nhanVienCanTinhLai = new List<NhanVien>();

            if (loaiTinhLaiSoLieu.Equals(LoaiTinhLaiSoLieu.Thuong))
            {
                nhanVienCanTinhLai = await context.ThuongNhanViens
                    .Where(th => th.ThuongId == id)
                    .Select(th => th.NhanVien)
                    .ToListAsync();
            }
            else if (loaiTinhLaiSoLieu.Equals(LoaiTinhLaiSoLieu.PhuCap))
            {
                nhanVienCanTinhLai = await context.PhuCapNhanViens
                    .Where(th => th.PhuCapId == id)
                    .Select(th => th.NhanVien)
                    .ToListAsync();
            }

            foreach (var nhanVien in nhanVienCanTinhLai)
            {
                await nhanVien.Accept(nhanVienVisitor);
            }
            return false;
        }
    }
}
