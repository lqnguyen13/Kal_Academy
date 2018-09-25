using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.VoiceCommandObjects
{
    public class VoiceCommand
    {
        public string VoiceCommandName { get; set; }
        public string CommandMode { get; set; }
        public string SongSpoken { get; set; }
        public string  SongOwner { get; set; }
    }
}
