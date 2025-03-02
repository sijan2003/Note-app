using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace YourNotes.viewmodel
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Color { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
       
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
