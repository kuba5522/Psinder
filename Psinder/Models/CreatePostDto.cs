using System.ComponentModel.DataAnnotations;

namespace Psinder.Models
{
    public class CreatePostDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł ogłoszenia")]
        public string Title { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Wiek")]
        public int Age { get; set; }
        [Display(Name = "Rozmiar")]
        public string Size { get; set; }
        [Display(Name = "Rasa")]
        public string Breed { get; set; }
        [Display(Name = "Trudność")]
        public string Difficulty { get; set; }
        [Display(Name = "Lokalizacja")]
        public string Location { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string? ImagePath { get; set; }

        [Display(Name = "Telefon kontaktowy")]
        public string ContactPhone { get; set; }
        [Display(Name = "Email kontaktowy")]
        public string ContactEmail { get; set; }
    }
}
