﻿using SkyDrive.Interfaces;

namespace SkyDrive.Entities
{
    public class EventEntity : IEntity
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public DateTime DateTimeOfEvent { get; set; }

        public InstructorEntity? Instructor { get; set; }
        public ICollection<MemberEntity>? Members { get; set; }
    }
}
