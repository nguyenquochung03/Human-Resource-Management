using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter.Recaculate_based_on_allowance_and_bonus;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.PhuCapService
{
    public class MyPhuCapsService 
    {
        private readonly IRecalculateBasedOnAllowanceAndBonusTarget recalculateBasedOnAllowanceAndBonusTarget;
        private readonly IPhuCapService phuCapService;
        private readonly HumanResourceManagementDbContext context;

        public MyPhuCapsService(IPhuCapService phuCapService, IRecalculateBasedOnAllowanceAndBonusTarget recalculateBasedOnAllowanceAndBonusTarget, 
            HumanResourceManagementDbContext context)
        {
            this.phuCapService = phuCapService;
            this.recalculateBasedOnAllowanceAndBonusTarget = recalculateBasedOnAllowanceAndBonusTarget;
            this.context = context;
        }

        public dynamic GetListByPage(int page = 1, int pageSize = 10)
        {
            return phuCapService.GetListByPage(page, pageSize);
        }

        public async Task<object> GetPhuCapById(string id)
        {
            return await phuCapService.GetPhuCapById(id);
        }

        public async Task<ICollection<PhuCap>> GetPhuCapNhanVienByEmployeeId(string employeeId)
        {
            return await phuCapService.GetPhuCapNhanVienByEmployeeId(employeeId);
        }

        public async Task<ICollection<PhuCapNhanVien>> GetPhuCapNhanViens()
        {
            return await phuCapService.GetPhuCapNhanViens();
        }

        public async Task<ICollection<object>> GetPhuCaps()
        {
            return await phuCapService.GetPhuCaps();
        }

        public async Task<object> ThemPhuCap(PhuCapDTO phuCap)
        {
            return await phuCapService.ThemPhuCap(phuCap);
        }

        public async Task<bool> SuaPhuCap(string id, PhuCapDTO phuCap)
        {
            bool isEdit = await phuCapService.SuaPhuCap(id, phuCap);

            await recalculateBasedOnAllowanceAndBonusTarget.RecalculateBasedOnAllowanceAndBonus(id, LoaiTinhLaiSoLieu.PhuCap);
            return isEdit;
        }

        public Task<bool> XoaPhuCap(string id)
        {
            return phuCapService.XoaPhuCap(id);
        }

        public async Task<object> AddPhuCapForNvs(CDPhuCapForNhanVienDTO dto)
        {
            object addPhuCapForNhanVien = await phuCapService.AddPhuCapForNvs(dto);

            PhuCap? phuCap = await context.PhuCaps
               .Include(pc => pc.PhuCapNhanViens)
               .SingleOrDefaultAsync(pc => pc.MaPhuCap.Equals(dto.PhuCapId));

            if (phuCap != null) await recalculateBasedOnAllowanceAndBonusTarget.RecalculateBasedOnAllowanceAndBonus(phuCap.MaPhuCap, LoaiTinhLaiSoLieu.PhuCap);

            return addPhuCapForNhanVien;
        }

        public async Task<bool> XoaPhuCapForNvs(CDPhuCapForNhanVienDTO dto)
        {
            bool deletePhuCapForNhanVien = await phuCapService.XoaPhuCapForNvs(dto);

            PhuCap? phuCap = await context.PhuCaps
               .Include(pc => pc.PhuCapNhanViens)
               .SingleOrDefaultAsync(pc => pc.MaPhuCap.Equals(dto.PhuCapId));

            if (phuCap != null) await recalculateBasedOnAllowanceAndBonusTarget.RecalculateBasedOnAllowanceAndBonus(phuCap.MaPhuCap, LoaiTinhLaiSoLieu.PhuCap);

            return deletePhuCapForNhanVien;
        }
    }
}
