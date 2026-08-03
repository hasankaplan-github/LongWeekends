using Haskap.DddBase.Utilities.Module;
using Modules.GlobalExceptionHandling.Module;
using Modules.ModuleManagement.Module;
using Modules.SpecialDayBase.Module;
using Modules.TurkiyeSpecialDay.Module;

namespace LongWeekends;

public static class DependencyInjection
{
    private const string ConnectionStringName = ""; //"NeonDbConnectionString";

    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddModule<ModuleManagementModule.Registrar>(configuration, ConnectionStringName, null);
        //services.AddModule<AuditLogModule.Registrar>(configuration, ConnectionStringName, "AuditLogMigrations");

        services.AddModule<GlobalExceptionHandlingModule.Registrar>(configuration, ConnectionStringName, null);

        services.AddModule<SpecialDayBaseModule.Registrar>(configuration, ConnectionStringName, null);
        services.AddModule<TurkiyeSpecialDayModule.Registrar>(configuration, ConnectionStringName, null);

        return services;
    }
}
