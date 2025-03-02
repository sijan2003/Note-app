using Microsoft.AspNetCore.Identity;
using NotesApp.Models;

namespace WebApplication2.Models
{
    public class User: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<MyNotes> Notes { get; set; }
    }

}
