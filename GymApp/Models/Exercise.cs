using System;
namespace GymApp.Models
{
	public class Exercise
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int BodyPartId { get; set; }
        public BodyPart BodyPart { get; set; }
        public string? RecordHolderId { get; set; }
        public User RecordHolder { get; set; }
    }
}

