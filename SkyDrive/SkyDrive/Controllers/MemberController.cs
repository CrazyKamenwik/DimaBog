using Microsoft.AspNetCore.Mvc;
using SkyDrive.Entities;

namespace SkyDrive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly ApplicationContext _context;

        public MemberController(ILogger<MemberController> logger, ApplicationContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public List<MemberEntity> GetAll()
        {
            return _context.Members.ToList();
        }

        [HttpPost]
        public MemberEntity Post(MemberEntity memberEntity)
        {
            _context.Members.Add(memberEntity);
            _context.SaveChanges();

            return memberEntity;
        }
    }
}