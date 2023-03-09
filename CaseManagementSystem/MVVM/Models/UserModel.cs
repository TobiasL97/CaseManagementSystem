using CaseManagementSystem.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.MVVM.Models
{
    internal class UserModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; } = null!;

        public string Description { get; set; } = null!;

        public CaseTypeEntity CaseType { get; set; } = null!;

        public StatusEntity status { get; set; } = new StatusEntity();

    }
}
