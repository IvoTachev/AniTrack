namespace AniTrack.Web.ViewModels.Anime
{
    using System.ComponentModel.DataAnnotations;
    using static AniTrack.Web.ViewModels.ValidationMessages.Anime;
    using static Common.EntityConstants.Anime;
    using System.Collections.Generic;

    public class EditAnimeFormModel
    {
        // Id is required for the form to be submitted correctly.
        // It is not required to be set by the user, as it is set by the database.
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthMessage)]
        public string Title { get; set; } = null!;


        [Required(ErrorMessage = EpisodesRequiredMessage)]
        [Range(EpisodesMin, EpisodesMax, ErrorMessage = EpisodesRangeMessage)]
        public int Episodes { get; set; }

        // This property is used to display the genres in the edit form.
        // The user chooses from a checklist, no need of validation here.
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
