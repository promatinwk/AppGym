using System;
namespace GymApp.Models
{
	public class TrainingExercises
	{
		public int Id { get; set; }
		public int TrainingId { get; set; }
		public Training Training { get; set; }
		public int ExerciseId { get; set; }
		public Exercise Exercise { get; set; }
		public int SeriesCount { get; set; }
	}
}

