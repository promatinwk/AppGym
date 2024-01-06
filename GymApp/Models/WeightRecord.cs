using System;
namespace GymApp.Models
{
	public class WeightRecord
	{
		public int Id { get; set; }
		public int TrainingSessionId { get; set; }
		public TrainingSession TrainingSession { get; set; }
		public int ExerciseId { get; set; }
		public Exercise Exercise { get; set; }
		public int SeriesCount { get; set; }
		public int Weight { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;

	}
}

