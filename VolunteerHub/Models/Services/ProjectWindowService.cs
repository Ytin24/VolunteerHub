using Avalonia.Controls;
using Avalonia.Layout;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace VolunteerHub.Models.Services {
    public interface IProjectWindowService {
        IObservable<Project> OpenProjectWindow();
    }
    public class ProjectWindowService : IProjectWindowService {
        private readonly IMainWindowProvider _windowProvider;

        // Переменные для элементов управления
        private TextBox _projectNameTextBox;
        private TextBox _descriptionTextBox;
        private DatePicker _startDatePicker;
        private TimePicker _startTimePicker;
        private DatePicker _endDatePicker;
        private TimePicker _endTimePicker;

        public ProjectWindowService(IMainWindowProvider windowProvider) {
            _windowProvider = windowProvider;
        }

        public IObservable<Project> OpenProjectWindow() {
            return Observable.Create<Project>(async observer => {
                var popupWindow = CreatePopupWindow();

                var grid = new Grid();
                SetupGrid(grid);

                var stackPanel = CreateStackPanel();
                SetupStackPanel(stackPanel);

                AddControlsToStackPanel(stackPanel);

                var buttonStackPanel = CreateButtonStackPanel();
                SetupButtonStackPanel(buttonStackPanel);

                var okCommand = CreateOkCommand(observer, popupWindow);
                var cancelButton = CreateCancelButton(observer, popupWindow);

                SetupButtonControls(buttonStackPanel, okCommand, cancelButton);

                AddControlsToGrid(grid, stackPanel, buttonStackPanel);

                popupWindow.Content = grid;
                await popupWindow.ShowDialog(_windowProvider.GetMainWindow());

                return () => { };
            });
        }

        private Window CreatePopupWindow() {
            return new Window {
                Title = "Форма заполнения проекта",
                Width = 600,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                CanResize = false,
                ShowInTaskbar = false
            };
        }

        private void SetupGrid(Grid grid) {
            grid.RowDefinitions = new RowDefinitions("*,Auto");
        }

        private StackPanel CreateStackPanel() {
            return new StackPanel();
        }

        private void SetupStackPanel(StackPanel stackPanel) {
            // Setup stack panel properties, if needed
        }

        private void AddControlsToStackPanel(StackPanel stackPanel) {
            // Присвоение переменным элементов управления
            _projectNameTextBox = CreateTextBoxWithWatermark("Название проекта");
            _descriptionTextBox = CreateTextBoxWithWatermark("Описание");
            _startDatePicker = CreateDatePicker();
            _startTimePicker = CreateTimePicker();
            _endDatePicker = CreateDatePicker();
            _endTimePicker = CreateTimePicker();

            // Добавление элементов управления в стек-панель
            stackPanel.Children.AddRange(new Control[]
            {
                CreateTextBlock("Название проекта:"),
                _projectNameTextBox,
                CreateTextBlock("Описание:"),
                _descriptionTextBox,
                CreateTextBlock("Дата начала:"),
                _startDatePicker,
                CreateTextBlock("Время начала:"),
                _startTimePicker,
                CreateTextBlock("Дата окончания:"),
                _endDatePicker,
                CreateTextBlock("Время окончания:"),
                _endTimePicker
            });
        }

        private TextBlock CreateTextBlock(string text) {
            return new TextBlock { Text = text };
        }

        private TextBox CreateTextBoxWithWatermark(string watermark) {
            return new TextBox { Watermark = watermark };
        }

        private DatePicker CreateDatePicker() {
            return new DatePicker();
        }

        private TimePicker CreateTimePicker() {
            return new TimePicker();
        }

        private StackPanel CreateButtonStackPanel() {
            var buttonStackPanel = new StackPanel();
            buttonStackPanel.Orientation = Orientation.Horizontal;
            buttonStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            buttonStackPanel.Spacing = 10;
            return buttonStackPanel;
        }

        private void SetupButtonStackPanel(StackPanel buttonStackPanel) {
            // Setup button stack panel properties, if needed
        }

        private void AddControlsToGrid(Grid grid, StackPanel stackPanel, StackPanel buttonStackPanel) {
            grid.Children.Add(stackPanel);
            grid.Children.Add(buttonStackPanel);
            Grid.SetRow(stackPanel, 0);
            Grid.SetRow(buttonStackPanel, 1);
        }

        private ReactiveCommand<Unit, Unit> CreateOkCommand(IObserver<Project> observer, Window popupWindow) {
            // Валидация для определения доступности команды
            var canExecute = this.WhenAnyValue(
                x => x._projectNameTextBox.Text,
                x => x._descriptionTextBox.Text,
                x => x._startDatePicker.SelectedDate,
                x => x._startTimePicker.SelectedTime,
                x => x._endDatePicker.SelectedDate,
                x => x._endTimePicker.SelectedTime,
                (projectName, description, startDate, startTime, endDate, endTime) =>
                !string.IsNullOrWhiteSpace(projectName)
                && !string.IsNullOrWhiteSpace(description)
                && startDate.HasValue && startTime.HasValue
                && endDate.HasValue && endTime.HasValue
            );

            return ReactiveCommand.Create(() => {
                var project = CreateProject();
                observer.OnNext(project);
                observer.OnCompleted();
                popupWindow.Close();
            }, canExecute);
        }

        private Project CreateProject() {
            // Получение данных из элементов управления
            var projectName = _projectNameTextBox.Text?.Take(100).ToString();
            var description = _descriptionTextBox.Text;
            var startDate = _startDatePicker.SelectedDate ?? DateTime.MinValue;
            var startTime = _startTimePicker.SelectedTime ?? TimeSpan.Zero;
            var endDate = _endDatePicker.SelectedDate ?? DateTime.MinValue;
            var endTime = _endTimePicker.SelectedTime ?? TimeSpan.Zero;

            // Создание объекта Project
            return new Project {
                ProjectName = projectName,
                Description = description,
                StartDate = startDate.Date + startTime,
                EndDate = endDate.Date + endTime,
            };
        }

        private Button CreateCancelButton(IObserver<Project> observer, Window popupWindow) {
            var cancelButton = new Button { Content = "Отмена" };
            cancelButton.Click += (s, args) => {
                observer.OnCompleted();
                popupWindow.Close();
            };
            return cancelButton;
        }

        private void SetupButtonControls(StackPanel buttonStackPanel, ReactiveCommand<Unit, Unit> okCommand, Button cancelButton) {
            var okButton = new Button { Content = "OK" };
            okButton.Command = okCommand;
            buttonStackPanel.Children.Add(okButton);
            buttonStackPanel.Children.Add(cancelButton);
        }
    }
}
