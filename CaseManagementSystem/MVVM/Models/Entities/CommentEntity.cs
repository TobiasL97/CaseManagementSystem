using System;
using System.ComponentModel.DataAnnotations;

namespace CaseManagementSystem.MVVM.Models.Entities
{
    internal class CommentEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Created { get; set; } = DateTime.Now;

        public string? Comment { get; set; }

        public int CaseId { get; set; }



        public CaseEntity Case { get; set; } = null!;
    }
}
