namespace AniTrack.GCommon
{
    public static class ExceptionMessages
    {
        public const string InterfaceNotFound = "No interface found for {0}, it was not added to the Service Collection. Use I{0} !";

        public const string AnimeAddErrorMessage = "An error occurred while adding the anime. Please try again later.";
        public const string AnimeAddSuccessMessage = "New anime added successfully";
        public const string AnimeDetailsErrorMessage = "An error occurred while retrieving the anime details. Please try again later.";
        public const string AnimeEditErrorMessage = "An error occurred while trying to edit the anime. Please try again later.";
        public const string AnimeEditSuccessMessage = "Anime edited successfully.";
        public const string AnimeDeleteErrorMessage = "An error occurred while trying to delete the anime. Please try again later.";
        public const string AnimeDeleteSuccessMessage = "Anime deleted successfully.";
        public const string AnimeRestoreErrorMessage = "An error occurred while trying to restore the anime. Please try again later.";
        public const string AnimeRestoreSuccessMessage = "Anime restored successfully.";
        public const string AnimeSearchErrorMessage = "An error occurred while searching. Please try again later.";

        public const string ReviewUsersErrorMessage = "An error occured while trying to find the User's reviews. Please try again later";
        public const string ReviewWriteErrorMessage = "An error occurred while posting the review. Please try again later.";
        public const string ReviewWrongAuthorErrorMessage = "You can only edit reviews written by you!";
        public const string ReviewEditSuccessMessage = "Review edited successfully.";

        public const string AnimelistRetrieveErrorMessage = "An error occurred while retrieving the animelist. Please try again later.";
        public const string AnimelistAddErrorMessage = "An error occurred while adding the anime. Please try again later.";
        public const string AnimelistAddSuccessMessage = "Anime added to your animelist successfully.";
        public const string AnimelistRemoveErrorMessage = "An error occurred while removing the anime from your animelist. Please try again later.";
        public const string AnimelistRemoveSuccessMessage = "Anime removed from your animelist successfully.";

        public const string GenreAnimesRetrieveErrorMessage = "An error occurred while retrieving the animes for this genre. Please try again later.";
        public const string GenreAlreadyExistsErrorMessage = "This genre already exists.";
        public const string GenreAddErrorMessage = "An error occurred while trying to add the genre. Please try again later.";
        public const string GenreAddSuccessMessage = "The new genre was added successfully";

        public const string AdminGetAnimesForRestoreErrorMessage = "An error occurred while trying to retrieve the animes for restore. Please try again later.";
    }
}
