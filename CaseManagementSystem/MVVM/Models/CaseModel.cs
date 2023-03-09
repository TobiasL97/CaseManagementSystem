using CaseManagementSystem.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.MVVM.Models
{
    internal class CaseModel
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public CaseTypeModel CaseType { get; set; } = null!;
        public StatusModel Status { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Created { get; set; } = DateTime.Now;

        public CommentModel? Comments { get; set; }

    }
}
