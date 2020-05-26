using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Snake_Kruglov
{
    public class Sounds
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        private string pathToMedia;
        public Sounds(string pathToResources)
        {
            pathToMedia = pathToResources;
        }

        public void Play()
        {
            player.URL = pathToMedia + "Jotaro.wav";
            player.settings.volume = 30;
            player.controls.play();
        }
        public void Play(string songName)
        {
            player.URL = pathToMedia + songName + ".wav";
            player.controls.play();
        }
    }

}