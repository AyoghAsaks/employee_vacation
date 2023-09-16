using AutoMapper;
using EmployeeVacation.Data;
using EmployeeVacation.IRepositories;
using EmployeeVacation.Models;
using EmployeeVacation.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeVacation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        public LeaveTypesController(ILeaveTypeRepository leaveTypeRepository, IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        /*
        // GET: LeaveTypes
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LeaveTypeVM>>> Get()
        {
            var leaveTypes = await _leaveTypeRepository.GetAllAsync();
            if(leaveTypes.Count == 0) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<LeaveTypeVM>>(leaveTypes));
        }
        */

        /*
        // GET: LeaveTypes
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var leaveTypes = _mapper.Map<List<LeaveTypeVM>>(await _leaveTypeRepository.GetAllAsync());
            return Ok(leaveTypes);
        }
        */

        // GET: LeaveTypes
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var leaveTypes = await _leaveTypeRepository.GetAllAsync();

            if (leaveTypes.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<LeaveTypeVM>>(leaveTypes));
        }

        // GET LeaveTypes by id
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int? id)
        {
            var leaveType = await _leaveTypeRepository.GetAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            
            var leaveTypeVM = _mapper.Map<LeaveTypeVM>(leaveType);

            return Ok(leaveTypeVM);
        }

        // CREATE leaveType Object
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(LeaveTypeVM leaveTypeVM)
        {
            
            var leaveType = _mapper.Map<LeaveType>(leaveTypeVM);
            var result = await _leaveTypeRepository.AddAsync(leaveType);
            if (result != null)
            {
                var leaveTypeDetails = _mapper.Map<LeaveTypeVM>(leaveType);

                // Return 201
                //return Created("~api/LeaveTypes/Create", leaveTypeDetails);
                return StatusCode(StatusCodes.Status201Created, leaveTypeDetails);

            }
            return BadRequest();
            
            /*
            var leaveType = _mapper.Map<LeaveType>(leaveTypeVM);
            await _leaveTypeRepository.AddAsync(leaveType);
            return Ok(true);
            */

        }

        // EDIT leaveType Object
        [HttpPut("Edit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(LeaveTypeVM leaveTypeVM)
        {
            var leaveTypeModel = _mapper.Map<LeaveType>(leaveTypeVM);
            var result = await _leaveTypeRepository.UpdateAsync(leaveTypeModel);
            if (result != null)
            {
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE leaveType Object
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var leaveType = await _leaveTypeRepository.GetAsync(id);
            if (leaveType != null)
            {
                return NotFound();
            }
            else
            {
                await _leaveTypeRepository.DeleteAsync(leaveType.Id);
                return NoContent();
            }
        }
        
        [HttpPost("AllocateLeave")]
        public async Task<IActionResult> AllocateLeave(int id)
        {
            await _leaveAllocationRepository.LeaveAllocation(id);
            return StatusCode(201);
        }
        
        /*
        [HttpPost("AssignLeave")]
        public async Task<IActionResult> AssignLeave(string typeOfLeave)
        {
            await _leaveAllocationRepository.VacationAllocation(typeOfLeave);
            return StatusCode(201);
        }
        */

    }
}
