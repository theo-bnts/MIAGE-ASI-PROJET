using System.ComponentModel.DataAnnotations;

namespace PROJET.Model;

public class Activity
{
	[Key] public required int Id { get; set; }

	[Required] public required string Name { get; set; }

	[Required]
	[DataType(DataType.MultilineText)]
	public required string Description { get; set; }

	public required int ActivityGroupId { get; set; }
	public ActivityGroup? ActivityGroup { get; set; }
}