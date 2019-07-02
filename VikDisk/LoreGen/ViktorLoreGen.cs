using SRML.SR;

namespace VikDisk.LoreGen
{
	/// <summary>
	/// Handles all lore based events for Viktor part in the Mod's Lore
	/// </summary>
	public class ViktorLoreGen
	{
		/// <summary>
		/// Registers the callbacks needed for the mails to be triggered
		/// </summary>
		public void RegisterCallbacks()
		{
			// Adds events into the Callbacks
			SRCallbacks.OnSaveGameLoaded += (sceneCxt) =>
			{
				SRSingleton<SceneContext>.Instance.MailDirector.SendMailIfExists(MailDirector.Type.EXCHANGE, Configs.Mails.INTRO_MAIL_KEY);
			};
		}
	}
}
