using Microsoft.AspNetCore.Mvc;
using SkyDrive.Entities;
using SkyDrive.Exceptions;

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

        [HttpGet("{id:int}")]
        public MemberEntity? GetById(int id)
        {
            var entity = _context.Members.Find(id);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Member with id: {id} not found");
            }

            return _context.Members.Find(id);
        }

        [HttpPost]
        public MemberEntity Post(MemberEntity memberEntity)
        {
            _context.Members.Add(memberEntity);
            _context.SaveChanges();

            return memberEntity;
        }

        [HttpPut]
        public MemberEntity Put(MemberEntity memberEntity)
        {
            var entity = _context.Members.Find(memberEntity.Id);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Member with id: {memberEntity.Id} not found");
            }

            _context.Members.Update(memberEntity);
            _context.SaveChanges();

            return memberEntity;
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            var entity = _context.Members.Find(id);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Member with id: {id} not found");
            }

            _context.Members.Remove(entity);
            _context.SaveChanges();
        }
    }
}