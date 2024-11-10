using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Observer;
using HumanResourceManagement.Pattern.Observer.RemoveLichLamNhanVienObserver;
using HumanResourceManagement.Pattern.Template.ObjectCopier;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.LichLamService
{
    public class LichLamService : ILichLamService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<LichLam> _uniqueIdGenerator;
        private readonly LichLamProfile _lichLamProfile;
        private readonly ILichLamNhanVienObserver lichLamNhanVienObserver;

        public LichLamService(HumanResourceManagementDbContext dbContext, IUniqueIdGenerator<LichLam> uniqueIdGenerator, LichLamProfile lichLamProfile, ILichLamNhanVienObserver lichLamNhanVienObserver)
        {
            _dbContext = dbContext;
            _uniqueIdGenerator = uniqueIdGenerator;
            _lichLamProfile = lichLamProfile;
            this.lichLamNhanVienObserver = lichLamNhanVienObserver;
        }

        public async Task<ICollection<object>> GetLichLams()
        {
            ICollection<LichLam> lichLams = await _dbContext.LichLams.ToListAsync();
            return _lichLamProfile.MapToListDto(lichLams);
        }
        public dynamic GetListByPage(int page = 1, int pageSize = 10)
        {
            // Lấy tổng số lượng mục
            var totalCount = _dbContext.LichLams.Count();

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Sử dụng Skip() và Take() để phân trang
            var items = _dbContext.LichLams
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = _lichLamProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<ICollection<LichLamNhanVien>> GetLichLamNhanViens()
        {
            List<LichLamNhanVien> lichLamNhanViens = await _dbContext.LichLamNhanViens
               .Include(llnv => llnv.NhanVien)
               .Include(llnv => llnv.LichLam)
               .ToListAsync();

            LichLamNhanVienCopier copier = new LichLamNhanVienCopier();

            return copier.CopyObjects(lichLamNhanViens);
        }

        public async Task<object> GetLichLamById(string id)
        {
            LichLam? lichLam = await _dbContext.LichLams
                .SingleOrDefaultAsync(dt => dt.MaLichLam.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy lịch làm việc");

            return _lichLamProfile.MapToDto(lichLam);
        }

        public async Task<ICollection<object>> GetLichLamByMaNhanVien(string maNhanVien)
        {
            NhanVien? nv = await _dbContext.NhanViens
                .SingleOrDefaultAsync(n => n.MaNhanVien.Equals(maNhanVien))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            ICollection<LichLam> lichLams = await _dbContext.LichLams
                .Include(l => l.LichLamNhanViens)
                .ThenInclude(llnv => llnv.NhanVien)
                .Where(l => l.LichLamNhanViens.Any(llnv => llnv.NhanVien.MaNhanVien.Equals(maNhanVien)))
                .ToListAsync();

            return _lichLamProfile.MapToListDto(lichLams);
        }

        public async Task<object> ThemLichLam(LichLamDTO lichLamDTO)
        {
            LichLam? checkLichLam = await _dbContext.LichLams
                .SingleOrDefaultAsync(dt => dt.NgayLam.Date == lichLamDTO.NgayLam.Date);

            if (checkLichLam != null)
                throw new ConflictException("Lịch làm đã tồn tại");
            LichLam lichLam = new LichLam()
            {
                MaLichLam = await _uniqueIdGenerator.GetUniqueID("LLV", 8, "MaLichLam"),
                GhiChu = lichLamDTO.GhiChu,
                GioBatDau = lichLamDTO.GioBatDau,
                GioKetThuc = lichLamDTO.GioKetThuc,
                MoTaCongViec = lichLamDTO.MoTaCongViec,
                NgayLam = lichLamDTO.NgayLam,
                LoaiLich = lichLamDTO.LoaiLich
            };

            await _dbContext.LichLams.AddAsync(lichLam);
            await _dbContext.SaveChangesAsync();

            return _lichLamProfile.MapToDto(lichLam);
        }

        public async Task<bool> CapNhatLichLam(string id, LichLamDTO lichLamDTO)
        {
            LichLam? lichLam = await _dbContext.LichLams
                .SingleOrDefaultAsync(dt => dt.MaLichLam.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy lịch làm");

            lichLam.GhiChu = lichLamDTO.GhiChu;
            lichLam.GioBatDau = lichLamDTO.GioBatDau;
            lichLam.GioKetThuc = lichLamDTO.GioKetThuc;
            lichLam.MoTaCongViec = lichLamDTO.MoTaCongViec;
            lichLam.NgayLam = lichLamDTO.NgayLam;
            lichLam.LoaiLich = lichLamDTO.LoaiLich;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> XoaLichLam(string id)
        {
            LichLam? lichLam = await _dbContext.LichLams
                .SingleOrDefaultAsync(dt => dt.MaLichLam.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy lịch làm");

            bool checkChamCong = await _dbContext.ChamCongs
                .AnyAsync(chc => chc.NgayChamCong.Date == lichLam.NgayLam.Date);

            if (checkChamCong)
                throw new ConflictException("Không thể xóa lịch làm");

      
            ILichLamSubject lichLamSubject = new LichLamSubject(lichLam);
            lichLamSubject.Attach(lichLamNhanVienObserver);
            lichLamSubject.Notify();
          

            return true;
        }

        public async Task<bool> ThemLichLamNhanVien(CDLichLamForNhanVienDTO cDLichLamForNhanVienDTO)
        {
            LichLam? lichLam = await _dbContext.LichLams
                .Include(ll => ll.LichLamNhanViens)
                .SingleOrDefaultAsync(ll => ll.MaLichLam.Equals(cDLichLamForNhanVienDTO.LichLamId))
                    ?? throw new NotFoundException("Không tìm thấy lịch làm");

            if (lichLam.LichLamNhanViens == null)
                lichLam.LichLamNhanViens = new List<LichLamNhanVien>();

            List<string> nhanVienIds = cDLichLamForNhanVienDTO.NhanVienIds;

            for (int i = 0; i < nhanVienIds.Count; i++)
            {
                bool hasExisted = lichLam.LichLamNhanViens.Any(ll => ll.NhanVienId.Equals(nhanVienIds[i]));

                if (hasExisted) continue;

                LichLamNhanVien lichLamNhanVien = new LichLamNhanVien()
                {
                    LichLam = lichLam,
                    NhanVienId = nhanVienIds[i]
                };

                lichLam.LichLamNhanViens.Add(lichLamNhanVien);
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> XoaLichLamNhanVien(CDLichLamForNhanVienDTO cDLichLamForNhanVienDTO)
        {
            LichLam? lichLam = await _dbContext.LichLams
                 .Include(ll => ll.LichLamNhanViens)
                 .SingleOrDefaultAsync(ll => ll.MaLichLam.Equals(cDLichLamForNhanVienDTO.LichLamId))
                     ?? throw new NotFoundException("Không tìm thấy lịch làm");

            if (lichLam.LichLamNhanViens == null)
                lichLam.LichLamNhanViens = new List<LichLamNhanVien>();

            List<string> nhanVienIds = cDLichLamForNhanVienDTO.NhanVienIds;

            foreach (string nhanVienId in nhanVienIds)
            {
                LichLamNhanVien? lichLamNhanVien = lichLam.LichLamNhanViens.FirstOrDefault(llnv => llnv.NhanVienId.Equals(nhanVienId));

                if (lichLamNhanVien != null)
                {
                    lichLam.LichLamNhanViens.Remove(lichLamNhanVien);
                }
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<object>> GetLichLamsFromTo(DateTime from, DateTime to)
        {
            ICollection<LichLam> lichLams = await _dbContext.LichLams
               .Where(chc =>
                  (chc.NgayLam.Date >= from.Date && chc.NgayLam.Date <= to.Date)
               ).ToListAsync();

            return _lichLamProfile.MapToListDto(lichLams);
        }
    }
}
