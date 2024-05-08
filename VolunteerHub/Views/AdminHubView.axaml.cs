using Avalonia.ReactiveUI;
using VolunteerHub.ViewModels;

namespace VolunteerHub.Views {
    public partial class AdminHubView : ReactiveUserControl<AdminHubViewModel> {
        public AdminHubView() {
            InitializeComponent();
        }
    }
}
