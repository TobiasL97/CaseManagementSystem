using CaseManagementSystem.Contexts;
using CaseManagementSystem.MVVM.Models;
using CaseManagementSystem.MVVM.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementSystem.Services
{
    internal class UserService
    {
        private static DataContext _context = new();

        public static async Task SaveUser(UserModel user)
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
    }
}
