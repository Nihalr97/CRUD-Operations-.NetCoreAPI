using Audree.Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Audree.Demo.Core.Contracts.IRepositories.Admin
{
    public interface IPlclubRepository
    {
        Task<List<PLClub>> GetPlClub();
        Task<List<PLClub>> GetPlclubDropdown();
        Task<PLClub> GetByIdPlClub(int? id);

        Task<string> CreateOrUpdatePlClub(PLClub pLClub);
        Task<string> DeleteDepartment(PLClub pLClub);
        Task<string> AlreadyExistPlClub(PLClub pLClub);
        //Task<PLClub> Update(PLClub pLClub);
    }
}
