using System.ComponentModel.DataAnnotations;

namespace PostTapeApp.Models;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }

    [Display(Name = "Имя пользователя")]
    public User? User { get; set; }

    [Display(Name = "Дата публикации")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DatePublish { get; set; } = DateTime.Now;


    [Display(Name = "Сообщение")]
    public string Message { get; set; }

}
