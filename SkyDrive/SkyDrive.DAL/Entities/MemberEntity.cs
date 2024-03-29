﻿namespace SkyDrive.DAL.Entities
{
    public class MemberEntity : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<EventEntity>? Events { get; set; }
    }
}
