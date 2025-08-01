﻿namespace AniTrack.Data.Models
{

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserAnime> Animelist { get; set; } = new List<UserAnime>();
        public virtual ICollection<AnimeReview> Reviews { get; set; } = new List<AnimeReview>();
    }
}
