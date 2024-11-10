using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Adapter.Create_ChamcongObserver;
using HumanResourceManagement.Pattern.Adapter.Recaculate_based_on_allowance_and_bonus;
using HumanResourceManagement.Pattern.Adapter.Update_DotTraLuong;
using HumanResourceManagement.Pattern.Factory.CalculateTax;
using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;
using HumanResourceManagement.Pattern.Factory;
using HumanResourceManagement.Pattern.Observer.ChamCongObserver;
using HumanResourceManagement.Pattern.Singleton;
using HumanResourceManagement.Pattern.Template.ObjectCopier;
using HumanResourceManagement.Pattern.Visitor;
using HumanResourceManagement.Profile;
using HumanResourceManagement.Services.ThuongService;
using Microsoft.EntityFrameworkCore;
using HumanResourceManagement.Pattern.Decorator.TinhLuongNhanVien;

namespace HumanResourceManagement.Services.PhuCapService.PhuCapService
{
    public class PhuCapService : IPhuCapService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<PhuCap> _uniqueIdGenerator;
        private readonly PhuCapProfile phuCapProfile;

        public PhuCapService(HumanResourceManagementDbContext context, IUniqueIdGenerator<PhuCap> uniqueIdGenerator, 
            PhuCapProfile phuCapProfile)
        {
            _dbContext = context;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.phuCapProfile = phuCapProfile;
        }

        public async Task<ICollection<object>> GetPhuCaps()
        {
            ICollection<PhuCap> phuCaps = await _dbContext.PhuCaps.ToListAsync();
            return phuCapProfile.MapToListDto(phuCaps);
        }
        public dynamic GetListByPage(int page = 1, int pageSize = 10)
        {
            // Lấy tổng số lượng mục
            var totalCount = _dbContext.PhuCaps.Count();

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Sử dụng Skip() và Take() để phân trang
            var items = _dbContext.PhuCaps
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = items
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<ICollection<PhuCapNhanVien>> GetPhuCapNhanViens()
        {
            List<NhanVien> nhanViens = await _dbContext.NhanViens
                .Include(nv => nv.PhuCapNhanViens)
                .Where(nv => nv.PhuCapNhanViens.Any())
                .ToListAsync();

            List<PhuCapNhanVien> phuCapNhanViens = nhanViens
                .SelectMany(nv => nv.PhuCapNhanViens)
                .ToList();

            PhuCapNhanVienCopier copier = new PhuCapNhanVienCopier();
            return copier.CopyObjects(phuCapNhanViens);
        }

        public async Task<object> GetPhuCapById(string id)
        {
            PhuCap? existedPhuCap = await _dbContext.PhuCaps
                .SingleOrDefaultAsync(pc => pc.MaPhuCap.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy phụ cấp");

            return existedPhuCap;
        }

        public async Task<ICollection<PhuCap>> GetPhuCapNhanVienByEmployeeId(string employeeId)
        {
            NhanVien? nv = await _dbContext.NhanViens
                .SingleOrDefaultAsync(n => n.MaNhanVien.Equals(employeeId))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            var phuCapNhanViens = await _dbContext.PhuCapNhanViens
                                        .Include(pcnv => pcnv.PhuCap)
                                        .Where(pcnv => pcnv.NhanVienId == employeeId)
                                        .Select(pcnv => pcnv.PhuCap)
                                        .ToListAsync();

            return phuCapNhanViens;
        }

        public async Task<object> ThemPhuCap(PhuCapDTO phuCap)
        {

            PhuCap newPhuCap = new PhuCap()
            {
                MaPhuCap = await _uniqueIdGenerator.GetUniqueID("PC", 8, "MaPhuCap"),
                TenPhuCap = phuCap.TenPhuCap,
                SoTienPhuCap = phuCap.SoTienPhuCap,
                TanSuat = phuCap.TanSuat,
                TrangThai = phuCap.TrangThai
            };

            await _dbContext.PhuCaps.AddAsync(newPhuCap);
            await _dbContext.SaveChangesAsync();

            return newPhuCap;
        }

        public async Task<bool> SuaPhuCap(string id, PhuCapDTO phuCap)
        {
            PhuCap? existedPhuCap = await _dbContext.PhuCaps
                .SingleOrDefaultAsync(pc => pc.MaPhuCap.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy phụ cấp");
            existedPhuCap.TenPhuCap = phuCap.TenPhuCap;
            existedPhuCap.SoTienPhuCap = phuCap.SoTienPhuCap;
            existedPhuCap.TanSuat = phuCap.TanSuat;
            existedPhuCap.TrangThai = phuCap.TrangThai;

            int rowCount = await _dbContext.SaveChangesAsync();
            if (rowCount == 0)
                throw new ConflictException("Cập nhật thông tin phụ cấp thất bại");

            return true;
        }

        public async Task<bool> XoaPhuCap(string id)
        {
            PhuCap? existedPhuCap = await _dbContext.PhuCaps
               .SingleOrDefaultAsync(pc => pc.MaPhuCap.Equals(id))
                   ?? throw new NotFoundException("Không tìm thấy phụ cấp");

            if(existedPhuCap.PhuCapNhanViens.Count() > 0)
                throw new ConflictException("Không thể xóa phụ cấp");

            _dbContext.PhuCaps.Remove(existedPhuCap);
            int rowCount = await _dbContext.SaveChangesAsync();
            if (rowCount == 0)
                throw new ConflictException("Xóa thông tin phụ cấp thất bại");

            return true;
        }

        public async Task<object> AddPhuCapForNvs(CDPhuCapForNhanVienDTO dto)
        {
            PhuCap? phuCap = await _dbContext.PhuCaps
                .Include(pc => pc.PhuCapNhanViens)
                .SingleOrDefaultAsync(pc => pc.MaPhuCap.Equals(dto.PhuCapId))
                    ?? throw new NotFoundException("Không tìm thấy phụ cấp");

            List<string> nhanVienIds = dto.NhanVienIds;

            if (phuCap.PhuCapNhanViens == null)
                phuCap.PhuCapNhanViens = new List<PhuCapNhanVien>();

            for (int i = 0; i < nhanVienIds.Count; i++)
            {
                bool isExisted = phuCap.PhuCapNhanViens
                    .Any(pc => pc.Equals(nhanVienIds[i]));

                if (isExisted)
                    continue;

                PhuCapNhanVien pcnv = new PhuCapNhanVien()
                {
                    NhanVienId = nhanVienIds[i],
                    PhuCapId = phuCap.MaPhuCap,
                };

                phuCap.PhuCapNhanViens.Add(pcnv);
            }

            await _dbContext.SaveChangesAsync();

            return phuCap;
        }

        public async Task<bool> XoaPhuCapForNvs(CDPhuCapForNhanVienDTO dto)
        {
            PhuCap phuCap = await _dbContext.PhuCaps
               .Include(pc => pc.PhuCapNhanViens)
               .SingleOrDefaultAsync(pc => pc.MaPhuCap.Equals(dto.PhuCapId))
               ?? throw new NotFoundException("Không tìm thấy phụ cấp");

            List<string> nhanVienIds = dto.NhanVienIds;

            if (phuCap.PhuCapNhanViens != null)
            {
                foreach (string nhanVienId in nhanVienIds)
                {
                    PhuCapNhanVien? phuCapNhanVien = phuCap.PhuCapNhanViens
                        .FirstOrDefault(pcnv => pcnv.NhanVienId.Equals(nhanVienId));

                    if (phuCapNhanVien != null)
                    {
                        phuCap.PhuCapNhanViens.Remove(phuCapNhanVien);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }

            return true;
        }
    }
}
