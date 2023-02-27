using SkyDrive.DAL.Entities;

namespace SkyDrive.Tests.Initialize
{
    public static class InitializeData
    {
        public static List<MemberEntity> GetMembers()
        {
            return new List<MemberEntity>
            {
                new()
                {
                    Id = 1,
                    FirstName = "FirsNameTest1",
                    MiddleName = "MiddleNameTest1",
                    LastName = "LastNameTest1",
                    Events = null
                },
                new ()
                {
                    Id = 2,
                    FirstName = "FirsNameTest2",
                    MiddleName = "MiddleNameTest2",
                    LastName = "LastNameTest2",
                    Events = null
                },
                new ()
                {
                    Id = 3,
                    FirstName = "FirsNameTest3",
                    MiddleName = "MiddleNameTest3",
                    LastName = "LastNameTest3",
                    Events = null
                },
                new ()
                {
                    Id = 4,
                    FirstName = "FirsNameTest4",
                    MiddleName = "MiddleNameTest4",
                    LastName = "LastNameTest4",
                    Events = null
                },
                new ()
                {
                    Id = 5,
                    FirstName = "FirsNameTest5",
                    MiddleName = "MiddleNameTest5",
                    LastName = "LastNameTest5",
                    Events = null
                },
                new ()
                {
                    Id = 6,
                    FirstName = "FirsNameTest6",
                    MiddleName = "MiddleNameTest6",
                    LastName = "LastNameTest6",
                    Events = null
                },
                new ()
                {
                    Id = 7,
                    FirstName = "FirsNameTest7",
                    MiddleName = "MiddleNameTest7",
                    LastName = "LastNameTest7",
                    Events = null
                },
                new ()
                {
                    Id = 8,
                    FirstName = "FirsNameTest8",
                    MiddleName = "MiddleNameTest8",
                    LastName = "LastNameTest8",
                    Events = null
                },
                new ()
                {
                    Id = 9,
                    FirstName = "FirsNameTest9",
                    MiddleName = "MiddleNameTest9",
                    LastName = "LastNameTest9",
                    Events = null
                }
            };
        }

        public static List<InstructorEntity> GetInstructors()
        {
            return new List<InstructorEntity>
            {
                new()
                {
                    Id = 1,
                    FirstName = "FirsNameTest1",
                    MiddleName = "MiddleNameTest1",
                    LastName = "LastNameTest1",
                    Experience = 1,
                    Events = null
                },
                new()
                {
                    Id = 2,
                    FirstName = "FirsNameTest2",
                    MiddleName = "MiddleNameTest2",
                    LastName = "LastNameTest2",
                    Experience = 2,
                    Events = null
                },
                new()
                {
                    Id = 3,
                    FirstName = "FirsNameTest3",
                    MiddleName = "MiddleNameTest3",
                    LastName = "LastNameTest3",
                    Experience = 3,
                    Events = null
                },
                new()
                {
                    Id = 4,
                    FirstName = "FirsNameTest4",
                    MiddleName = "MiddleNameTest4",
                    LastName = "LastNameTest4",
                    Experience = 4,
                    Events = null
                },
                new()
                {
                    Id = 5,
                    FirstName = "FirsNameTest5",
                    MiddleName = "MiddleNameTest5",
                    LastName = "LastNameTest5",
                    Experience = 5,
                    Events = null
                },
                new()
                {
                    Id = 6,
                    FirstName = "FirsNameTest6",
                    MiddleName = "MiddleNameTest6",
                    LastName = "LastNameTest6",
                    Experience = 6,
                    Events = null
                },
                new()
                {
                    Id = 7,
                    FirstName = "FirsNameTest7",
                    MiddleName = "MiddleNameTest7",
                    LastName = "LastNameTest7",
                    Experience = 7,
                    Events = null
                },
                new()
                {
                    Id = 8,
                    FirstName = "FirsNameTest8",
                    MiddleName = "MiddleNameTest8",
                    LastName = "LastNameTest8",
                    Experience = 8,
                    Events = null
                },
                new()
                {
                    Id = 9,
                    FirstName = "FirsNameTest9",
                    MiddleName = "MiddleNameTest9",
                    LastName = "LastNameTest9",
                    Experience = 9,
                    Events = null
                }
            };
        }

        public static List<EventEntity> GetEvents()
        {
            return new List<EventEntity>
            {
                new()
                {
                    Id = 1,
                    DateTimeOfEvent= DateTime.Now,
                    InstructorId = 1,
                    Members = null,
                    Instructor = null
                },
                new()
                {
                    Id = 2,
                    DateTimeOfEvent= DateTime.Now,
                    InstructorId = 2,
                    Members = null,
                    Instructor = null
                },
                new()
                {
                    Id = 3,
                    DateTimeOfEvent= DateTime.Now,
                    InstructorId = 3,
                    Members = null,
                    Instructor = null
                },
                new()
                {
                    Id = 4,
                    DateTimeOfEvent= DateTime.Now,
                    InstructorId = 4,
                    Members = null,
                    Instructor = null
                },
                new()
                {
                    Id = 5,
                    DateTimeOfEvent= DateTime.Now,
                    InstructorId = 5,
                    Members = null,
                    Instructor = null
                },
                new()
                {
                    Id = 6,
                    DateTimeOfEvent= DateTime.Now,
                    InstructorId = 6,
                    Members = null,
                    Instructor = null
                },
                new()
                {
                    Id = 7,
                    DateTimeOfEvent= DateTime.Now,
                    InstructorId = 7,
                    Members = null,
                    Instructor = null
                },
                new()
                {
                    Id = 8,
                    DateTimeOfEvent= DateTime.Now,
                    InstructorId = 8,
                    Members = null,
                    Instructor = null
                },
                new()
                {
                    Id = 9,
                    DateTimeOfEvent= DateTime.Now,
                    InstructorId = 9,
                    Members = null,
                    Instructor = null
                },
            };
        }
    }
}
