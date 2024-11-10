using HumanResourceManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace HumanResourceManagement.Pattern.Observer.RemoveLichLamNhanVienObserver
{
    public class LichLamNhanVienObserver : ILichLamNhanVienObserver
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public LichLamNhanVienObserver([FromServices] IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async void Update(LichLam lichLam)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HumanResourceManagementDbContext>();

                var lichLamNhanViensToRemove = context.LichLamNhanViens
                                           .Where(llnv => llnv.LichLamId == lichLam.MaLichLam)
                                           .ToList();

                context.LichLamNhanViens.RemoveRange(lichLamNhanViensToRemove);
                context.LichLams.Remove(lichLam);
                await context.SaveChangesAsync();
            }
        }
    }
}
