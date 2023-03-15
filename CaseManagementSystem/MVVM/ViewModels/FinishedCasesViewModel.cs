using CaseManagementSystem.MVVM.Models.Entities;
using CaseManagementSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.MVVM.ViewModels
{
    partial class FinishedCasesViewModel : ObservableObject
    {
        public FinishedCasesViewModel()
        {
            _ = LoadFinishedCasesAsync();
        }

        #region Properties

        [ObservableProperty]
        public ObservableCollection<CaseEntity> finishedCases = null!;

        #endregion


        #region Methods

        // Hämtar alla färdiga ärenden
        public async Task LoadFinishedCasesAsync()
        {
            FinishedCases = await CaseService.GetAllFinishedCases();
        }
        #endregion
    }
}
