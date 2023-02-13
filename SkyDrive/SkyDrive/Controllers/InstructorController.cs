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
        public async Task<IEnumerable<InstructorViewModel>> GetAllInstructors()
        {
            var instructorModels = await _service.GetAllInstructors();

            return instructorModels.Adapt<IEnumerable<InstructorViewModel>>();
        }

        [HttpGet("{id:int}")]
        public async Task<InstructorViewModel> GetInstructorById(int id)
        {
            var instructorModel = await _service.GetInstructorById(id);

            return instructorModel.Adapt<InstructorViewModel>();
        }

        [HttpPost]
        public async Task<InstructorViewModel> CreateInstructor(InstructorViewModel instructorViewModel)
        {
            await _validator.ValidateAndThrowAsync(instructorViewModel);

            var instructorModel = instructorViewModel.Adapt<InstructorModel>();

            var instructorViewModelResult = await _service.CreateInstructor(instructorModel);

            return instructorViewModelResult.Adapt<InstructorViewModel>();
        }

        [HttpPut]
        public async Task<InstructorViewModel> UpdateInstructor(InstructorViewModel instructorViewModel)
        {
            await _validator.ValidateAndThrowAsync(instructorViewModel);

            var instructorModel = instructorViewModel.Adapt<InstructorModel>();

            var instructorViewModelResult = await _service.UpdateInstructor(instructorModel);

            return instructorViewModelResult.Adapt<InstructorViewModel>();
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _service.DeleteInstructor(id);
        }
    }
}
