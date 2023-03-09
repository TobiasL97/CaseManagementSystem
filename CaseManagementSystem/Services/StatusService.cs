using CaseManagementSystem.Contexts;
using CaseManagementSystem.MVVM.Models;
using CaseManagementSystem.MVVM.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.Services
{
    internal class StatusService
    {
        private static DataContext _context = new();

        public static async Task<IEnumerable<StatusEntity>> GetAllStatus()
        {
            return await _context.Status.ToListAsync();
        }
    }
}
