using Audree.Demo.Core.Contracts.IRepositories.Admin;
using Audree.Demo.Core.Contracts.IUnitOfWork;
using Audree.Demo.Core.Models;
using Audree.Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audree.Demo.Infrastructure.Repositories.Admin
{
    public class PlClubRepository : IPlclubRepository
    {

        #region Private Fields
        private readonly IUnitOfWork _unitOfWork;

        private readonly Plcontextclass _dbw;

        #endregion
        #region Constructor for Dependency Injection
        public PlClubRepository(IUnitOfWork unitOfWork, Plcontextclass dbw)
        {

            _unitOfWork = unitOfWork;
            _dbw = dbw;
        }
        #endregion

        public async Task<List<PLClub>> GetPlClub()
        {
            try
            {
                using (_unitOfWork)
                {
                    var result = await _dbw.pLClubs.OrderBy(x => x.id).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<PLClub>> GetPlclubDropdown()
        {
            using (_unitOfWork)
            {
                var result = await _dbw.pLClubs.ToListAsync();
                return result;
            }
        }

        

        public Task<PLClub> GetByIdPlClub(int? id)
        {
            throw new NotImplementedException();
        }


        public Task<string> AlreadyExistPlClub(PLClub pLClub)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateOrUpdatePlClub(PLClub pLClub)
        {
            string Message = "";
            using (_unitOfWork)
            {
                using (var transaction = _dbw.Database.BeginTransaction())
                {
                    try
                    {
                        var userId = pLClub.id;
                        //var userId = 18;
                        if (pLClub.id == 0)
                        {

                            //pLClub.Date = DateTime.Now;
                            pLClub.Date = pLClub.Date;

                            pLClub.plname = pLClub.plname;
                            pLClub.seasonrank =pLClub.seasonrank ;

                            await _dbw.pLClubs.AddAsync(pLClub);

                            _dbw.SaveChanges();

                            Message = "created successfully";

                            //auditTraildata.ExecuteAudit("Role", role.Id, role.CreatedById, role.LoginUSerPlantId, role.RoleName, null, 0, (int)EnumDBActions.Create, "Role Name", role.Comments, role.LoginUSerPlant, role.LoginUSerName, _batchContext);
                        }
                        else
                        {
                            var roleExist = await _dbw.pLClubs.AsNoTracking().Where(w => w.id == pLClub.id).FirstOrDefaultAsync();
                            if (roleExist != null)
                            {

                                roleExist.plname =pLClub.plname;
                                roleExist.Date = pLClub.Date;


                                //roleExist.Date = DateTime.Now;
                                roleExist.seasonrank = pLClub.seasonrank;
                            }

                            _dbw.pLClubs.Update(roleExist);
                            _dbw.SaveChanges();
                            Message = "created successfully";
                        }
                         transaction.Commit();

                        return Message;
                    }
                    catch (Exception Ex)
                    {
                        Message ="issue";
                        // Message = "TransactionFail";
                        transaction.Rollback();
                        return Message;
                    }
                }
                return Message;
            }
        }


        public async Task<string> DeleteDepartment(PLClub pLClub)
        {
            string Message = "";
            PLClub emp = await _dbw.pLClubs.Where(a => a.id == pLClub.id).FirstOrDefaultAsync();
            _dbw.pLClubs.Remove(emp); await _dbw.SaveChangesAsync();
            Message = "deleted successfully";
            return Message;
        }

        //public async Task<PLClub> Update(PLClub pLClub)
        //{
        //    _dbw.Entry(pLClub).State = EntityState.Modified;
        //    await _dbw.SaveChangesAsync();
        //    return pLClub;
        //}

    }
}
