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
    public class InstructorController : ControllerBase
    {
        private readonly ILogger<InstructorController> _logger;
        private readonly IInstructorService _service;
        private readonly IValidator<InstructorViewModel> _validator;

        public InstructorController(ILogger<InstructorController> logger, IInstructorService service, IValidator<InstructorViewModel> validator)
        {
            _service = service;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IEnumerable<InstructorViewModel>> GetAllInstructors(CancellationToken cancellationToken)
        {
            var instructorModels = await _service.GetAllInstructors(cancellationToken);

            return instructorModels.Adapt<IEnumerable<InstructorViewModel>>();
        }

        [HttpGet("{id:int}")]
        public async Task<InstructorViewModel> GetInstructorById(int id, CancellationToken cancellationToken)
        {
            var instructorModel = await _service.GetInstructorById(id, cancellationToken);

            return instructorModel.Adapt<InstructorViewModel>();
        }

        [HttpPost]
        public async Task<InstructorViewModel> CreateInstructor(InstructorViewModel instructorViewModel, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(instructorViewModel, cancellationToken);

            var instructorModel = instructorViewModel.Adapt<InstructorModel>();

            var instructorViewModelResult = await _service.CreateInstructor(instructorModel, cancellationToken);

            return instructorViewModelResult.Adapt<InstructorViewModel>();
        }

        [HttpPut]
        public async Task<InstructorViewModel> UpdateInstructor(InstructorViewModel instructorViewModel, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(instructorViewModel, cancellationToken);

            var instructorModel = instructorViewModel.Adapt<InstructorModel>();

            var instructorViewModelResult = await _service.UpdateInstructor(instructorModel, cancellationToken);

            return instructorViewModelResult.Adapt<InstructorViewModel>();
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteInstructor(id, cancellationToken);
        }
    }
}
