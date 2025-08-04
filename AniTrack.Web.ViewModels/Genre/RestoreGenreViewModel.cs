namespace AniTrack.Web.ViewModels.Genre
{
    using AniTrack.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static AniTrack.GCommon.ExceptionMessages;

    public class RestoreGenreViewModel
    {
        public List<Genre> Genres { get; set; } = new List<Genre>();

        [Required(ErrorMessage = GenreSelectAGenreErrorMessage)]
        public string? SelectedGenreName { get; set; }
    }
}