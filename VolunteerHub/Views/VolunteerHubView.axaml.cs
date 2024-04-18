using Avalonia.Controls;
using Avalonia.ReactiveUI;
using VolunteerHub.ViewModels;

namespace VolunteerHub.Views {
    public partial class VolunteerHubView : ReactiveUserControl<VolunteerHubViewModel> {
        public VolunteerHubView() {
            InitializeComponent();
        }
    }
}
