using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class PhuCapProfile
    {
        public object MapToDto(PhuCap phuCap)
        {
            return new
            {
                MaPhuCap = phuCap.MaPhuCap,
                TenPhuCap = phuCap.TenPhuCap,
                SoTienPhuCap = phuCap.SoTienPhuCap,
                TrangThai = phuCap.TrangThai,
                TanSuat = phuCap.TanSuat
            };
        }

        public ICollection<object> MapToListDto(ICollection<PhuCap> phuCaps)
        {
            return phuCaps.Select(MapToDto).ToList();
        }
    }
}
