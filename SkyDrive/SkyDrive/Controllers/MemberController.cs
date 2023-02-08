using Microsoft.AspNetCore.Mvc;
using SkyDrive.DAL;
using SkyDrive.DAL.Entities;
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
            var entity = GetEntity(id);

            return entity;
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
            var entity = GetEntity(memberEntity.Id);

            _context.Members.Update(memberEntity);
            _context.SaveChanges();

            return memberEntity;
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            var entity = GetEntity(id);

            _context.Members.Remove(entity!);
            _context.SaveChanges();
        }

        private MemberEntity GetEntity(int id)
        {
            var entity = _context.Members.Find(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Member with id: {id} not found");
            }

            return entity;
        }
    }
}