using System.ComponentModel.DataAnnotations;

namespace PostTapeApp.Models;

public class User
{
    public int Id { get; set; }

    [Display(Name = "Имя")]
    public string Name { get; set; }
}
