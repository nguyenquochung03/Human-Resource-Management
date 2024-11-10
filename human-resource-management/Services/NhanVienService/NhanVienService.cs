using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Factory.SearchEmployeeStrategyFactory;
using HumanResourceManagement.Pattern.Builder.NhanVienBuilder;
using HumanResourceManagement.Pattern.SearchEmployeeStrategy;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.NhanVienService
{
    public class NhanVienService : INhanVienService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly SearchEmployeeStrategyFactory _searchEmployeeStrategyFactory;
        private readonly IUniqueIdGenerator<NhanVien> _uniqueIdGenerator;
        private readonly NhanVienProfile nhanVienProfile;

        public NhanVienService(HumanResourceManagementDbContext dbContext, SearchEmployeeStrategyFactory searchEmployeeStrategyFactory, IUniqueIdGenerator<NhanVien> uniqueIdGenerator, NhanVienProfile nhanVienProfile)
        {
            _dbContext = dbContext;
            _searchEmployeeStrategyFactory = searchEmployeeStrategyFactory;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.nhanVienProfile = nhanVienProfile;
        }

        public async Task<ICollection<object>> GetNhanViens()
        {

            ISearchEmployeeStrategy searchStrategy = _searchEmployeeStrategyFactory.CreateStrategy("");
            var nhanViens = await searchStrategy.SearchAsync("");

            return nhanVienProfile.MapToListDto(nhanViens);
        }
        public async Task<object> GetListByPage(int page = 1, int pageSize = 10, string queryType = "", string queryValue = "")
        {

            ISearchEmployeeStrategy searchStrategy = _searchEmployeeStrategyFactory.CreateStrategy(queryType);
            var nhanViens = await searchStrategy.SearchAsync(queryValue);

            // Lấy tổng số lượng mục
            var totalCount = nhanViens.Count();

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Sử dụng Skip() và Take() để phân trang
            var items = nhanViens
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = nhanVienProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<object> GetNhanVienById(string id)
        {
            var searchStrategy = _searchEmployeeStrategyFactory.CreateStrategy("id");
            var nhanViens = await searchStrategy.SearchAsync(id);

            return nhanVienProfile.MapToDto(nhanViens[0]);
        }

        public async Task<ICollection<object>> GetNhanVienByName(string name)
        {
            var searchStrategy = _searchEmployeeStrategyFactory.CreateStrategy("name");
            var nhanViens = await searchStrategy.SearchAsync(name);

            return nhanVienProfile.MapToListDto(nhanViens);
        }

        public async Task<object> GetNhanVienByPhone(string phone)
        {
            var searchStrategy = _searchEmployeeStrategyFactory.CreateStrategy("phone");
            var nhanViens = await searchStrategy.SearchAsync(phone);

            return nhanVienProfile.MapToListDto(nhanViens);
        }

        public async Task<object> ThemNhanVien(NhanVienDTO nhanVienDto)
        {
            string maNhanVien = await _uniqueIdGenerator.GetUniqueID("NV", 8, "MaNhanVien");
            NhanVien nhanVien = new NhanVienBuilder()
                .MaNhanVien(maNhanVien)
                .HoTen(nhanVienDto.HoTen)
                .NgaySinh(nhanVienDto.NgaySinh)
                .GioiTinh(nhanVienDto.GioiTinh)
                .SoNguoiPhuThuoc(nhanVienDto.SoNguoiPhuThuoc)
                .TrangThaiHonNhan(nhanVienDto.TrangThaiHonNhan)
                .Cccd(nhanVienDto.Cccd)
                .TonGiao(nhanVienDto.TonGiao)
                .DiaChi(nhanVienDto.DiaChi)
                .SoDienThoai(nhanVienDto.SoDienThoai)
                .Email(nhanVienDto.Email)
                .NgayVaoLam(nhanVienDto.NgayVaoLam)
                .MucLuong(nhanVienDto.MucLuong)
                .TrangThai(nhanVienDto.TrangThai)
                .PhongBanId(nhanVienDto.PhongBanId)
                .ChucVuId(nhanVienDto.ChucVuId)
                .Build();

            await _dbContext.NhanViens.AddAsync(nhanVien);
            await _dbContext.SaveChangesAsync();

            return nhanVienProfile.MapToDto(nhanVien);
        }

        public async Task<bool> CapNhatNhanVien(string id, NhanVienDTO nhanVienDto)
        {
            var existingNhanVien = await _dbContext.NhanViens
                .Include(nv => nv.ChucVu)
                .Include(nv => nv.PhongBan)
                .FirstOrDefaultAsync(nv => nv.MaNhanVien == id);

            if (existingNhanVien == null)
            {
                throw new NotFoundException("Không tìm thấy nhân viên");
            }

            existingNhanVien.HoTen = nhanVienDto.HoTen;
            existingNhanVien.NgaySinh = nhanVienDto.NgaySinh;
            existingNhanVien.GioiTinh = nhanVienDto.GioiTinh;
            existingNhanVien.SoNguoiPhuThuoc = nhanVienDto.SoNguoiPhuThuoc;
            existingNhanVien.TrangThaiHonNhan = nhanVienDto.TrangThaiHonNhan;
            existingNhanVien.Cccd = nhanVienDto.Cccd;
            existingNhanVien.TonGiao = nhanVienDto.TonGiao;
            existingNhanVien.DiaChi = nhanVienDto.DiaChi;
            existingNhanVien.SoDienThoai = nhanVienDto.SoDienThoai;
            existingNhanVien.Email = nhanVienDto.Email;
            existingNhanVien.NgayVaoLam = nhanVienDto.NgayVaoLam;
            existingNhanVien.MucLuong = nhanVienDto.MucLuong;
            existingNhanVien.TrangThai = nhanVienDto.TrangThai;

            if (!string.IsNullOrEmpty(nhanVienDto.PhongBanId))
            {
                existingNhanVien.PhongBanId = nhanVienDto.PhongBanId;
            }
            else
            {
                existingNhanVien.PhongBanId = null;
            }

            if (!string.IsNullOrEmpty(nhanVienDto.ChucVuId))
            {
                existingNhanVien.ChucVuId = nhanVienDto.ChucVuId;
            }
            else
            {
                existingNhanVien.ChucVuId = null;
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> XoaNhanVien(string id)
        {
            var nhanVien = await _dbContext.NhanViens
                .FindAsync(id);

            if (nhanVien == null)
            {
                throw new NotFoundException("Không tìm thấy nhân viên");
            }

            _dbContext.NhanViens.Remove(nhanVien);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
