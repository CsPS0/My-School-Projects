using CommunityToolkit.Mvvm.ComponentModel;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase _currentPage;

        public MainWindowViewModel()
        {
            _currentPage = new MainMenuViewModel(NavigateTo);
        }

        private void NavigateTo(ViewModelBase viewModel)
        {
            CurrentPage = viewModel;
        }
    }
}
