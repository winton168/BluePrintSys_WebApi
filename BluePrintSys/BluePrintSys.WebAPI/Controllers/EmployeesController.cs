using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using BluePrintSys.WebAPI.Models;
using BluePrintSys.DataAccess;
using BluePrintSys.Core.IRepository;
using BluePrintSys.Core.Repository;



namespace BluePrintSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(IUnitOfWork unitOfWork, ILogger<EmployeesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<EmployeeDTO> _retUserList = null;
            try
            {

                var _employees = await _unitOfWork.Employees.GetAll(null, null, new List<string>() { "Position" });

                _retUserList = new List<EmployeeDTO>();

                foreach (var userItem in _employees)
                {
                    EmployeeDTO _retUser = new EmployeeDTO();


                    _retUser.EmployeeId = userItem.EmployeeId;

                    _retUser.EmployeeGuid = _retUser.EmployeeGuid;

                    _retUser.FullName = userItem.FullName;

                    _retUser.Address = userItem.Address;

                    _retUser.PhoneNumber = userItem.PhoneNumber;

                    _retUser.PositionId = userItem.PositionId;

                    _retUser.PositionName = string.Empty;

                    if (userItem.Position != null)
                    {
                        _retUser.PositionName = userItem.Position.PositionName;
                    }

                    if ( ! string.IsNullOrEmpty(userItem.Comment))
                    {
                        _retUser.Comment = userItem.Comment;
                    }
                    else
                    {
                        _retUser.Comment = string.Empty;
                    }

                    _retUserList.Add(_retUser);
                }

                return Ok( new { ActionResult = 1, ActionMessage = "Get Data List Sucessfully",  DataList = _retUserList } );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetAll)}");
                return StatusCode(500,  new { ActionResult = 0, ActionMessage = " Internal Server Error. Please Try Again Later." });
            }
        }



        [HttpPost("InsertEmployee")]
        public async Task<IActionResult> InsertEmployee(EmployeeInsertDTO employeeDTO )
        {
          
            try
            {
                if ( ! ModelState.IsValid )
                {
                    return StatusCode(500, "Model State is invalid. Please Try Again Later.");
                }

                Employee _newItem = new Employee();

                _newItem.EmployeeGuid = Guid.NewGuid().ToString();

                _newItem.FullName = employeeDTO.FullName;

                _newItem.Address = employeeDTO.Address;

                _newItem.PhoneNumber = employeeDTO.PhoneNumber;

                _newItem.PositionId = employeeDTO.PositionId;

                _newItem.DateCreated = DateTime.Now;

                _newItem.DateUpdated = DateTime.Now;

                await _unitOfWork.Employees.Insert(_newItem);

                await _unitOfWork.Save();
        
             
               return Ok(new { ActionResult = 1, ActionMessage = "New Employee Saved Successfully", Data = _newItem });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(InsertEmployee)}");
              
                return StatusCode(500, new {ActionResult = 0, ActionMessage = "Internal Server Error. Please Try Again Later." });
            }
        }



        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult>GetEmployeeById(int id)
        {

            try
            {

                Employee _savedItem = await _unitOfWork.Employees.GetById((object)id);

                if (_savedItem == null)
                {
                    return StatusCode(404, "Employee not found in our database system .");
                }

                EmployeeDTO _retUser = new EmployeeDTO();


                _retUser.EmployeeId = _savedItem.EmployeeId;

                _retUser.EmployeeGuid = _retUser.EmployeeGuid;

                _retUser.FullName = _savedItem.FullName;

                _retUser.Address = _savedItem.Address;

                _retUser.PhoneNumber = _savedItem.PhoneNumber;

                _retUser.PositionId = _savedItem.PositionId;

                _retUser.PositionName = string.Empty;

                if (_savedItem.Position != null)
                {
                    _retUser.PositionName = _savedItem.Position.PositionName;
                }

                if (!string.IsNullOrEmpty(_savedItem.Comment))
                {
                    _retUser.Comment = _savedItem.Comment;
                }
                else
                {
                    _retUser.Comment = string.Empty;
                }


                return Ok( new { ActionResult = 1, ActionMessage = "Get Employee Info Sucessfully", Data = _retUser}    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteEmployee)}");
                return StatusCode(500, new { ActionResult = 0, ActionMessage = "Internal Server Error. Please Try Again Later." });
            }

        }




        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdateDTO employeeDTO)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(500, "Model State is invalid. Please Try Again Later.");
                }


                Employee _savedItem = await _unitOfWork.Employees.GetById((object)employeeDTO.EmployeeId);

                if (_savedItem == null)
                {
                    return StatusCode(404, "Employee not found in our database system .");
                }

                _savedItem.FullName = employeeDTO.FullName;

                _savedItem.Address = employeeDTO.Address;

                _savedItem.PhoneNumber = employeeDTO.PhoneNumber;

                _savedItem.PositionId = employeeDTO.PositionId;


                _savedItem.DateUpdated = DateTime.Now;

                _unitOfWork.Employees.Update(_savedItem);

                await _unitOfWork.Save();

                return Ok(new { ActionResult = 0, ActionMessage = "Update User Successfully ", Data  = _savedItem });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateEmployee)}");
                return StatusCode(500, new { ActionResult = 0, ActionMessage = "Internal Server Error. Please Try Again Later." });
            }
        }


        [HttpPost("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(EmployeeDTO employeeDTO)
        {

            try
            {
               
                Employee _savedItem = await _unitOfWork.Employees.GetById((object)employeeDTO.EmployeeId);

                if (_savedItem == null )
                {
                    return StatusCode(404, "Employee not found in our database system .");
                }

               await _unitOfWork.Employees.Delete(employeeDTO.EmployeeId);

                await _unitOfWork.Save();


                return Ok(new { ActionResult = 1,  ActionMessage = "Emplolyee Delete Successfully ." });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteEmployee)}");
                return StatusCode(500, new { ActionResult = 0, ActionMessage = "Internal Server Error. Please Try Again Later." });
            }
        }






    }


}
