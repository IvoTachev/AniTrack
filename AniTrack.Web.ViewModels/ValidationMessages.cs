

using AniTrack.Web.ViewModels.Anime;
using System.ComponentModel.DataAnnotations;

namespace AniTrack.Web.ViewModels
{
    public static class ValidationMessages
    {
        public static class Anime
        {
            public const string TitleRequiredMessage = "Title is required.";
            public const string TitleMaxLengthMessage = "Title must be at most 200 characters long.";

            public const string EpisodesRequiredMessage = "Episodes is required.";
            public const string EpisodesRangeMessage = "Episodes must be between 1 and 5000 episodes.";

            public const string AirDateRequiredMessage = "Air date is required.";

            public const string SynopsisRequiredMessage = "Synopsis is required.";
            public const string SynopsisMinLengthMessage = "Synopsis must be at least 10 characters long.";
            public const string SynopsisMaxLengthMessage = "Synopsis must be at most 2000 characters long.";

            public const string ImageUrlRequiredMessage = "Image URL is required.";
            public const string ImageUrlMaxLengthMessage = "Image URL must be at most 2083 characters long.";

            public const string ServiceCreateError = "An error occurred while trying to create the anime. Please try again later.";
            public class EndDateAfterAirDateAttribute : ValidationAttribute
            {
                protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
                {
                    var model = (AddAnimeFormModel)validationContext.ObjectInstance;
                    if (string.IsNullOrWhiteSpace(model.EndDate))
                        return ValidationResult.Success; // Allow null/empty EndDate

                    if (DateTime.TryParse(model.AirDate, out var airDate) &&
                        DateTime.TryParse(model.EndDate, out var endDate))
                    {
                        if (endDate < airDate)
                        {
                            return new ValidationResult("End Date can not be before Air Date.");
                        }
                    }
                    // If parsing fails, let other validators handle it
                    return ValidationResult.Success;
                }
            }
        }
    }
}