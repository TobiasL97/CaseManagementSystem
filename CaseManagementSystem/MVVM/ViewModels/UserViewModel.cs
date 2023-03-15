using CaseManagementSystem.MVVM.Models;
using CaseManagementSystem.MVVM.Models.Entities;
using CaseManagementSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.MVVM.ViewModels
{
    partial class UserViewModel : ObservableObject
    {


        public UserViewModel()
        {
            
            _ = CaseTypeService.CheckCaseTypeDataBase();
            _ = StatusService.CheckStatusDataBase();
            _ = LoadCaseTypeAsync();
        }

        #region Properties

        [ObservableProperty]
        public CaseTypeEntity selectedCaseType = null!;

        [ObservableProperty]
        public IEnumerable<CaseTypeEntity> casetypes;

        [ObservableProperty]
        public string tb_FirstName = string.Empty;

        [ObservableProperty]
        public string tb_LastName = string.Empty;

        [ObservableProperty]
        public string tb_Email = string.Empty;

        [ObservableProperty]
        public string tb_PhoneNumber = string.Empty;

        [ObservableProperty]
        public string tb_Description = string.Empty;

        #endregion


        #region Methods

        // Hämtar alla ärendetyper
        private async Task LoadCaseTypeAsync()
        {
            Casetypes = await CaseTypeService.GetAllCaseTypes();
        }

        #endregion


        #region Buttons
        [RelayCommand]

        // Lägger till ett nytt ärende
        private async Task AddCase()
        {
            UserModel user = new UserModel();

            user.FirstName = Tb_FirstName;
            user.LastName = Tb_LastName;
            user.Email = Tb_Email;
            user.PhoneNumber = Tb_PhoneNumber;
            user.Description = Tb_Description;
            user.CaseType = SelectedCaseType;

            await CaseService.NewCase(user);

            Tb_FirstName = string.Empty;
            Tb_LastName = string.Empty;
            Tb_Email = string.Empty;
            Tb_PhoneNumber = string.Empty;
            Tb_Description = string.Empty;

            SelectedCaseType = null!;
        }

        #endregion

    }
}
