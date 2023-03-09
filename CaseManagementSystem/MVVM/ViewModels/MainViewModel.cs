using CaseManagementSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CaseManagementSystem.MVVM.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableObject currentViewModel;

        [RelayCommand]
        private void UserView()
        {
            CurrentViewModel = new UserViewModel();
        }

        [RelayCommand]
        private void CaseView()
        {
            CurrentViewModel = new CaseViewModel();
        }

        public MainViewModel()
        {
            CurrentViewModel = new UserViewModel();
        }
    }
}
