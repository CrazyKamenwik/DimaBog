using Microsoft.AspNetCore.Mvc;
using SkyDrive.BLL.Entities;
using SkyDrive.BLL.Interfaces;

namespace SkyDrive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService _memberService;

        public MemberController(ILogger<MemberController> logger, IMemberService memberService)
        {
            _memberService = memberService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<MemberEntity>> GetAll()
        {
            return await _memberService.GetAllMemberEntities();
        }

        [HttpGet("{id:int}")]
        public async Task<MemberEntity?> GetById(int id)
        {
            var entity = await _memberService.GetMemberEntityById(id);

            return entity;
        }

        [HttpPost]
        public async Task<MemberEntity> Post(MemberEntity memberEntity)
        {
            var result = await _memberService.CreateMemberEntity(memberEntity);

            return result;
        }

        [HttpPut]
        public async Task<MemberEntity> Put(MemberEntity memberEntity)
        {
            var entity = await _memberService.UpdateMemberEntity(memberEntity);

            return entity;
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _memberService.DeleteMemberEntity(id);
        }


    }
}