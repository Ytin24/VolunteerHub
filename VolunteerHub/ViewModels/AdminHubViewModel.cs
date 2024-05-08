using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using DynamicData.Aggregation;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerHub.Db;
using VolunteerHub.Models;
using VolunteerHub.Models.Services;
using VolunteerHub.Views.Controls;

namespace VolunteerHub.ViewModels
{
    public class AdminHubViewModel : ViewModelBase
    {
        private readonly VolunteerDbContext _db = new();
        private readonly IProjectWindowService _windowProvider;
        public UserService _userService { get; set; }

        public AdminHubViewModel(IScreen screen, UserService us, IProjectWindowService windowProvider) : base(screen)
        {
            _userService = us;
            _windowProvider = windowProvider;
            LoadProjects();

            // Подписываемся на изменение выбранного проекта
            this.WhenAnyValue(x => x.SelectedProject)
                .Subscribe(_ => LoadUsersForSelectedProject());

            // Подписываемся на изменение выбранного статуса
            this.WhenAnyValue(x => x.SelectedStatus)
                .Where(status => status != null)
                .Subscribe(_ => UpdateProjectStatus.Execute().Subscribe());

            RemoveVolunteer = ReactiveCommand.Create(() =>
            {
                var target = SelectedProject?.ProjectParticipations.FirstOrDefault(x => x.UserId == SelectedUser?.UserId);
                if (target == null) return;
                Users.Remove(SelectedUser);
                _db.ProjectParticipations.Remove(target);
                _db.SaveChanges();
            });

            UpdateProjectStatus = ReactiveCommand.CreateFromTask(async () =>
            {
                if (SelectedProject == null || SelectedStatus == null) return;

                // Получаем экземпляр проекта из базы данных
                var project = await _db.Projects.FindAsync(SelectedProject.ProjectId);
                if (project == null) return; 
                project.Statuses.Add(SelectedStatus);

                await _db.SaveChangesAsync();
                var lstproj = SelectedProject;
                SelectedProject = null;
                SelectedProject = Projects.First(x=>x==lstproj);
                
            });

            OpenCloseMenuPane = ReactiveCommand.Create(
                () =>
                { this.IsModePaneOpen = !this.IsModePaneOpen; });

            AddNewProject = ReactiveCommand.CreateFromTask(async () => {
                var observableProject = _windowProvider.OpenProjectWindow().Subscribe(
                    onNext: project => {
                        try {
                            _db.Projects.Add(project);
                            _db.SaveChanges();
                            LoadProjects();
                        }
                        catch (DbUpdateException ex) {
                            _db.Projects.Remove(project);
                        }
                        catch (Exception ex) {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                    },
                    onError: ex => {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    },
                    onCompleted: () => {
                        Console.WriteLine("Создание проекта завершено");
                    });
            });

            var statuses = _db.ProjectStatuses.ToList();
            Statuses = new ObservableCollection<ProjectStatus>(statuses);
        }

        private void LoadProjects()
        {
            var projects = _db.Projects.Include(p => p.ProjectParticipations).ThenInclude(pp => pp.User).Include(s=>s.Statuses).ToList();
            Projects = new ObservableCollection<Project>(projects);
            this.RaisePropertyChanged(nameof(Projects));
        }

        private void LoadUsersForSelectedProject()
        {
            Users.Clear();
            if (SelectedProject != null)
            {
                foreach (var participation in SelectedProject.ProjectParticipations)
                {
                    Users.Add(participation.User);
                }
            }
            this.RaisePropertyChanged(nameof(Users));
        }

        public ObservableCollection<Project> Projects { get; private set; } = new();
        public ObservableCollection<User> Users { get; private set; } = new();

        public ObservableCollection<ProjectStatus> Statuses { get; private set; } = new();

        private Project _selectedProject;

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                this.RaisePropertyChanged();
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                this.RaisePropertyChanged();
            }
        }

        private ProjectStatus _selectedStatus;

        public ProjectStatus SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                _selectedStatus = value;
                this.RaisePropertyChanged();
            }
        }

        public ReactiveCommand<Unit, Unit> RemoveVolunteer { get; }
        public ReactiveCommand<Unit, Unit> UpdateProjectStatus { get; }
        public ReactiveCommand<Unit, Unit> OpenCloseMenuPane { get; }
        public ReactiveCommand<Unit, Unit> AddNewProject { get; }


        private bool _isModePaneOpen;

        public bool IsModePaneOpen
        {
            get { return _isModePaneOpen; }
            set
            {
                _isModePaneOpen = value;
                this.RaisePropertyChanged();
            }
        }

    }
}
