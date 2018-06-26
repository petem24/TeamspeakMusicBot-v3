using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TeamspeakMusicBot_v3
{
    public class TS3Reader
    {
        String dupCheck = null;
        String fileLine = null;

        static FileStream stream = File.Open(MainForm.ts3FileDest, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        static StreamReader reader = new StreamReader(stream);

        public void LastLineListener()
        {
            while (!reader.EndOfStream)
            { 
                fileLine = reader.ReadLine().ToString();
            }

            fileLine = fileLine.Trim();
            dupCheck = fileLine;

            CommandLoop();
        }

        public void CommandLoop()
        {

            new Thread(() =>
            {
                Process thisProc = Process.GetCurrentProcess();
                thisProc.PriorityClass = ProcessPriorityClass.Normal;

                while (true)
                {
                    //Thread.CurrentThread.IsBackground = true;

                    stream = File.Open(MainForm.ts3FileDest, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    reader = new StreamReader(stream);

                    while (!reader.EndOfStream)
                    {
                        fileLine = reader.ReadLine().ToString();
                    }

                    fileLine = fileLine.Trim();




                    if (fileLine.Contains("spotify:track") && fileLine.Contains("!add") && !fileLine.Equals(dupCheck))
                    {

                        string result = null;
                        int start = fileLine.IndexOf("spotify");

                        try
                        {
                            result = fileLine.Substring(start, 36);
                        }

                        catch
                        {
                            TextToSpeak.TTS_System("Please enter a valid song");
                            dupCheck = fileLine;
                            break;
                        }

                        Playlist.userQueue.Enqueue(result);


                        if (!Playlist.CheckPlaying())
                        {
                            Playlist.PlayNextSong();
                        }

                        dupCheck = fileLine;

                    }




                    if (fileLine.Contains("!skip") && !fileLine.Equals(dupCheck))
                    {
                        Playlist.PlayNextSong();
                        dupCheck = fileLine;
                    }




                    if (fileLine.Contains("!volume") && !fileLine.Equals(dupCheck))
                    {

                        String[] volumeString = fileLine.Split(' ');
                        float volume = 0;
                        try
                        {
                            volume = float.Parse(volumeString[3]);
                        }

                        catch
                        {
                            TextToSpeak.TTS_System("Please enter a number between 0-100");
                            break;
                        }


                        if (volume > 100 || volume < 0)
                        {
                            TextToSpeak.TTS_System("Please enter a number between 0-100");
                        }

                        else
                        {
                            MainForm.spotify.SetSpotifyVolume(volume);
                        }

                        dupCheck = fileLine;

                    }




                    if (fileLine.Contains("!tts") && !fileLine.Equals(dupCheck))
                    {

                        TextToSpeak.TTS_User(fileLine);
                        dupCheck = fileLine;


                    }




                    if (fileLine.Contains("!song") && !fileLine.Equals(dupCheck))
                    {
                        TextToSpeak.TTS_System(Playlist.GetTrackName() + " by " + Playlist.GetTrackArtist());
                        dupCheck = fileLine;
                    }




                    if (fileLine.Contains("!pl") && !fileLine.Equals(dupCheck))
                    {
                        String[] plString = fileLine.Split(' ');
                        int plNumber = 0;
                        List<String> list = new List<String>();

                        try
                        {
                            plNumber = int.Parse(plString[3])-1;
                        }

                        catch
                        {
                            TextToSpeak.TTS_System("");
                            break;
                        }

                        if (plNumber > MainForm.playlists.Count || plNumber < 0)
                        {
                            TextToSpeak.TTS_System("Enter valid playlist number");
                        }

                        else
                        {
                            list = WebApi.GetTracksFromPlaylist(MainForm.playlists[plNumber]);
                            Playlist.ChangeCurrentPlaylist(list);
                        }

                        dupCheck = fileLine;


                    }



                    if (fileLine.Contains("!artist") && !fileLine.Equals(dupCheck))
                    {
                        String[] artistString = fileLine.Split('!');
                        String artistName;

                        try
                        {
                            artistName = artistString[1].Substring(7);
                        }
                        catch
                        {
                            TextToSpeak.TTS_System("Invalid Format");
                            break;
                        }

                        List<String> tracks = WebApi.GetTracksFromMultiAlbums(WebApi.GetAlbumsFromArtistURI(WebApi.GetArtistFromSearchTerm(artistName)));
                        Playlist.ChangeCurrentPlaylist(tracks);

                        dupCheck = fileLine;
                    }



                    if (fileLine.Contains("!album") && !fileLine.Equals(dupCheck))
                    {
                        String[] albumString = fileLine.Split('!');
                        String albumName;

                        try
                        {
                            albumName = albumString[1].Substring(6);
                        }
                        catch
                        {
                            TextToSpeak.TTS_System("Invalid Format");
                            break;
                        }

                        List<String> tracks = WebApi.GetTracksFromAlbum(WebApi.GetAlbumFromSearchTerm(albumName));
                        Playlist.ChangeCurrentPlaylist(tracks);

                        dupCheck = fileLine;
                    }



                    if (fileLine.Contains("!track") && !fileLine.Equals(dupCheck))
                    {
                        String[] trackString = fileLine.Split('!');
                        String trackName;

                        try
                        {
                            trackName = trackString[1].Substring(6);
                        }
                        catch
                        {
                            TextToSpeak.TTS_System("Invalid Format");
                            break;
                        }

                        String track = WebApi.GetTrackFromSearchTerm(trackName);
                        Playlist.userQueue.Enqueue(track);

                        dupCheck = fileLine;
                    }

                    stream.Close();
                    reader.Close();

                    Thread.Sleep(1000);


                }



            }).Start();


        }

        /*[return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_APPCOMMAND = 0x0319;
        private const long cmdNextTrack = 0x000B0000L;
        private const long cmdPreviousTrack = 0x000C0000L;

        static Process proc = Process.GetProcessesByName("Spotify").FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

        private IntPtr spotifyHWnd = proc.MainWindowHandle;

        public void NextTrack()
        {
            PostMessage(spotifyHWnd, WM_APPCOMMAND, IntPtr.Zero, (IntPtr)cmdNextTrack);
        }

        public void PreviousTrack()
        {
            PostMessage(spotifyHWnd, WM_APPCOMMAND, IntPtr.Zero, (IntPtr)cmdPreviousTrack);
        }
        */

    }

}
