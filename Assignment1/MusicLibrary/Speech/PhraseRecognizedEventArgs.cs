using MusicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;

namespace MusicLibrary.Speech
{
    public class PhraseRecognizedEventArgs : EventArgs
    {
        public PhraseRecognizedEventArgs(
            User user,
            string phrase,
            CommandVerb verb,
            SpeechContinuousRecognitionResultGeneratedEventArgs speechRecognitionArgs)
        {
            PhraseTargetUser = user;
            PhraseText = phrase;
            Verb = verb;
        }

        public User PhraseTargetUser { get; private set; }

        public string PhraseText { get; private set; }

        public CommandVerb Verb { get; private set; }

    }
}
