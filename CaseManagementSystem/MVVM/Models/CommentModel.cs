using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.MVVM.Models
{
    internal class CommentModel
    {
        public DateTime? Created { get; set; } = DateTime.Now;

        public string? Comment { get; set; }
    }
}
