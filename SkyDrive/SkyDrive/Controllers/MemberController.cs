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

        public MemberController(ILogger<MemberController> logger, IMemberService service)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<MemberViewModel>> GetAllMembers()
        {
            var memberModels = await _service.GetAllMembers();

            return memberModels.Adapt<IEnumerable<MemberViewModel>>();
        }

        [HttpGet("{id:int}")]
        public async Task<MemberViewModel> GetMemberById(int id)
        {
            var memberModel = await _service.GetMemberById(id);

            return memberModel.Adapt<MemberViewModel>();
        }

        [HttpPost]
        public async Task<MemberViewModel> CreateMember(MemberViewModel memberViewModel)
        {
            var memberModel = memberViewModel.Adapt<MemberModel>();

            var memberViewModelResult = await _service.CreateMember(memberModel);

            return memberViewModelResult.Adapt<MemberViewModel>();
        }

        [HttpPut]
        public async Task<MemberViewModel> UpdateMember(MemberViewModel memberViewModel)
        {
            var memberModel = memberViewModel.Adapt<MemberModel>();

            var memberViewModelResult = await _service.UpdateMember(memberModel);

            return memberViewModelResult.Adapt<MemberViewModel>();
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteMember(int id)
        {
            await _service.DeleteMember(id);
        }
    }
}