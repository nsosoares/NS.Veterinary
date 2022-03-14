using NS.Veterinary.Api.Data.Repositorys;
using NS.Veterinary.Api.Data.UOW;
using NS.Veterinary.Api.Interfaces;
using NS.Veterinary.Api.Notifications;

namespace NS.Veterinary.Api.Configurations
{
    public static class DIConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //Repositorys
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();

            //UOW
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Notifications
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
