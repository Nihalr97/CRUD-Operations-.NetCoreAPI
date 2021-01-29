using Audree.Demo.Core.Contracts.IRepositories.Admin;
using Audree.Demo.Core.Contracts.IUnitOfWork;
using Audree.Demo.Core.Models.Admin;
using Audree.Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audree.Demo.Infrastructure.Repositories.Admin
{
    public class EmployRepository : IEmployRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly Plcontextclass _dbw;

        #region Constructor for Dependency Injection
        public EmployRepository(IUnitOfWork unitOfWork, Plcontextclass dbw)
        {

            _unitOfWork = unitOfWork;
            _dbw = dbw;
        }
        #endregion
        public async Task<string> CreateOrUpdateEmploy(Employ employs)
        {
            string Message = "";
            using (_unitOfWork)
            {
                using (var transaction = _dbw.Database.BeginTransaction())
                {
                    try
                    {
                        var userId = employs.id;
                        //var userId = 18;
                        if (employs.id == 0)
                        {


                            //employs.Date = DateTime.Now;
                            employs.Date = employs.Date;
                            employs.empname = employs.empname;
                            employs.empid = employs.empid;

                            await _dbw.employs.AddAsync(employs);

                            _dbw.SaveChanges();

                            Message = "created successfully";

                            //auditTraildata.ExecuteAudit("Role", role.Id, role.CreatedById, role.LoginUSerPlantId, role.RoleName, null, 0, (int)EnumDBActions.Create, "Role Name", role.Comments, role.LoginUSerPlant, role.LoginUSerName, _batchContext);
                        
                        }
                        else
                        {
                            var roleExist = await _dbw.employs.AsNoTracking().Where(w => w.id == employs.id).FirstOrDefaultAsync();
                            if (roleExist != null)
                            {

                                roleExist.empname = employs.empname;

                                //roleExist.Date = DateTime.Now;
                                roleExist.Date = employs.Date;

                                roleExist.empid = employs.empid;
                            }

                            _dbw.employs.Update(roleExist);
                            _dbw.SaveChanges();
                            Message = "created successfully";
                        }
                         transaction.Commit();

                        return Message;
                    }
                    catch (Exception Ex)
                    {
                        Message = "issue";
                        // Message = "TransactionFail";
                        transaction.Rollback();
                        return Message;
                    }
                }
                return Message;
            }
        }

        public async Task<string> DeleteDepartment(Employ employs)
        {
            string Message = "";
            Employ emp = await _dbw.employs.Where(a => a.id == employs.id).FirstOrDefaultAsync();
            _dbw.employs.Remove(emp); await _dbw.SaveChangesAsync();
            Message = "deleted successfully";
            return Message;
        }
    

        public async Task<List<Employ>> GetEmployDropdown()
        {
            using (_unitOfWork)
            { 
                var result = await _dbw.employs.ToListAsync();
                return result;
            }
        }
      

        public async Task<List<Employ>> GetEmploy()
        {
            try
            {
                using (_unitOfWork)
                {
                    var result = await _dbw.employs.OrderBy(x => x.id).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<Employ> GetById(int? id)
        {
            using (_unitOfWork)
            {
                return await _dbw.employs.FirstOrDefaultAsync(x => x.id == id);
            }
        }

        //public async Task<Employ> Update(Employ employs)
        //{
        //    _dbw.Entry(employs).State = EntityState.Modified;
        //    await _dbw.SaveChangesAsync();
        //    return employs;
        //}
    }
}
