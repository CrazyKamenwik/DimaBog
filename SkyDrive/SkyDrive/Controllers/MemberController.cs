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
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService _service;
        private readonly IValidator<MemberViewModel> _validator;

        public MemberController(ILogger<MemberController> logger, IMemberService service, IValidator<MemberViewModel> validator)
        {
            _service = service;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IEnumerable<MemberViewModel>> GetAllMembers(CancellationToken cancellationToken)
        {
            var memberModels = await _service.GetAllMembers(cancellationToken);

            return memberModels.Adapt<IEnumerable<MemberViewModel>>();
        }

        [HttpGet("{id:int}")]
        public async Task<MemberViewModel> GetMemberById(int id, CancellationToken cancellationToken)
        {
            var memberModel = await _service.GetMemberById(id, cancellationToken);

            return memberModel.Adapt<MemberViewModel>();
        }

        [HttpPost]
        public async Task<MemberViewModel> CreateMember(MemberViewModel memberViewModel, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(memberViewModel, cancellationToken);

            var memberModel = memberViewModel.Adapt<MemberModel>();

            var memberViewModelResult = await _service.CreateMember(memberModel, cancellationToken);

            return memberViewModelResult.Adapt<MemberViewModel>();
        }

        [HttpPut]
        public async Task<MemberViewModel> UpdateMember(MemberViewModel memberViewModel, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(memberViewModel, cancellationToken);

            var memberModel = memberViewModel.Adapt<MemberModel>();

            var memberViewModelResult = await _service.UpdateMember(memberModel, cancellationToken);

            return memberViewModelResult.Adapt<MemberViewModel>();
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteMember(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteMember(id, cancellationToken);
        }
    }
}