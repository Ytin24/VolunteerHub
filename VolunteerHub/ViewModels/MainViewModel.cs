using Avalonia.Media;
using ReactiveUI;
using Splat;

namespace VolunteerHub.ViewModels;

public class MainViewModel : ReactiveObject, IScreen {
    public MainViewModel(IMutableDependencyResolver mutableDependencyResolver) {
        mutableDependencyResolver.RegisterConstant<IScreen>(this);
    }  
    public RoutingState Router { get; set; } = new RoutingState();
}
