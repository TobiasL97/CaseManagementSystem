using CaseManagementSystem.Contexts;
using CaseManagementSystem.MVVM.Models;
using CaseManagementSystem.MVVM.Models.Entities;
using CaseManagementSystem.MVVM.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.Services
{
    internal class CommentsService
    {
        private static readonly DataContext _context = new();

        public static async void NewComment(string comment, CaseEntity selectedCase, ObservableCollection<CommentEntity> comments)
        {
            var _comment = new CommentEntity { Comment = comment, CaseId = selectedCase.Id };

            comments.Add(_comment);
            _context.Comments.Add(_comment);
            
            await _context.SaveChangesAsync();

        }

        public static async Task<ObservableCollection<CommentEntity>> GetComments(CaseEntity selectedcase)
        {
            var items = new ObservableCollection<CommentEntity>();
            foreach (var comment in await _context.Comments.Where(x => x.CaseId == selectedcase.Id).ToListAsync())
            {
                items.Add(comment);
            }

            return items;
        }

        public static async Task DeleteComment(CommentEntity selectedComment, ObservableCollection<CommentEntity> comments)
        {
            var _comments = comments;
            foreach (var comment in _comments.ToList())
            {
                if(comment.Id == selectedComment.Id)
                {
                    _comments.Remove(comment);
                    _context.Comments.Remove(comment);

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
