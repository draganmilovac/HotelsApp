using HotelsApp.Application.Dtos;
using HotelsApp.Application.Requests.Commands;
using HotelsApp.Application.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HotelsApp.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the list of all created hotels
        /// </summary>
        /// <returns>The list of all created hotels</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllHotels()
        {
            var response = await _mediator.Send(new GetHotelsQuery());
            return response.IsSuccess ? Ok(response) : NotFound();
        }

        /// <summary>
        /// Gets the hotel with specified identifier, if exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The hotel with specified identifier, if exists</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHotelById(long id)
        {
            var response = await _mediator.Send(new GetHotelQuery(id));
            return response.IsSuccess ? Ok(response) : NotFound();
        }

        /// <summary>
        /// Creates a new hotel, based on a specefied request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>New created hotel</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(HotelCreateCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDto request)
        {
            var response = await _mediator.Send(new HotelCreateCommand(request));
            return response.IsSuccess ? CreatedAtAction(nameof(GetHotelById),
                new { id = response.Result }, response.Result)
                : NotFound();
        }

        /// <summary>
        /// Delete hotel based on specified identifier, if exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHotel(long id)
        {
            var response = await _mediator.Send(new HotelDeleteCommand(id));
            return response.IsSuccess ? Ok(response) : NotFound();
        }

        /// <summary>
        /// Update hotel's values based on request and id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns>Updated hotel</returns>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateHotel(HotelDto request, long id)
        {
            var response = await _mediator.Send(new HotelUpdateCommand(request, id));
            return response.IsSuccess ? Ok(response) : NotFound();
        }
        #endregion
    }
}
