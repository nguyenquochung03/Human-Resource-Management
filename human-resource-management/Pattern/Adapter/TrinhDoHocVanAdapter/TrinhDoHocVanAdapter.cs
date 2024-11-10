using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Services.TrinhDoHocVanService;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Adapter.TrinhDoHocVanAdapter
{
    public class TrinhDoHocVanAdapter : ITrinhDoHocVanTargetDTO
    {
        private ITrinhDoHocVanService trinhDoHocVanService;

        public TrinhDoHocVanAdapter(ITrinhDoHocVanService trinhDoHocVanService)
        {
            this.trinhDoHocVanService = trinhDoHocVanService;
        }

        public async Task<bool> SuaTrinhDoHocVan(string id, TrinhDoHocVanDTO trinhDoHocVanDTO)
        {
            TrinhDoHocVan trinhDoHocVan = new TrinhDoHocVan();

            trinhDoHocVan.MaTrinhDo = id;
            trinhDoHocVan.TenTrinhDoHocVan = trinhDoHocVanDTO.TenTrinhDoHocVan;
            trinhDoHocVan.ChuyenNganh = trinhDoHocVanDTO.ChuyenNganh;
            trinhDoHocVan.TenTruong = trinhDoHocVanDTO.TenTruong;
            trinhDoHocVan.NameTotNghiep = new DateTime(trinhDoHocVanDTO.NamTotNghiep, 1, 1);
            trinhDoHocVan.BangCap = trinhDoHocVanDTO.BangCap;

            if (!string.IsNullOrEmpty(trinhDoHocVanDTO.NhanVienId))
            {
                trinhDoHocVan.NhanVienId = trinhDoHocVanDTO.NhanVienId;
            }
            else
            {
                trinhDoHocVan.NhanVienId = null;
            }

            return await trinhDoHocVanService.SuaTrinhDoHocVan(id, trinhDoHocVan);
        }

        public async Task<object> ThemTrinhDoHocVan(TrinhDoHocVanDTO trinhDoHocVanDTO)
        {
            TrinhDoHocVan trinhDoHocVan = new TrinhDoHocVan();

            trinhDoHocVan.TenTrinhDoHocVan = trinhDoHocVanDTO.TenTrinhDoHocVan;
            trinhDoHocVan.ChuyenNganh = trinhDoHocVanDTO.ChuyenNganh;
            trinhDoHocVan.TenTruong = trinhDoHocVanDTO.TenTruong;
            trinhDoHocVan.NameTotNghiep = new DateTime(trinhDoHocVanDTO.NamTotNghiep, 1, 1);
            trinhDoHocVan.BangCap = trinhDoHocVanDTO.BangCap;

            if (!string.IsNullOrEmpty(trinhDoHocVanDTO.NhanVienId))
            {
                trinhDoHocVan.NhanVienId = trinhDoHocVanDTO.NhanVienId;
            }
            else
            {
                trinhDoHocVan.NhanVienId = null;
            }

            return await trinhDoHocVanService.ThemTrinhDoHocVan(trinhDoHocVan);
        }

    }
}
