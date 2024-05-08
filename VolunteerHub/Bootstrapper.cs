using ReactiveUI;
using Splat;
using VolunteerHub.Models.Services;
using VolunteerHub.ViewModels;

namespace VolunteerHub;

public static class Bootstrapper {
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver) {

        services.RegisterLazySingleton<UserService>(() => new UserService());
        services.RegisterLazySingleton<RoutingState>(() => new RoutingState());
        services.RegisterLazySingleton<IMainWindowProvider>(() => new MainWindowProvider());
        services.RegisterLazySingleton<IProjectWindowService>(() => new ProjectWindowService(
           resolver.GetRequiredService<IMainWindowProvider>()
           ));
        RegisterCommonViewModels(services, resolver);
        RegisterConst(services, resolver);

    }
    private static void RegisterConst(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver) {
        services.RegisterConstant<IMutableDependencyResolver>(services);

        services.RegisterConstant(new MainViewModel(
           resolver.GetRequiredService<IMutableDependencyResolver>()
           ));

        services.RegisterConstant(new AuthViewModel(
            resolver.GetRequiredService<IScreen>(),
            resolver.GetRequiredService<UserService>(),
            resolver.GetRequiredService<AdminHubViewModel>(),
            resolver.GetRequiredService<VolunteerHubViewModel>()
            ));

    }
    private static void RegisterCommonViewModels(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver) {
        services.RegisterLazySingleton(() => new AdminHubViewModel(
            resolver.GetRequiredService<IScreen>(),
            resolver.GetRequiredService<UserService>(),
            resolver.GetRequiredService<IProjectWindowService>()
            ));
        services.RegisterLazySingleton(() => new VolunteerHubViewModel(
            resolver.GetRequiredService<IScreen>(),
            resolver.GetRequiredService<UserService>()
            ));


    }
}
