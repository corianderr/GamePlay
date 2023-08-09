using System.ComponentModel.DataAnnotations;

namespace GamePlay.Domain.Models.Collection;

public class CreateCollectionModel
{
    [Required(ErrorMessage = "Please enter collection name using less than 50 characters.")]
    [StringLength(50)]
    public string? Name { get; set; }
    public string? UserId { get; set; }
    public bool IsDefault { get; set; }
}