using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audree.Demo.Application.DTO;
using Audree.Demo.Core.Contracts.IRepositories.Admin;
using Audree.Demo.Core.Models;
using Audree.Demo.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audree.Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PLClubController : ControllerBase
    {
        #region Private Fields
        private IPlclubRepository _plclubRepository;
        private IMapper _mapper;
        private readonly Plcontextclass _dbw;
        #endregion
        #region  Constructor for Dependency Injection 
        public PLClubController(IPlclubRepository plRepository, IMapper mapper, Plcontextclass dbw)
        {
            _plclubRepository = plRepository;
            _mapper = mapper;
            _dbw = dbw;
        }
        #endregion

        #region Get Method
        /// <summary>
        /// This method is used get details from repository
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var rle = await _plclubRepository.GetPlClub();
                var model = _mapper.Map<IEnumerable<PLClubdto>>(rle);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.ToString());
            }

        }
        #endregion
        #region Create or Update Method
        /// <summary>
        /// This method is used to create or update a record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate([FromBody] PLClubdto plclubDTO)
        {
            //if (userid <= 0)
            //{
            //    return BadRequest(Tuple.Create(false, "User Not Exist"));
            //}
            return Ok(Tuple.Create(true, await _plclubRepository.CreateOrUpdatePlClub(_mapper.Map<PLClub>(plclubDTO))));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteDepartment([FromBody]PLClubdto plclubDTO)
        {
            return Ok(await _plclubRepository.DeleteDepartment(_mapper.Map<PLClub>(plclubDTO)));
        }
        #endregion
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] PLClub obj)
        //{
        //    var data = _dbw.pLClubs.Update(obj);
        //    _dbw.SaveChanges();
        //    return Ok();
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var data = _dbw.pLClubs.Where(a => a.id == id).FirstOrDefault();
        //    _dbw.pLClubs.Remove(data);
        //    _dbw.SaveChanges();
        //    return Ok();
        //}
       // #region Put
        //[HttpPut]
        //public async Task<IActionResult> Put(int id, PLClubdto plclubDTO)
        //{
        //    return Ok(await _plclubRepository.Update(_mapper.Map<PLClub>(plclubDTO)));
        //}
        //#endregion
    }
}