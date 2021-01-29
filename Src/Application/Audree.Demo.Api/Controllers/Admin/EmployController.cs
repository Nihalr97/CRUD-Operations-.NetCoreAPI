using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audree.Demo.Core.Contracts.IRepositories.Admin;
using Audree.Demo.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Audree.Demo.Application.DTO.Admin;
using Audree.Demo.Core.Models.Admin;

namespace Audree.Demo.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployController : ControllerBase
    {
        #region Private Fields
        private IEmployRepository _EmployRepository;
        private IMapper _mapper;
        private readonly Plcontextclass _dbw;
        #endregion
        #region  Constructor for Dependency Injection 
        public EmployController(IEmployRepository empRepository, IMapper mapper, Plcontextclass dbw)
        {
            _EmployRepository = empRepository;
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
                var rle = await _EmployRepository.GetEmploy();
                var model = _mapper.Map<IEnumerable<EmployDTO>>(rle);
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
        public async Task<IActionResult> CreateOrUpdate([FromBody] EmployDTO employDTO)
        {
            //if (userid <= 0)
            //{
            //    return BadRequest(Tuple.Create(false, "User Not Exist"));
            //}
            return Ok(Tuple.Create(true, await _EmployRepository.CreateOrUpdateEmploy(_mapper.Map<Employ>(employDTO))));
        }
        #endregion
        #region Delete Method
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteDepartment([FromBody]EmployDTO employDTO)
        {
            return Ok(await _EmployRepository.DeleteDepartment(_mapper.Map<Employ>(employDTO)));
        }
        #endregion
        #region Get By Id Method
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var RoleName = await _EmployRepository.GetById(id);
            var RoleData = _mapper.Map<EmployDTO>(RoleName);
            if (RoleName == null)
            {
                return NotFound();
            }
            return Ok(RoleData);
        }
        #endregion
        //#region Put
        //[HttpPut]
        //public async Task<IActionResult> Put(int id, EmployDTO employDTO)
        //{
        //    return Ok(await _EmployRepository.Update(_mapper.Map<Employ>(employDTO)));
        //}
        //#endregion
    }
}