using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Template.ObjectCopier
{
    public class ThuongNhanVienCopier : ObjectCopier<ThuongNhanVien>
    {
        protected override ThuongNhanVien CopyObject(ThuongNhanVien obj)
        {
            if (obj == null)
                return null;

            ThuongNhanVien copy = new ThuongNhanVien
            {
                NhanVienId = obj.NhanVienId,
                ThuongId = obj.ThuongId,
                LanThu = obj.LanThu
            };

            return copy;
        }
    }
}
