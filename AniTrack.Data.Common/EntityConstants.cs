namespace AniTrack.Common
{
    public static class EntityConstants
    {
        public static class Anime
        {
            public const int TitleMaxLength = 200; // Max length for Anime titles

            public const int SynopsisMinLength = 10;  // Min length for Anime synopsis
            public const int SynopsisMaxLength = 2000; // Max length for Anime synopsis

            public const int ImageUrlMaxLength = 2083; // Max length for URLs  

            public const int EpisodesMin = 1;   // Minimum number of episodes for an anime series    
            public const int EpisodesMax = 5000; // Maximum number of episodes for an anime series               
        }
        public static class Genre
        {
            public const int GenreNameMaxLength = 50; // Max length for Genre names
        }

        public static class AnimeReview
        {
            public const int ReviewContentMinLength = 10; // Min length for review content
            public const int ReviewContentMaxLength = 2000; // Max length for review content
            public const string CreatedOnDefaultValue = "GETUTCDATE()"; // Default value for CreatedOn property
        }
    }
}
