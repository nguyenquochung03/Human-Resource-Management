using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Template.ObjectCopier
{
    public class PhuCapNhanVienCopier : ObjectCopier<PhuCapNhanVien>
    {
        protected override PhuCapNhanVien CopyObject(PhuCapNhanVien obj)
        {
            if (obj == null)
                return null;

            PhuCapNhanVien copy = new PhuCapNhanVien
            {
                NhanVienId = obj.NhanVienId,
                PhuCapId = obj.PhuCapId,
                
            };

            return copy;
        }
    }
}
