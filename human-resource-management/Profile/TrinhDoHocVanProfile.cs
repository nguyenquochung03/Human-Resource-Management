using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class TrinhDoHocVanProfile
    {
        public object MapToDto(TrinhDoHocVan tdhv)
        {
            return new
            {
                MaTrinhDo = tdhv.MaTrinhDo,
                TenTrinhDoHocVan = tdhv.TenTrinhDoHocVan,
                BangCap = tdhv.BangCap,
                ChuyenNganh = tdhv.ChuyenNganh,
                NamTotNghiep = tdhv.NameTotNghiep.Year,
                TenTruong = tdhv.TenTruong,
                NhanVien = tdhv.NhanVien != null ? new
                {
                    MaNhanVien = tdhv.NhanVien.MaNhanVien,
                    HoTen = tdhv.NhanVien.HoTen,
                } : null
            };
        }

        public ICollection<object> MapToListDto(ICollection<TrinhDoHocVan> tdhvs)
        {
            return tdhvs.Select(MapToDto).ToList();
        }
    }
}
