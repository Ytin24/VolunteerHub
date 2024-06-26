﻿using ReactiveUI;
using System;
using VolunteerHub.ViewModels;
using VolunteerHub.Views;

namespace VolunteerHub {
    internal class Locator : IViewLocator {
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null) => viewModel switch {
            AuthViewModel ctx => new AuthView() { DataContext = ctx },
            VolunteerHubViewModel ctx => new VolunteerHubView() { DataContext = ctx },
            AdminHubViewModel ctx => new AdminHubView() { DataContext = ctx },
            RegistrationViewModel ctx => new RegistrationView() {  DataContext = ctx },
            _ => throw new ArgumentNullException(nameof(viewModel))
        };
    }
}
