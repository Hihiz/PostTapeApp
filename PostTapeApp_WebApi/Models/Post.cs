using System.ComponentModel.DataAnnotations;

namespace PostTapeApp_WebApi.Models;


public class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [Display(Name = "Пользователь")]
    public User? User { get; set; } = null!;

    [Display(Name = "Дата публикации")]
    [Required]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DatePublish { get; set; } = DateTime.Now;

    [Display(Name = "Сообщение")]
    [Required]
    public string Message { get; set; } = null!;
}
