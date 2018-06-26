using SpotifyAPI;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TeamspeakMusicBot_v3
{
    public partial class MainForm : Form
    {

        private static SpotifyLocalAPIConfig config;
        public static SpotifyLocalAPI spotify;
        public static Track currentTrack = new Track();
        public static TrackTimeChangeEventArgs x;
        public static String ts3FileDest;
        public static LinkedList<String> queue = new LinkedList<String>();
        public static List<String> playlists = new List<String>();
        public static SpotifyWebAPI web;
        public readonly ProxyConfig _proxyConfig;


        public MainForm()
        {
            InitializeComponent();

            //Query.ConnectQueryAsync();

            config = new SpotifyLocalAPIConfig
            {
                ProxyConfig = new ProxyConfig()
            };

            spotify = new SpotifyLocalAPI(config);

            Task.Run(() => WebApi.RunAuthentication());

            //Creates files needed
            SetFileLocation(false);
            CreatePlaylistFile();

            //Sets the listners
            spotify.OnTrackTimeChange += OnTrackTimeChange;
            spotify.OnTrackChange += OnTrackChange;
            spotify.OnPlayStateChange += OnStateChange;

            Playlist.CreateBackgroundPlaylist();

            UpdateSongInfo();

        }


        /// <summary>
        /// Sets the file location of the TS3 chat txt
        /// </summary>
        public void SetFileLocation(bool change)
        {
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ts3Dest");

            if (!File.Exists(destPath))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Please Select TS3 Chat Log File...";
                openFileDialog.InitialDirectory = @"C:\Users\Pete\AppData\Roaming\TS3Client\chats";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ts3FileDest = openFileDialog.FileName;
                    File.WriteAllText(destPath, ts3FileDest);

                }
            }

            if (File.Exists(destPath) && change == true)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Please Select TS3 Chat Log File...";
                openFileDialog.InitialDirectory = @"C:\Users\Pete\AppData\Roaming\TS3Client\chats";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ts3FileDest = openFileDialog.FileName;
                    File.WriteAllText(destPath, ts3FileDest);

                }
            }

            else
            {
                ts3FileDest = File.ReadAllText(destPath);
            }
        }

        /// <summary>
        /// Creates the playlist txt if it does not exist
        /// </summary>
        public void CreatePlaylistFile()
        {
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "playlists.txt");

            if (!File.Exists(destPath))
            {
                File.Create(destPath);
            }

            AddPlaylistsToArray();
        }

        /// <summary>
        /// Adds the playlists from the playlists file to an array
        /// </summary>
        public static void AddPlaylistsToArray()
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "playlists.txt");

            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {

                String fileLine = reader.ReadLine().ToString();
                playlists.Add(fileLine);

            }

            reader.Close();
            stream.Close();

        }



        /// <summary>
        /// Connects To Spotify Local
        /// </summary>
        public void ConnectToSpotify()
        {

            if (!SpotifyLocalAPI.IsSpotifyRunning())
            {
                MessageBox.Show(@"Spotify isn't running!");
                return;
            }
            if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning())
            {
                MessageBox.Show(@"SpotifyWebHelper isn't running!");
                return;
            }

            bool successful = spotify.Connect();
            if (successful)
            {
                connectBtn.BackgroundImage = new Bitmap(TeamspeakMusicBot_v3.Properties.Resources.SpotifyGreen);
                connectBtn.Enabled = false;
                lblConnect.Text = "Connected!";
                Playlist.PlayNextSong();

                spotify.ListenForEvents = true;
                new TS3Reader().LastLineListener();

            }
            else
            {
                DialogResult res = MessageBox.Show(@"Couldn't connect to the spotify client. Retry?", @"Spotify", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                    ConnectToSpotify();
            }

        }














        //UI Methods
        public void UpdateSongInfo()
        {
            StatusResponse status = spotify.GetStatus();
            if (status == null)
                return;

            if (status.Track != null)
            {
                UpdateUI(status.Track);
                currentTrack = status.Track;
            }
                

        }

        public async void UpdateUI(Track track)
        {
                songTimeBar.Maximum = track.Length;

                if (track.IsAd())
                    return; //Don't process further, maybe null values

                trackLbl.Text = track.TrackResource?.Name;
                trackLbl.Tag = track.TrackResource?.Uri;

                artistLbl.Text = track.ArtistResource?.Name;
                artistLbl.Tag = track.ArtistResource?.Uri;

                SpotifyUri uri = track.TrackResource?.ParseUri();

                albumArt.Image = track.AlbumResource != null ? await track.GetAlbumArtAsync(AlbumArtSize.Size160, config.ProxyConfig) : null;
        }


        private void OnTrackTimeChange(object sender, TrackTimeChangeEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnTrackTimeChange(sender, e)));
                return;
            }

            
            if(!trackLbl.Text.Equals(currentTrack.TrackResource.Name))
                UpdateSongInfo();

            lblTime.Text = $@"{FormatTime(e.TrackTime)}/{FormatTime(currentTrack.Length)}";

            if (e.TrackTime < currentTrack.Length)
                songTimeBar.Value = (int)e.TrackTime;

            if (e.TrackTime == 0 && !Playlist.CheckPlaying())
                Playlist.PlayNextSong();


        }

        private void OnTrackChange(object sender, TrackChangeEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnTrackChange(sender, e)));
                return;
            }

            UpdateSongInfo();
        }

        private void OnStateChange(object sender, PlayStateEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnStateChange(sender, e)));
                return;
            }

            UpdateSongInfo();
        }


        private void changeFileBtn_Click(object sender, EventArgs e)
        {
            SetFileLocation(true);
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            ConnectToSpotify();
        }


















        public static String FormatTime(double sec)
        {
            TimeSpan span = TimeSpan.FromSeconds(sec);
            String secs = span.Seconds.ToString(), mins = span.Minutes.ToString();
            if (secs.Length < 2)
                secs = "0" + secs;
            return mins + ":" + secs;
        }

    }
}
