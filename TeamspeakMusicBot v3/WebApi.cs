using SpotifyAPI;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TeamspeakMusicBot_v3
{
    class WebApi
    {
        /// <summary>
        /// Return the ID of the artist from search term
        /// </summary>
        public static String GetArtistFromSearchTerm(String term)
        {
            String id = null;
            try
            {
                SearchItem search = MainForm.web.SearchItems(term, SearchType.Artist, 1, 0, "gb");
                Paging<FullArtist> artist = search.Artists;
                List<FullArtist> list = artist.Items.ToList();
                id = list[0].Id;
            }
            catch
            {
                TextToSpeak.TTS_System("Invalid Term or No Results");
                return "n";
            }

            return id;
        }


        /// <summary>
        /// Return the ID of the album from search term
        /// </summary>
        public static String GetAlbumFromSearchTerm(String term)
        {
            String id = null;
            try
            {
                SearchItem search = MainForm.web.SearchItems(term, SearchType.Album, 1, 0, "gb");
               /* if(search.Error != null)
                    if(search.Error.Status == 401)
                        RunAuthentication(); */
                
                Paging<SimpleAlbum> album = search.Albums;
                List<SimpleAlbum> list = album.Items.ToList();
                id = list[0].Id;
            }
            catch
            {
                TextToSpeak.TTS_System("Invalid Term or No Results");
                return "n";
            }


            return id;
        }


        /// <summary>
        /// Return the URI of a track from search term
        /// </summary>
        public static String GetTrackFromSearchTerm(String term)
        {
            String id = null;
            try
            {
                SearchItem search = MainForm.web.SearchItems(term, SearchType.Track, 1, 0, "gb");
                Paging<FullTrack> track = search.Tracks;
                List<FullTrack> list = track.Items.ToList();
                id = list[0].Uri;
            }
            catch
            {
                TextToSpeak.TTS_System("Invalid Term or No Results");
                return "n";
            }


            return id;
        }


        /// <summary>
        /// Returns a list of URIs of albums from an artist URI
        /// </summary>
        public static List<String> GetAlbumsFromArtistURI(String uri)
        {

            List<String> uriList = new List<string>();

            if (uri.Equals("n"))
            {
                return uriList;
            }

            Paging<SimpleAlbum> album = MainForm.web.GetArtistsAlbums(uri, AlbumType.Album, 20, 0, "gb");
            List<SimpleAlbum> list = album.Items.ToList();
            
            for (int i = 0; i < list.Count; i++)
            {
                uriList.Add(list[i].Id);
            }

            return uriList;
        }


        /// <summary>
        /// Returns a list of URIs of tracks from a list of albums URI
        /// </summary>
        public static List<String> GetTracksFromMultiAlbums(List<String> albumList)
        {
            List<String> uriList = new List<string>();

            if (albumList.Count == 0)
            {
                return uriList;
            }

            for (int i = 0; i < albumList.Count; i++)
            {
                Paging<SimpleTrack> tracks = MainForm.web.GetAlbumTracks(albumList[i], 30, 0, "gb");
                List<SimpleTrack> list = tracks.Items.ToList();

                for (int j = 0; j < list.Count; j++)
                {
                    uriList.Add(list[j].Uri);
                }

            }

            return uriList;
        }



        /// <summary>
        /// Returns a list of URIs of tracks from a list of albums URI
        /// </summary>
        public static List<String> GetTracksFromAlbum(String album)
        {
            List<String> uriList = new List<string>();

            if (album.Equals("n"))
            {
                return uriList;
            }

            Paging<SimpleTrack> tracks = MainForm.web.GetAlbumTracks(album, 30, 0, "gb");
            List<SimpleTrack> list = tracks.Items.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                uriList.Add(list[i].Uri);
            }

            return uriList;
        }


        /// <summary>
        /// Returns a lists of tracks URI from a playlist URI
        /// </summary>
        public static List<String> GetTracksFromPlaylist(String uri)
        {
            List<String> uriList = new List<string>();

            if (uri.Equals("n"))
            {
                return uriList;
            }

            String[] uriSplit = uri.Split(':');
            Paging<PlaylistTrack> tracks = MainForm.web.GetPlaylistTracks(uriSplit[2], uriSplit[4],"",200,0,"gb");
            List<PlaylistTrack> list = tracks.Items.ToList();

            for(int i = 0; i < list.Count; i++)
            {
                uriList.Add(list[i].Track.Uri);
            }

            return uriList;
        }


        /// <summary>
        /// Runs spotify auth on local browser
        /// </summary>
        public async static void RunAuthentication()
        {

            MainForm.web = new SpotifyWebAPI();
            ProxyConfig _proxyConfig = new ProxyConfig();

            WebAPIFactory webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                "069e3c4de9984bdeb368876fe96d1c1a", Scope.UserLibraryRead,_proxyConfig);

            try
            {
                MainForm.web = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (MainForm.web == null)
                return;

        }
    }
}
