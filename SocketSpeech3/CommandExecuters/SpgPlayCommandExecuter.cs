using System;
using System.IO;
using System.Media;
using Robotics.API;

namespace SocketSpeech3
{
	/// <summary>
	/// Implements a synchronous command execuer for the play command
	/// </summary>
	public class SpgPlayCommandExecuter : AsyncCommandExecuter
	{
		#region Variables

		/// <summary>
		/// AudioPlayer used to play audio files
		/// </summary>
		private readonly IAudioPlayer audioPlayer;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of PlayCommandExecuter
		/// </summary>
		/// <param name="speechGenerator">AudioPlayer object used to play audio files</param>
		public SpgPlayCommandExecuter(IAudioPlayer audioPlayer)
			: base("spg_play")
		{
			if (audioPlayer == null)
				throw new ArgumentNullException();
			this.audioPlayer = audioPlayer;
		}

		#endregion

		#region Events
		#endregion

		#region Properties
		#endregion

		#region Methodos
		#endregion

		#region Inherited Methodos

		/// <summary>
		/// Executes the play command
		/// </summary>
		/// <param name="command">Command object which contains the command to be executed</param>
		/// <returns>The Response object result of provided command execution. If no response is required, must return null</returns>
		/// <remarks>If the command execution is aborted the execution of this method is
		/// canceled and a failure response is sent if required</remarks>
		protected override Response AsyncTask(Command command)
		{
			bool result;

			result = false;
			try
			{
				if (File.Exists(command.Parameters))
				{
					if (audioPlayer.IsPlaying || (audioPlayer.Pending > 0))
						result = false;
					else
						result = audioPlayer.PlaySync(command.Parameters);
				}
			}
			catch
			{
				result = false;
			}

			return Response.CreateFromCommand(command, result);
		}

		/// <summary>
		/// When overriden, receives the parameters of an analyzed command by the default Signature object as an array of strings
		/// </summary>
		/// <param name="parameters">Array of strings containing the parameters of the command</param>
		public override void DefaultParameterParser(string[] parameters)
		{
			return;
		}

		#endregion
	}
}
