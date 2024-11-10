using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Template.ObjectCopier
{
    public class LichLamNhanVienCopier : ObjectCopier<LichLamNhanVien>
    {
        protected override LichLamNhanVien CopyObject(LichLamNhanVien obj)
        {
            if (obj == null)
                return null;

            LichLamNhanVien copy = new LichLamNhanVien
            {
                NhanVienId = obj.NhanVienId,
                LichLamId = obj.LichLamId
            };

            return copy;
        }
    }
}