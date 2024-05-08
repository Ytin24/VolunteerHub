using Avalonia.ReactiveUI;
using VolunteerHub.ViewModels;

namespace VolunteerHub.Views;

public partial class AuthView : ReactiveUserControl<AuthViewModel> {
    public AuthView() {
        InitializeComponent();
    }
}
