using System;
namespace GymApp.Models
{
	public class TrainingSession
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime SessionDate { get; set; }
		public int TrainingId { get; set; }
		public Training Training { get; set; }
	}
}

