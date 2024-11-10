using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HumanResourceManagement.Services.HieuSuatService
{
    public class HieuSuatService : IHieuSuatService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<HieuSuat> _uniqueIdGenerator;
        private readonly HieuSuatProfile hieuSuatProfile;

        public HieuSuatService(HumanResourceManagementDbContext context, IUniqueIdGenerator<HieuSuat> uniqueIdGenerator, HieuSuatProfile hieuSuatProfile)
        {
            _dbContext = context;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.hieuSuatProfile = hieuSuatProfile;
        }

        public async Task<ICollection<object>> GetHieuSuats()
        {
            ICollection<HieuSuat> hieuSuats = await _dbContext.HieuSuats
               .Include(h => h.NhanVien)
               .ToListAsync();

            return hieuSuatProfile.MapToListDto(hieuSuats);
        }
        public dynamic GetListByPage(int page = 1, int pageSize = 10)
        {
            // Lấy tổng số lượng mục
            var totalCount = _dbContext.HieuSuats.Count();

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            //if (page < 1 || page > totalPages)
            //{
            //    return new { msg = "Invalid page number" };
            //}

            // Sử dụng Skip() và Take() để phân trang
            var items = _dbContext.HieuSuats
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .Include(h => h.NhanVien)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = hieuSuatProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }

        public async Task<ICollection<object>> GetHieuSuatByMaNhanVien(string maNhanVien)
        {
            ICollection<HieuSuat> hieuSuats = await _dbContext.HieuSuats
              .Include(h => h.NhanVien).Where(hs => hs.NhanVienId.Equals(maNhanVien))
              .ToListAsync();

            return hieuSuatProfile.MapToListDto(hieuSuats);
        }

        public async Task<object> ThemHieuSuat(HieuSuatDTO hieuSuatDTO)
        {
            NhanVien? nhanVien = await _dbContext.NhanViens
               .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(hieuSuatDTO.NhanVienId))
                   ?? throw new NotFoundException("Không tìm thấy nhân viên");

            HieuSuat hieuSuat = new HieuSuat()
            {
                MaHieuSuat = await _uniqueIdGenerator.GetUniqueID("HS", 8, "MaHieuSuat"),
                DanhGiaHieuSuat = hieuSuatDTO.DanhGiaHieuSuat,
                KeHoachPhatTrien = hieuSuatDTO.KeHoachPhatTrien,
                KyDanhGia = hieuSuatDTO.KyDanhGia,
                MucTieuHieuSuat = hieuSuatDTO.MucTieuHieuSuat,
                PhanHoi = hieuSuatDTO.PhanHoi,
                NhanVien = nhanVien
            };

            if (hieuSuatDTO.NguoiDanhGia.IsNullOrEmpty())
            {
                hieuSuat.NguoiDanhGia = " ";
            }
            else
            {
                hieuSuat.NguoiDanhGia = hieuSuatDTO.NguoiDanhGia;
            }

            await _dbContext.HieuSuats.AddAsync(hieuSuat);
            await _dbContext.SaveChangesAsync();

            return hieuSuatProfile.MapToDto(hieuSuat);
        }

        public async Task<bool> CapNhatHieuSuat(string id, HieuSuatDTO hieuSuatDTO)
        {
            HieuSuat? hieuSuat = await _dbContext.HieuSuats
                .Include(h => h.NhanVien)
                .SingleOrDefaultAsync(dt => dt.MaHieuSuat.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy hiệu suất");

            NhanVien? nhanVien = await _dbContext.NhanViens
                .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(hieuSuatDTO.NhanVienId))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            hieuSuat.DanhGiaHieuSuat = hieuSuatDTO.DanhGiaHieuSuat;
            hieuSuat.KeHoachPhatTrien = hieuSuatDTO.KeHoachPhatTrien;
            hieuSuat.KyDanhGia = hieuSuatDTO.KyDanhGia;
            hieuSuat.MucTieuHieuSuat = hieuSuatDTO.MucTieuHieuSuat;
            hieuSuat.PhanHoi = hieuSuatDTO.PhanHoi;
            hieuSuat.NhanVien = nhanVien;

            if (hieuSuatDTO.NguoiDanhGia.IsNullOrEmpty())
            {
                hieuSuat.NguoiDanhGia = " ";
            }
            else
            {
                hieuSuat.NguoiDanhGia = hieuSuatDTO.NguoiDanhGia;
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> XoaHieuSuat(string id)
        {
            HieuSuat? hieuSuat = await _dbContext.HieuSuats
                .SingleOrDefaultAsync(dt => dt.MaHieuSuat.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy hiểu suất");

            _dbContext.HieuSuats.Remove(hieuSuat);
            await _dbContext.SaveChangesAsync();
            return true;
        }


    }
}
