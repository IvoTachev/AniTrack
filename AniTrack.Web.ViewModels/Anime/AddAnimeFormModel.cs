namespace AniTrack.Web.ViewModels.Anime
{
    using System.ComponentModel.DataAnnotations;
    using static AniTrack.Web.ViewModels.ValidationMessages.Anime;
    using static Common.EntityConstants.Anime;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using AniTrack.Data.Models;

    public class AddAnimeFormModel  
    {
        // Id is an unique index in the database, so it is not required to be set by the user.
        public string Id { get; set; }
            = string.Empty;

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthMessage)]
        public string Title { get; set; } = null!;


        [Required(ErrorMessage = EpisodesRequiredMessage)]
        [Range(EpisodesMin, EpisodesMax, ErrorMessage = EpisodesRangeMessage)]
        public int Episodes { get; set; }

        public List<int> SelectedGenreIds { get; set; } = new List<int>();

        [Required(ErrorMessage = AirDateRequiredMessage)]
        public string AirDate { get; set; } = null!;

        [EndDateAfterAirDate]
        public string? EndDate { get; set; }

        [Required(ErrorMessage = SynopsisRequiredMessage)]
        [MinLength(SynopsisMinLength, ErrorMessage = SynopsisMinLengthMessage)]
        [MaxLength(SynopsisMaxLength, ErrorMessage = SynopsisMaxLengthMessage)]
        public string Synopsis { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength, ErrorMessage = ImageUrlMaxLengthMessage)]
        [Required(ErrorMessage = ImageUrlRequiredMessage)]
        public string ImageUrl { get; set; } = null!;
    }


}