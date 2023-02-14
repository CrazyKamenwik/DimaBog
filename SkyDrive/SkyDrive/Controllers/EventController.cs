using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using SkyDrive.BLL.Interfaces;
using SkyDrive.BLL.Models;
using SkyDrive.ViewModels;

namespace SkyDrive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService _service;
        private readonly IValidator<EventViewModel> _validator;

        public EventController(ILogger<EventController> logger, IEventService service, IValidator<EventViewModel> validator)
        {
            _service = service;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IEnumerable<EventViewModel>> GetAllEvents(CancellationToken cancellationToken)
        {
            var eventModels = await _service.GetAllEvents(cancellationToken);

            return eventModels.Adapt<IEnumerable<EventViewModel>>();
        }

        [HttpGet("{id:int}")]
        public async Task<EventViewModel> GetEventById(int id, CancellationToken cancellationToken)
        {
            var eventModel = await _service.GetEventById(id, cancellationToken);

            return eventModel.Adapt<EventViewModel>();
        }

        [HttpPost]
        public async Task<EventViewModel> CreateEvent(EventViewModel eventViewModel, CancellationToken cancellationToken)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(eventViewModel, cancellationToken);

                var eventModel = eventViewModel.Adapt<EventModel>();

                var eventViewModelResult = await _service.CreateEvent(eventModel, cancellationToken);

                return eventViewModelResult.Adapt<EventViewModel>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut]
        public async Task<EventViewModel> UpdateEvent(EventViewModel eventViewModel, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(eventViewModel, cancellationToken);

            var eventModel = eventViewModel.Adapt<EventModel>();

            var eventViewModelResult = await _service.UpdateEvent(eventModel, cancellationToken);

            return eventViewModelResult.Adapt<EventViewModel>();
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteEvent(id, cancellationToken);
        }
    }
}
