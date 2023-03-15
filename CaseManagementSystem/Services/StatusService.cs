using CaseManagementSystem.Contexts;
using CaseManagementSystem.MVVM.Models;
using CaseManagementSystem.MVVM.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        #region Methods

        // Hämtar alla statusar från status-databasen
        public static async Task<IEnumerable<StatusEntity>> GetAllStatus()
        {
            return await _context.Status.ToListAsync();
        }

        // Kollar om databasen innehar några värden, om inte så läggs det till tre värden
        public static async Task CheckStatusDataBase()
        {
            var status = _context.Status;
            if (status.IsNullOrEmpty())
            {
                var statusList = new List<StatusEntity>
                {
                    new StatusEntity { Status = "Ej Påbörjad" },
                    new StatusEntity { Status = "Pågående" },
                    new StatusEntity { Status = "Avslutad" },
                };

                await status.AddRangeAsync(statusList);

            }

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
