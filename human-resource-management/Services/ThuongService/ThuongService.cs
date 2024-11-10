using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Adapter.Recaculate_based_on_allowance_and_bonus;
using HumanResourceManagement.Pattern.Template.ObjectCopier;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.ThuongService
{
    public class ThuongService : IThuongService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<Thuong> _uniqueIdGenerator;
        private readonly ThuongProfile _thuongProfile;
        private readonly IRecalculateBasedOnAllowanceAndBonusTarget recalculateBasedOnAllowanceAndBonusTarget;

        public ThuongService(HumanResourceManagementDbContext context, IUniqueIdGenerator<Thuong> uniqueIdGenerator, ThuongProfile thuongProfile, IRecalculateBasedOnAllowanceAndBonusTarget recalculateBasedOnAllowanceAndBonusTarget)
        {
            _dbContext = context;
            _uniqueIdGenerator = uniqueIdGenerator;
            _thuongProfile = thuongProfile;
            this.recalculateBasedOnAllowanceAndBonusTarget = recalculateBasedOnAllowanceAndBonusTarget;
        }

        public async Task<ICollection<object>> GetThuongs()
        {
            ICollection<Thuong> thuongs = await _dbContext.Thuongs.ToListAsync();
            return _thuongProfile.MapToListDto(thuongs);
        }
        public dynamic GetListByPage(int page = 1, int pageSize = 10)
        {
            // Lấy tổng số lượng mục
            var totalCount = _dbContext.Thuongs.Count();

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

      
            // Sử dụng Skip() và Take() để phân trang
            var items = _dbContext.Thuongs
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = _thuongProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<ICollection<ThuongNhanVien>> GetThuongNhanVien()
        {
            List<ThuongNhanVien> thuongNhanViens = await _dbContext.ThuongNhanViens
              .Include(llnv => llnv.NhanVien)
              .Include(llnv => llnv.Thuong)
              .ToListAsync();

            ThuongNhanVienCopier copier = new ThuongNhanVienCopier();

            return copier.CopyObjects(thuongNhanViens);
        }

        public async Task<object> GetThuongById(string id)
        {
            Thuong? thuong = await _dbContext.Thuongs
                .SingleOrDefaultAsync(t => t.MaKhoanThuong.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy khoản thưởng");

            return _thuongProfile.MapToDto(thuong);
        }

        public async Task<ICollection<object>> GetThuongByMaNhanVien(string id)
        {
            NhanVien? nv = await _dbContext.NhanViens
                .SingleOrDefaultAsync(n => n.MaNhanVien.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            ICollection<Thuong> thuongs = await _dbContext.Thuongs
                .Include(l => l.ThuongNhanViens)
                .ThenInclude(llnv => llnv.NhanVien)
                .Where(l => l.ThuongNhanViens.Any(llnv => llnv.NhanVien.MaNhanVien.Equals(id)))
                .ToListAsync();

            return _thuongProfile.MapToListDto(thuongs);
        }

        public async Task<object> ThemThuong(ThuongDTO thuongDTO)
        {
            Thuong thuong = new Thuong()
            {
                MaKhoanThuong = await _uniqueIdGenerator.GetUniqueID("KT", 8, "MaKhoanThuong"),
                LyDoKhenThuong = thuongDTO.LyDoKhenThuong,
                NgayKhenThuong = thuongDTO.NgayKhenThuong,
                SoTienThuong = thuongDTO.SoTienThuong,
            };

            await _dbContext.Thuongs.AddAsync(thuong);
            await _dbContext.SaveChangesAsync();

            return thuongDTO;
        }

        public async Task<bool> CapNhatThongTinThuong(string id, ThuongDTO thuongDTO)
        {
            Thuong? thuong = await _dbContext.Thuongs
                .SingleOrDefaultAsync(t => t.MaKhoanThuong.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy khoản thưởng");

            thuong.LyDoKhenThuong = thuongDTO.LyDoKhenThuong;
            thuong.NgayKhenThuong = thuongDTO.NgayKhenThuong;
            thuong.SoTienThuong = thuongDTO.SoTienThuong;

            await _dbContext.SaveChangesAsync();
            await recalculateBasedOnAllowanceAndBonusTarget.RecalculateBasedOnAllowanceAndBonus(id, LoaiTinhLaiSoLieu.Thuong);
            return true;
        }

        public async Task<bool> XoaThuong(string id)
        {
            Thuong? thuong = await _dbContext.Thuongs
                .Include(t => t.ThuongNhanViens)
                .SingleOrDefaultAsync(t => t.MaKhoanThuong.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy khoản thưởng");

            if (thuong.ThuongNhanViens.Count() > 0)
                throw new ConflictException("Không thể xóa khoản thưởng");

            _dbContext.Thuongs.Remove(thuong);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<object> ThemThuongNhanVien(CDKhoanThuongNV cdKhoanThuongNV)
        {
            Thuong khoanThuong = await _dbContext.Thuongs
                .Include(t => t.ThuongNhanViens)
                .SingleOrDefaultAsync(t => t.MaKhoanThuong.Equals(cdKhoanThuongNV.ThuongId))
                ?? throw new NotFoundException("Không tìm thấy khoản thưởng");

            if (khoanThuong.ThuongNhanViens == null)
            {
                khoanThuong.ThuongNhanViens = new List<ThuongNhanVien>();
            }

            List<string> nhanVienIds = cdKhoanThuongNV.NhanVienIds;
            foreach (string nhanVienId in nhanVienIds)
            {
                ThuongNhanVien newTnv = new ThuongNhanVien()
                {
                    NhanVienId = nhanVienId,
                    ThuongId = khoanThuong.MaKhoanThuong,
                    LanThu = _dbContext.ThuongNhanViens.Count(th => th.NhanVienId.Equals(nhanVienId)) + 1
                };

                khoanThuong.ThuongNhanViens.Add(newTnv);
            }

            await _dbContext.SaveChangesAsync();

            await recalculateBasedOnAllowanceAndBonusTarget.RecalculateBasedOnAllowanceAndBonus(khoanThuong.MaKhoanThuong, LoaiTinhLaiSoLieu.Thuong);

            return _thuongProfile.MapToDto(khoanThuong);
        }

        public async Task<bool> XoaThuongNhanVien(CDKhoanThuongNV cdKhoanThuongNV)
        {
            Thuong? khoanThuong = await _dbContext.Thuongs
                .Include(t => t.ThuongNhanViens)
                .SingleOrDefaultAsync(t => t.MaKhoanThuong.Equals(cdKhoanThuongNV.ThuongId))
                    ?? throw new NotFoundException("Không tìm thấy khoản thưởng");

            if (khoanThuong.ThuongNhanViens == null)
            {
                khoanThuong.ThuongNhanViens = new List<ThuongNhanVien>();
            }

            List<string> nhanVienIds = cdKhoanThuongNV.NhanVienIds;

            foreach (string nhanVienId in nhanVienIds)
            {
                ThuongNhanVien? thuongNhanVien = khoanThuong.ThuongNhanViens.FirstOrDefault(tnv => tnv.NhanVienId.Equals(nhanVienId));

                if (thuongNhanVien != null)
                {
                    khoanThuong.ThuongNhanViens.Remove(thuongNhanVien);
                }
            }

            await _dbContext.SaveChangesAsync();
            await recalculateBasedOnAllowanceAndBonusTarget.RecalculateBasedOnAllowanceAndBonus(khoanThuong.MaKhoanThuong, LoaiTinhLaiSoLieu.Thuong);

            return true;
        }
    }
}
