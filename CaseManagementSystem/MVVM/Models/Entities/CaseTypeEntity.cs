using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CaseManagementSystem.MVVM.Models.Entities;

namespace CaseManagementSystem.MVVM.Models
{
    internal class CaseTypeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string TypeOfCase { get; set; } = null!;


        ICollection<CaseEntity> Cases { get; set; } = null!;
    }
}
