using Insttantt.FieldsManagement.Application.Common.Utils;
using Insttantt.FieldsManagement.Application.Common.Interfaces.Utils;
using Insttantt.FieldsManagement.Application.Common.Interfaces.Repository;
using Insttantt.FieldsManagement.Infrastructure.Repositories;
using Insttantt.FieldsManagement.Application.Common.Interfaces.Services;
using Insttantt.FieldsManagement.Application.Services;

namespace Insttantt.FieldsManagement.Api
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IUtility, Utility>();
            services.AddScoped<IFieldService, FieldService>();
            services.AddScoped<IFieldRepository , FieldRepository>();
            services.AddCors();
            return services;
        }
    }
}
