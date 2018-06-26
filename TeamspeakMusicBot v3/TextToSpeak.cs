using System;
using System.Speech.Synthesis;


namespace TeamspeakMusicBot_v3
{
    class TextToSpeak
    {
        static SpeechSynthesizer synth = new SpeechSynthesizer();
        public static void TTS_System(String message)
        {

            synth.SelectVoice("Microsoft Hazel Desktop");
            synth.Volume = 70;
            synth.SpeakAsync(message);
        }

        public static void TTS_User(String message)
        {
            synth.SelectVoice("Microsoft Hazel Desktop");
            synth.Volume = 70;
            String tts = Truncate(message, 65, true);
            synth.SpeakAsync(tts);
        }

        public static string Truncate(string str, int length, bool user)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be >= 0");
            }

            if (str == null)
            {
                return null;
            }

            int maxLength;

            if (user)
            {
                String[] a = str.Split('!');
                String ttsString = a[1].Substring(3, a[1].Length - 3);

                maxLength = Math.Min(ttsString.Length, length);
                return ttsString.Substring(0, maxLength);
            }

            maxLength = Math.Min(str.Length, length);
            return str.Substring(0, maxLength);


        }
    }
}
