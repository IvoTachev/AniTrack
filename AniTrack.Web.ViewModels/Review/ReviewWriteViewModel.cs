namespace AniTrack.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;
    using static AniTrack.Common.EntityConstants.AnimeReview;
    using static AniTrack.Web.ViewModels.ValidationMessages.AnimeReview;
    public class ReviewWriteViewModel
    {
        public int AnimeId { get; set; }
        public string? AnimeTitle { get; set; }

        [Required(ErrorMessage = ContentRequiredMessage)]
        [MinLength(ReviewContentMinLength, ErrorMessage = ContentMinLengthMessage)]
        [MaxLength(ReviewContentMaxLength, ErrorMessage = ContentMaxLengthMessage)]
        public string Content { get; set; } = null!;

        public string? AnimeImageUrl { get; set; }
        [Required(ErrorMessage = RecommendRequiredMessage)]
        public bool? isAnimeRecommended { get; set; }
    }
}
