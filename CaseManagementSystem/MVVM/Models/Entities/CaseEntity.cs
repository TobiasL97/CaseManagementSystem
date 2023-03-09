using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaseManagementSystem.MVVM.Models.Entities
{
    internal class CaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int CaseTypeId { get; set; }

        public int StatusId { get; set; }

        public string Description { get; set; } = null!;

        public DateTime Created { get; set; } = DateTime.Now;




        public UserEntity User { get; set; } = null!;

        public StatusEntity Status { get; set; } = null!;

        public CaseTypeEntity CaseType { get; set; } = null!;


        public ICollection<CommentEntity> Comments = null!;

    }
}
