using CaseManagementSystem.Contexts;
using CaseManagementSystem.MVVM.Models;
using CaseManagementSystem.MVVM.Models.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.Services
{
    internal class CaseTypeService
    {
        private static DataContext _context = new();

        #region Methods

        // Hämtar alla ärendetyper från CaseTypes-databasen
        public static async Task<IEnumerable<CaseTypeEntity>> GetAllCaseTypes()
        {
            return await _context.CaseTypes.ToListAsync();
        }

        // Kollar om databasen är tom, är databasen tom så skapas fyra ärendetyper
        public static async Task CheckCaseTypeDataBase()
        {
            var caseTypes = _context.CaseTypes;
            if (caseTypes.IsNullOrEmpty())
            {
                var CaseTypeList = new List<CaseTypeEntity>
                {
                    new CaseTypeEntity { TypeOfCase = "Reparation" },
                    new CaseTypeEntity { TypeOfCase = "Service" },
                    new CaseTypeEntity { TypeOfCase = "Felsökning" },
                    new CaseTypeEntity { TypeOfCase = "Annat" },
                    
                };

                await caseTypes.AddRangeAsync(CaseTypeList);

            }

            await _context.SaveChangesAsync();
        }

        #endregion

    }
}
