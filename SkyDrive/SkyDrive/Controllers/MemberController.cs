using Microsoft.AspNetCore.Mvc;
using SkyDrive.BLL.Interfaces;
using SkyDrive.DAL.Entities;

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
        public async Task<IEnumerable<MemberEntity>> GetAllMembers()
        {
            return await _service.GetAllMember();
        }

        [HttpGet("{id:int}")]
        public async Task<MemberEntity> GetMemberById(int id)
        {
            return await _service.GetMemberById(id);
        }

        [HttpPost]
        public async Task<MemberEntity> CreateMember(MemberEntity memberEntity)
        {
            return await _service.CreateMember(memberEntity);
        }

        [HttpPut]
        public async Task<MemberEntity> UpdateMember(MemberEntity memberEntity)
        {
            return await _service.UpdateMember(memberEntity);
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteMember(int id)
        {
            await _service.DeleteMember(id);
        }
    }
}