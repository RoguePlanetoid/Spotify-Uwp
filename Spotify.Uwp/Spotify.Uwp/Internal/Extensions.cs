﻿using Spotify.NetStandard.Responses;
using Spotify.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.Uwp.Internal
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Large Image
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public static ImageViewModel GetLargeImage(this List<Image> images) =>
            Mapping.MapImage(images.OrderByDescending(o => o.Height * o.Width).FirstOrDefault());

        /// <summary>
        /// Medium Image
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public static ImageViewModel GetMediumImage(this List<Image> images)
        {
            var small = GetSmallImage(images);
            var large = GetLargeImage(images);
            var medium = Mapping.MapImage(images.FirstOrDefault
                (f => Mapping.MapImage(f) != small || Mapping.MapImage(f) != large));
            return medium ?? large;
        }

        /// <summary>
        /// Small Image
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public static ImageViewModel GetSmallImage(this List<Image> images) =>
            Mapping.MapImage(images.OrderBy(o => o.Height * o.Width).FirstOrDefault());

        /// <summary>
        /// Get Artist
        /// </summary>
        /// <param name="artists"></param>
        /// <returns>Artist</returns>
        public static Artist GetArtist(this List<Artist> artists) =>
            artists.FirstOrDefault();

        /// <summary>
        /// As TimeSpan
        /// </summary>
        /// <param name="value"></param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan AsTimeSpan(this long value) =>
            TimeSpan.FromMilliseconds(value);

        /// <summary>
        /// As TimeSpan String
        /// </summary>
        /// <param name="value"></param>
        /// <returns>TimeSpan String</returns>
        public static string AsTimeSpanString(this TimeSpan value) =>
            value.TotalHours > 1
            ? value.ToString(@"hh\:mm")
            : value.ToString(@"mm\:ss");

        /// <summary>
        /// Get Release Year
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetReleaseYear(this string value) =>
            value?.Substring(0, 4);

        /// <summary>
        /// Get Songs
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static string GetSongs(this int total) =>
             $"{total} {((total > 1) ? "songs" : "song")}";
    }
}