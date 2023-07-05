using System.ComponentModel.DataAnnotations;

namespace PostTapeApp_WebApi.Models;

public class User
{
    public int Id { get; set; }

    [Display(Name = "Имя")]
    [Required]
    public string Name { get; set; } = null!;

}
