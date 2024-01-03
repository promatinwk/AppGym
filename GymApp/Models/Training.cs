using System;
namespace GymApp.Models
{
	public class Training
	{
		
		public int Id { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		public string Name { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public List<TrainingExercises> TrainingExercises { get; set; }

	}
}

