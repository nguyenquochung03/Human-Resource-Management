using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Singleton
{
    public class CreateThueDTOSingleton
    {
        private static CreateThueDTOSingleton instance;

        private CreateThueDTOSingleton() { }

        public static CreateThueDTOSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new CreateThueDTOSingleton();
            }

            return instance;
        }

        public ThueDTO createThueDTOFrom(NhanVien nhanVien, DateTime thangNam)
        {
            ThueDTO thueDTO = new ThueDTO()
            {
                ThoiGianTinh = thangNam,
                NhanVien = nhanVien
            };

            return thueDTO;
        }
    }
}
