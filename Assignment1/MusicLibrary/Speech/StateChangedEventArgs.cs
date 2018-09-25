using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;

namespace MusicLibrary.Speech
{
    public class StateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangedEventArgs"/> class
        /// with the specified <see cref="SpeechRecognizer"/> state.
        /// </summary>
        /// <param name="args">The speech recognizer state.</param>
        /// <remarks>Use this overload for reporting changes in <see cref="SpeechRecognizer"/> state.</remarks>
        public StateChangedEventArgs(SpeechRecognizerStateChangedEventArgs args)
        {
            IsSpeechRecognizerState = true;
            IsSessionState = false;

            IsIdle = (args.State == SpeechRecognizerState.Idle);
            IsCapturing = (args.State == SpeechRecognizerState.Capturing);
            IsProcessing = (args.State == SpeechRecognizerState.Processing);
            SoundStarted = (args.State == SpeechRecognizerState.SoundStarted);
            SoundEnded = (args.State == SpeechRecognizerState.SoundEnded);
            SpeechDetected = (args.State == SpeechRecognizerState.SpeechDetected);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangedEventArgs"/> class
        /// with the specified recognition session state.
        /// </summary>
        /// <param name="args">The session state.</param>
        /// <remarks>Use this overload for reporting changes in the session state.</remarks>
        public StateChangedEventArgs(SpeechContinuousRecognitionCompletedEventArgs args)
        {
            IsSessionState = true;
            IsSpeechRecognizerState = false;

            // Session-related properties.
            SessionCompletedSuccessfully = (args.Status == SpeechRecognitionResultStatus.Success);
            AudioQualitySuccess = !(args.Status == SpeechRecognitionResultStatus.AudioQualityFailure);
            UserCanceledSession = (args.Status == SpeechRecognitionResultStatus.UserCanceled);
            SessionTimedOut = (args.Status == SpeechRecognitionResultStatus.TimeoutExceeded);
            MicrophoneAvailable = !(args.Status == SpeechRecognitionResultStatus.MicrophoneUnavailable);
            NetworkAvailable = !(args.Status == SpeechRecognitionResultStatus.NetworkFailure);
        }

        #region Public properties that define the kind of data provided

        /// <summary>
        /// Gets a value indicating whether the event data contains
        /// info about the recognizer session.
        /// </summary>
        public bool IsSessionState { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the event data contains
        /// info about the speech recognizer state.
        /// </summary>
        public bool IsSpeechRecognizerState { get; private set; }

        #endregion

        #region Public properties related to the recognition session

        /// <summary>
        /// Gets a value indicating whether the recognition session 
        /// or grammar compilation completed successfully.
        /// </summary>
        public bool SessionCompletedSuccessfully { get; private set; }

        /// <summary>
        /// Gets a value indicating whether audio problems caused recognition to fail.
        /// </summary>
        public bool AudioQualitySuccess { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the user canceled the recognition session.
        /// </summary>
        public bool UserCanceledSession { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the session timeout 
        /// expired due to extended silence or poor audio. 
        /// </summary>
        public bool SessionTimedOut { get; private set; }

        /// <summary>
        /// Gets a value indicating whether a microphone is turned on
        /// and available for the app to use. 
        /// </summary>
        public bool MicrophoneAvailable { get; private set; }

        /// <summary>
        /// Gets a value indicating whether network problems are preventing
        /// speech recognition from working.
        /// </summary>
        public bool NetworkAvailable { get; private set; }

        #endregion

        #region Public properties related to the state of the speech recognizer

        /// <summary>
        /// Gets a value indicating whether the speech recognizer 
        /// is not listening for audio input from the user.
        /// </summary>
        public bool IsIdle { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the speech recognizer 
        /// is listening for audio input from the user.
        /// </summary>
        public bool IsCapturing { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the speech recognizer 
        /// is attempting to recognize audio input from the user. 
        /// </summary>
        /// <remarks><para>In the processing state, the recognizer doesn't 
        /// listen for audio input from the user. During standard recognition, 
        /// the state can occur after the recognizer has stopped capturing 
        /// audio input and before a recognition result is returned.
        /// During continuous recognition, this state can occur after the 
        /// StopAsync method has been called and before the Completed event 
        /// is raised.</para>
        /// <para>This flag is useful for indicating that the user should stop speaking.
        /// </para>
        /// </remarks>
        public bool IsProcessing { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the speech recognizer has detected 
        /// sound (not necessarily speech) on the audio stream.
        /// </summary>
        public bool SoundStarted { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the speech recognizer 
        /// no longer detects sound on the audio stream.
        /// </summary>
        /// <remarks>The recognition session remains active.</remarks>
        public bool SoundEnded { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the speech recognizer 
        /// has detected speech input on the audio stream.
        /// </summary>
        public bool SpeechDetected { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the speech recognizer 
        /// is no longer attempting to recognize audio input from the user 
        /// and is buffering ongoing audio input.
        /// </summary>
        /// <remarks>In the paused state, you can add and compile grammar constraints.
        /// this state is valid only for continuous recognition.</remarks>
        public bool IsPaused { get; private set; }

        #endregion
    }
}
