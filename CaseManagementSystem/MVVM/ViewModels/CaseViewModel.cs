using CaseManagementSystem.Contexts;
using CaseManagementSystem.MVVM.Models.Entities;
using CaseManagementSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.MVVM.ViewModels
{
    partial class CaseViewModel : ObservableObject
    {
        private static DataContext _context = new();

        public CaseViewModel()
        {
            _ = LoadCasesAsync();

        }

        #region Properties

        [ObservableProperty]
        public ObservableCollection<CaseEntity> cases = null!;

        [ObservableProperty]
        public ObservableCollection<CommentEntity> comments = null!;

        [ObservableProperty]
        public bool listViewVisible = false;

        [ObservableProperty]
        public bool addButtonVisible = false;

        [ObservableProperty]
        public bool commentFieldVisible = false;

        [ObservableProperty]
        public bool deleteButtonVisible = false;

        [ObservableProperty]
        public bool submitButtonVisible = false;

        [ObservableProperty]
        public bool emptySubmitTextVisible = false;

        [ObservableProperty]
        public bool finishCaseButtonVisible = false;

        [ObservableProperty]
        public bool startCaseButtonVisible = false;

        [ObservableProperty]
        public string tb_Comment = string.Empty;

        [ObservableProperty]
        public string tb_EmptySubmitText = string.Empty;

        private CaseEntity _selectedCase = null!;
        public CaseEntity SelectedCase
        {
            get { return _selectedCase; }
            set
            {
                _selectedCase = value;
                OnPropertyChanged(nameof(SelectedCase));
                ListViewVisible = true;
                SubmitButtonVisible = true;
                CommentFieldVisible = true;
                FinishCaseButtonVisible = false;
                StartCaseButtonVisible = true;
                _ = LoadCommentsAsync();
                CaseButtonsVisibility();
            }
        }

        private CommentEntity _selectedComment = null!;
        public CommentEntity SelectedComment
        {
            get { return _selectedComment; }
            set
            {
                _selectedComment = value;
                OnPropertyChanged(nameof(SelectedComment));
                DeleteButtonVisible = true;
                if(EmptySubmitTextVisible == true)
                {
                    EmptySubmitTextVisible = false;
                }
            }
        }

        #endregion


        #region Methods

        // Visar knappen för att avsluta ett ärende om ett ärende är valt och ärendet har statusId 2/pågående
        public void CaseButtonsVisibility()
        {
            if(SelectedCase != null && SelectedCase.StatusId == 2)
            {
                FinishCaseButtonVisible = true;
                StartCaseButtonVisible = false;
            }
        }

        // Hämtar alla Ärenden som inte är avslutade
        public async Task LoadCasesAsync()
        {
            Cases = await CaseService.GetAllCases();
        }

        // Hämtar alla kommentarer till det valda ärendet
        public async Task LoadCommentsAsync()
        {
           Comments = await CommentsService.GetComments(SelectedCase);
        }

        #endregion


        #region Buttons
        [RelayCommand]

        // Lägger till en kommentar till det valda ärendet om kommentarsfältet är ifyllt
        public async Task SubmitCommentAsync()
        {
            if (SelectedCase != null)
            {

                if (!Tb_Comment.IsNullOrEmpty())
                {
                    Comments = await CommentsService.GetComments(SelectedCase);
                    CommentsService.NewComment(Tb_Comment, SelectedCase, Comments);
                    Tb_Comment = string.Empty;
                    if(EmptySubmitTextVisible == true)
                    {
                        EmptySubmitTextVisible = false;
                    }
                }
                else
                {
                    EmptySubmitTextVisible = true;
                    Tb_EmptySubmitText = "Kan inte lägga till en tom kommentar!";
                }
            }
        }

        [RelayCommand]

        // Tar bort en kommentar från det valda ärendet
        public async Task DeleteCommentAsync()
        {
            if(SelectedComment != null)
            {
                Comments = await CommentsService.GetComments(SelectedCase);
                await CommentsService.DeleteComment(SelectedComment, Comments);

                DeleteButtonVisible = false;
            }

        }

        [RelayCommand]

        // Startar ett ärende
        public async Task StartCaseAsync()
        {
            await CaseService.StartCase(SelectedCase);
            SelectedCase = await CaseService.FetchLatestSelectedCase(SelectedCase);
        }

        [RelayCommand]
        public async Task FinishCaseAsync()
        {
            await CaseService.FinishCase(SelectedCase);
            SelectedCase = await CaseService.FetchLatestSelectedCase(SelectedCase);
            StartCaseButtonVisible = false;
        }

        #endregion

    }
}
