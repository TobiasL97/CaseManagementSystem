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

        public CaseViewModel()
        {
            _ = LoadCasesAsync();
        }

        [ObservableProperty]
        public IEnumerable<CaseEntity> cases = null!;

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
                _ = LoadCommentsAsync();
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


        [ObservableProperty]
        public string tb_Comment = string.Empty;

        [ObservableProperty]
        public string tb_EmptySubmitText = string.Empty;

        public async Task LoadCasesAsync()
        {
            Cases = await CaseService.GetAllCases();
        }

        public async Task LoadCommentsAsync()
        {
           Comments = await CommentsService.GetComments(SelectedCase);
        }

        [RelayCommand]
        public async Task SubmitComment()
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
                    Tb_EmptySubmitText = "You must enter a comment to submit!";
                }
            }
        }

        [RelayCommand]
        public async Task DeleteComment()
        {
            if(SelectedComment != null)
            {
                Comments = await CommentsService.GetComments(SelectedCase);
                await CommentsService.DeleteComment(SelectedComment, Comments);
            }

        }
    }
}
