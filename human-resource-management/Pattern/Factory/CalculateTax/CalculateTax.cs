
using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Factory.CalculateTax
{
    public class CalculateTax : ICalculateTax
    {
        private readonly HumanResourceManagementDbContext _context;
        private readonly ThueDTO _thueDTO;
        private readonly ISalaryCalculationService salaryCalculationService;
        private readonly ICalculateIncomeService calculateIncomeService;

        public CalculateTax(HumanResourceManagementDbContext context, ThueDTO thueDTO, IIncomeCalculationFactory incomeCalculationFactory)
        {
            _context = context;
            _thueDTO = thueDTO;
            salaryCalculationService = incomeCalculationFactory.CreateSalaryCalculationService();
            calculateIncomeService = incomeCalculationFactory.CreateCalculateIncomeService();
        }

        public async Task<double> BasicSalaryCalculation()
        {
            return await salaryCalculationService.CalculateSalary(_thueDTO.NhanVien.MaNhanVien, _thueDTO.ThoiGianTinh.Month, _thueDTO.ThoiGianTinh.Year);
        }

        public async Task<double> TotalSalaryCalculation()
        {
            return await calculateIncomeService.CalculateTotal(_thueDTO.NhanVien.MaNhanVien, _thueDTO.ThoiGianTinh.Month, _thueDTO.ThoiGianTinh.Year);
        }

        public async Task<double> TotalInsuranceCalculation()
        {
            double totalSalary = await BasicSalaryCalculation();
            double socialInsurance = totalSalary * 0.08;
            double healthInsurance = totalSalary * 0.015;
            double unemploymentInsurance = totalSalary * 0.01;

            return socialInsurance + healthInsurance + unemploymentInsurance;
        }

        public async Task<double> TotalDeductionsCalculation()
        {
            double totalAllowance = await calculateIncomeService.CalculateAllowance(_thueDTO.NhanVien.MaNhanVien);

            return totalAllowance;
        }

        public async Task<double> TaxableIncomeCalculation()
        {
            double taxableIncome = 0;
            double totalIncome = await TotalSalaryCalculation();
            double totalInsurance = await TotalInsuranceCalculation();
            double totalDeduction = await TotalDeductionsCalculation();
            NhanVien? nhanVien = await _context.NhanViens.SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(_thueDTO.NhanVien.MaNhanVien));

            if (nhanVien != null)
            {
                double totalAmountDeductions = nhanVien.SoNguoiPhuThuoc * 4400000;
                if (nhanVien.TrangThaiHonNhan.Equals(TrangThaiHonNhan.DangHonNhan))
                {
                    totalAmountDeductions += 11000000;
                }

                taxableIncome = totalIncome - (totalInsurance + totalDeduction + totalAmountDeductions);

                if (taxableIncome < 0)
                    return 0;
            }
            return taxableIncome;
        }

        public async Task<double> TaxRateCalculation()
        {
            double taxableIncome = await TaxableIncomeCalculation();

            if (taxableIncome <= 5000000)
            {
                return 0.05;
            }
            else if ((taxableIncome > 5000000) && (taxableIncome <= 10000000))
            {
                return 0.1;
            }
            else if ((taxableIncome > 10000000) && (taxableIncome <= 18000000))
            {
                return 0.15;
            }
            else if ((taxableIncome > 18000000) && (taxableIncome <= 32000000))
            {
                return 0.2;
            }
            else if ((taxableIncome > 32000000) && (taxableIncome <= 52000000))
            {
                return 0.25;
            }
            else if ((taxableIncome > 52000000) && (taxableIncome <= 80000000))
            {
                return 0.3;
            }
            else
            {
                return 0.35;
            }
        }

        public async Task<double> PersionalIncomeTaxCalculation()
        {
            double taxableIncome = await TaxableIncomeCalculation();
            double taxRate = await TaxRateCalculation();

            return taxableIncome * taxRate;
        }

        public async Task<double> SalaryDetuctionsCalculation()
        {
            double totalInsurance = await TotalInsuranceCalculation();
            double persionalIncomeTax = await PersionalIncomeTaxCalculation();

            return totalInsurance + persionalIncomeTax;
        }

        public async Task<double> SalaryReceived()
        {
            double salaryReceived;
            double salaryDetuctions = await SalaryDetuctionsCalculation();
            double totalIncome = await TotalSalaryCalculation();
            double totalBonus = await calculateIncomeService.CalculateBonus(_thueDTO.NhanVien.MaNhanVien, _thueDTO.ThoiGianTinh.Month, _thueDTO.ThoiGianTinh.Year);

            salaryReceived = totalIncome - salaryDetuctions;

            if (salaryReceived < 0)
                return 0;

            return salaryReceived + totalBonus;
        }
    }
}
