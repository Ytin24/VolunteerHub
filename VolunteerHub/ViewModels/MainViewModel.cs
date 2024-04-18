using ReactiveUI;

namespace VolunteerHub.ViewModels;

public class MainViewModel : ReactiveObject, IScreen {
    public MainViewModel() {
        Router.Navigate.Execute(new AuthViewModel(this));
    }  
    public RoutingState Router { get; set; } = new RoutingState();
}
