using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Adapter.Create_ChamcongObserver;
using HumanResourceManagement.Pattern.Adapter.Recaculate_based_on_allowance_and_bonus;
using HumanResourceManagement.Pattern.Adapter.Update_DotTraLuong;
using HumanResourceManagement.Pattern.Factory;
using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;
using HumanResourceManagement.Pattern.Factory.IncomeCalculationFactory;
using HumanResourceManagement.Pattern.Factory.SearchEmployeeStrategyFactory;
using HumanResourceManagement.Pattern.Observer.ChamCongObserver;
using HumanResourceManagement.Pattern.Observer.RemoveLichLamNhanVienObserver;
using HumanResourceManagement.Pattern.Singleton;
using HumanResourceManagement.Pattern.State;
using HumanResourceManagement.Pattern.Strategy.LeaveByMonthAndYear;
using HumanResourceManagement.Pattern.Visitor;
using HumanResourceManagement.Profile;
using HumanResourceManagement.Services.ChamCongService;
using HumanResourceManagement.Services.ChucVuService;
using HumanResourceManagement.Services.DotTraLuongService;
using HumanResourceManagement.Services.HieuSuatService;
using HumanResourceManagement.Services.HopDongService;
using HumanResourceManagement.Services.LichLamService;
using HumanResourceManagement.Services.LuongService;
using HumanResourceManagement.Services.NghiVangService;
using HumanResourceManagement.Services.NhanVienService;
using HumanResourceManagement.Services.PhongBanService;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;
using HumanResourceManagement.Services.TaiKhoanService;
using HumanResourceManagement.Services.ThueService;
using HumanResourceManagement.Services.ThuongService;
using HumanResourceManagement.Services.TraLuongService;
using HumanResourceManagement.Services.TrinhDoHocVanService;
using HumanResourceManagement.Utils;

namespace HumanResourceManagement.Extensions.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Register dependencies for server classes
            services.AddScoped<SearchEmployeeStrategyFactory>();
            services.AddScoped<INhanVienService, NhanVienService>();
            services.AddScoped<ITrinhDoHocVanService, TrinhDoHocVanService>();
            services.AddScoped<IHopDongService, HopDongService>();
            services.AddScoped<IHieuSuatService, HieuSuatService>();
            services.AddScoped<IPhongBanService, PhongBanService>();
            services.AddScoped<IChucVuService, ChucVuService>();
            services.AddScoped<IPhuCapService, PhuCapService>();
            services.AddScoped<IThuongService, ThuongService>();
            services.AddScoped<ILichLamService, LichLamService>();
            services.AddScoped<IChamCongService, ChamCongService>();
            services.AddScoped<INghiVangService, NghiVangService>();
            services.AddScoped<IThueService, ThueService>();
            services.AddScoped<ILuongService, LuongService>();
            services.AddScoped<IDotTraLuongService, DotTraLuongService>();
            services.AddScoped<ITraLuongService, TraLuongService>();
            services.AddScoped<ITaiKhoanService, TaiKhoanService>();
            services.AddScoped<IIncomeCalculationFactory, ConcreteCalculateTotalIncomeFactory>();
            services.AddScoped<ICalculateIncomeService, CalculateIncomeService>();
            services.AddScoped<ISalaryCalculationService, SalaryCalculationService>();
            services.AddScoped<ICreateChamCongObserverTarget, CreateChamCongObserverAdapter>();
            services.AddScoped<IRecalculateBasedOnAllowanceAndBonusTarget, RecalculateBasedOnAllowanceAndBonusAdapter>();
            services.AddScoped<IUpdateDotTraLuongTarget, UpdateDotTraLuongAdapter>();
            services.AddScoped<INhanVienVisitor, RecalculateVisitor>();
            services.AddScoped<ILichLamNhanVienObserver, LichLamNhanVienObserver>();


            services.AddScoped<DotTraLuongConcrete>();
            services.AddScoped<LuongConcrete>();
            services.AddScoped<ThueConcrete>();
            services.AddScoped<TraLuongConcrete>();


            services.AddScoped<RecalculateSalary>();
            services.AddScoped<CreateChamCongObserverAdaptee>();

            services.AddScoped<ICalculateNumberOfLeaveByMonthAndYearStrategy, CalculateNumberOfMonthlyAndYearlyLeaveBePaidStrategy>();

            services.AddScoped<MyPhuCapsService>();
            services.AddScoped<UpdateDotTraLuongAdaptee>();
            services.AddScoped<RecalculateBasedOnAllowanceAndBonusAdaptee>();

            // Register dependencies for auto-id generated classes
            var entityTypes = new[] { typeof(ChucVu), typeof(PhongBan), typeof(NhanVien), typeof(TrinhDoHocVan),
                typeof(PhuCap), typeof(Thue), typeof(Thuong), typeof(HopDong), typeof(HieuSuat), typeof(Luong),
                typeof(DotTraLuong), typeof(TraLuong), typeof(LichLam), typeof(NghiVang), typeof(ChamCong)
};

            foreach (var entityType in entityTypes)
            {
                var createIdAdapterType = typeof(CreateIdAdapter<>).MakeGenericType(entityType);
                var uniqueIdGeneratorType = typeof(IUniqueIdGenerator<>).MakeGenericType(entityType);

                services.AddScoped(uniqueIdGeneratorType, createIdAdapterType);
            }

            services.AddScoped(typeof(CreateId<>));

            // Profile
            services.AddScoped<ThuongProfile>();
            services.AddScoped<ThueProfile>();
            services.AddScoped<LuongProfile>();
            services.AddScoped<ChamCongProfile>();
            services.AddScoped<TraLuongProfile>();
            services.AddScoped<DotTraLuongProfile>();
            services.AddScoped<HieuSuatProfile>();
            services.AddScoped<HopDongProfile>();
            services.AddScoped<NghiVangProfile>();
            services.AddScoped<TrinhDoHocVanProfile>();
            services.AddScoped<NhanVienProfile>();
            services.AddScoped<PhuCapProfile>();
            services.AddScoped<LichLamProfile>();
            services.AddScoped<ChucVuProfile>();
            services.AddScoped<TaiKhoanProfile>();
            
            services.AddScoped<PasswordEncoder>();
            services.AddScoped<JwtUtils>();

            return services;
        }
    }
}
