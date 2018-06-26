using SpotifyAPI.Local.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamspeakMusicBot_v3
{
    class Playlist
    {
        public static Queue<String> currentPlaylist = new Queue<string>();
        public static Queue<String> userQueue = new Queue<string>();

        /// <summary>
        /// Plays the next song in the queue add by user using !add
        /// </summary>
        public static void PlayNextSong()
        {
            StatusResponse status = MainForm.spotify.GetStatus();

            if (userQueue.Count > 0)
            {
                MainForm.spotify.PlayURL(userQueue.Dequeue());
                
                MainForm.currentTrack = status.Track;
                return;
            }

            if(currentPlaylist.Count > 0)
            {

                MainForm.spotify.PlayURL(currentPlaylist.Dequeue());

                MainForm.currentTrack = status.Track;
                return;
            }

                CreateBackgroundPlaylist();

        }


        /// <summary>
        /// Changes the playlist
        /// </summary>
        public static void ChangeCurrentPlaylist(List<String> newSongs)
        {
            if(newSongs.Count > 0)
            {
                currentPlaylist.Clear();
                newSongs = newSongs.OrderBy(a => Guid.NewGuid()).ToList();

                for (int i = 0; i < newSongs.Count; i++)
                {
                    currentPlaylist.Enqueue(newSongs[i]);
                }

            }

        }

        /// <summary>
        /// Creates a playlist out off all playlists from text file
        /// </summary>
        public static void CreateBackgroundPlaylist()
        {
            List<String> songs = new List<string>();

            for (int i = 0; i < MainForm.playlists.Count; i++)
            {
                List<String> temp = WebApi.GetTracksFromPlaylist(MainForm.playlists[i]);

                for (int j = 0; j < temp.Count; j++)
                {
                    songs.Add(temp[j]);
                }
            }

            ChangeCurrentPlaylist(songs);
        }


        /// <summary>
        /// Return the currently playing track title
        /// </summary>
        /// <returns></returns>
        public static String GetTrackName()
        {
            StatusResponse status = MainForm.spotify.GetStatus();
            return status.Track.TrackResource?.Name;

        }


        /// <summary>
        /// Returns the currently playing artist title
        /// </summary>
        /// <returns></returns>
        public static String GetTrackArtist()
        {
            StatusResponse status = MainForm.spotify.GetStatus();
            return status.Track.ArtistResource?.Name;

        }


        /// <summary>
        /// Checks if spotify is playing
        /// </summary>
        /// <returns></returns>
        public static bool CheckPlaying()
        {
            return MainForm.spotify.GetStatus().Playing;
            
        }


    }
}
