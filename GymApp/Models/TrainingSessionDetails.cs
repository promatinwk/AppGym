using System;
namespace GymApp.Models
{
	public class TrainingSessionDetails
	{
		public int Id { get; set; }
		public int SessionId { get; set; }
		public TrainingSession TrainingSession { get; set; }
		public DateTime SessionDate { get; set; }
		public string TrainingName { get; set; }
		public List<TrainingExercises> TrainingExercises { get; set;}
	}
}

