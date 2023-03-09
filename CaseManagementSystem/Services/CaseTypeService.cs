﻿using CaseManagementSystem.Contexts;
using CaseManagementSystem.MVVM.Models;
using CaseManagementSystem.MVVM.Models.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
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

        public static async Task<IEnumerable<CaseTypeEntity>> GetAllCaseTypes()
        {
            return await _context.CaseTypes.ToListAsync();
        }
    }
}
