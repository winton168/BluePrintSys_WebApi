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
    public class PositionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PositionsController> _logger;
        public PositionsController(IUnitOfWork unitOfWork, ILogger<PositionsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<PositionDTO> _retPositionList = null;
            try
            {

                var _positions = await _unitOfWork.Positions.GetAll( c => c.IsActive == true);

                _retPositionList = new List<PositionDTO>();

                foreach (var item in _positions)
                {
                    PositionDTO _retPosition = new PositionDTO();

                    _retPosition.PositionId = item.PositionId;

                    _retPosition.PositionName = item.PositionName;


                    _retPositionList.Add(_retPosition);
                }

                return Ok(_retPositionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetAll)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }




    }


}
