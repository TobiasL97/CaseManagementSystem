using CaseManagementSystem.Contexts;
using CaseManagementSystem.MVVM.Models.Entities;
using CaseManagementSystem.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Microsoft.IdentityModel.Tokens;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CaseManagementSystem.Services
{
    internal class CaseService
    {
        private static DataContext _context = new();

        #region Methods

        // Mappar om user till UserEntity och tilldelar UserEntity ett case om det inte redan finns ett
        public static async Task NewCase(UserModel user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            };

            var caseEntity = await _context.Cases.FirstOrDefaultAsync(x => x.CaseTypeId == user.CaseType.Id && x.StatusId == 1 && x.Description == user.Description);
            if (caseEntity != null)
            {
                caseEntity.StatusId = 1;
                caseEntity.CaseTypeId = user.CaseType.Id;
                userEntity.CaseId = caseEntity.Id;
            }
            else
            {
                userEntity.Case = new CaseEntity
                {

                    CaseTypeId = user.CaseType.Id,
                    StatusId = 1,
                    Description = user.Description,
                    Created = DateTime.Now,
                };

                _context.Add(userEntity);
                await _context.SaveChangesAsync();
            }
        }

        // Hämtar alla Ärenden där StatusId inte är 3/Avslutad
        public static async Task<ObservableCollection<CaseEntity>> GetAllCases()
        {
            var caseList = new ObservableCollection<CaseEntity>();
            foreach (var _case in await _context.Cases.Include(x => x.Status).Where(x => x.StatusId != 3).Include(x => x.CaseType).Include(x => x.User).ToListAsync())
            {
                caseList.Add(_case);
            }

            return caseList;
        }

        public static async Task<ObservableCollection<CaseEntity>> GetAllFinishedCases()
        {
            var caseList = new ObservableCollection<CaseEntity>();
            foreach (var _case in await _context.Cases.Include(x => x.Status).Where(x => x.StatusId == 3).Include(x => x.CaseType).Include(x => x.User).ToListAsync())
            {
                caseList.Add(_case);
            }

            return caseList;
        }

        // Ändrar statusId till 2/pågående om det finns ett valt ärende
        public static async Task StartCase(CaseEntity SelectedCase)
        {
            if (SelectedCase != null)
            {
                var _cases = await _context.Cases.Include(x => x.Status).Include(x => x.CaseType).Include(x => x.User).ToListAsync();
                foreach (var _case in _cases)
                {
                    if (_case.Id == SelectedCase.Id)
                    {
                        _case.StatusId = 2;

                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        public static async Task FinishCase(CaseEntity SelectedCase)
        {
            if (SelectedCase != null)
            {
                var _cases = await _context.Cases.Include(x => x.Status).Include(x => x.CaseType).Include(x => x.User).ToListAsync();
                foreach (var _case in _cases)
                {
                    if (_case.Id == SelectedCase.Id)
                    {
                        _case.StatusId = 3;

                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        // Hämtar den senaste datan från databasen till SelectedCase
        public static async Task<CaseEntity> FetchLatestSelectedCase(CaseEntity SelectedCase)
        {
            
            SelectedCase = await _context.Cases.Include(x => x.Status).Include(x => x.User).Include(x => x.CaseType).FirstOrDefaultAsync(x => x.Id == SelectedCase.Id);

            return SelectedCase;
        }

        

        #endregion

    }
}
