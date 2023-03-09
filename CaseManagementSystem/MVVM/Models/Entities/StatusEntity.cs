using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseManagementSystem.MVVM.Models.Entities
{
    internal class StatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string Status { get; set; } = null!;


        ICollection<CaseEntity> Cases { get; set; } = null!;
    }
}
