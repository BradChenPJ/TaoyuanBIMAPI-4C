﻿namespace TaoyuanBIMAPI.Parameter
{
    public class BookmarkParameter
    {
        public string? UserId { get; set; }

        public string? BookmarkName { get; set; }

        public decimal? Scale { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Heading { get; set; }

        public decimal? Tilt { get; set; }

        public string? Note { get; set; }

        public DateTime? CreateTime { get; set; }

        public string? Layer { get; set; }

        public string? Dimention { get; set; }

        public string? Base64img { get; set; }

        public string? DimensionMark { get; set; }

        public string? Slice { get; set; }
    }
    public class UserBookmarkParameter
    {
        public string UserId { get; set; }
    }
}
