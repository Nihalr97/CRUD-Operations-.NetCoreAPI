using Audree.Demo.Core.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Audree.Demo.Core.Contracts.IRepositories.Admin
{
   public interface IEmployRepository
    {
        Task<List<Employ>> GetEmploy();
        Task<List<Employ>> GetEmployDropdown();        
        Task<string> CreateOrUpdateEmploy(Employ employs);
        Task<string> DeleteDepartment(Employ employs);
        Task<Employ> GetById(int? id);
        //Task<Employ> Update(Employ employs);
    }
}
