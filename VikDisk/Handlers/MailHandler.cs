using SRML.SR;

namespace VikDisk.Handlers
{
	/// <summary>
	/// Handles the mails added by the mod
	/// </summary>
	public class MailHandler : Handler<MailHandler>
	{
		// THE KEY TO ACCESS THE EMBBEDDED MAIL TEXT FILES
		private const string RESOURCE_KEY = "Mails.";

		/// <summary>
		/// Setups the handler
		/// </summary>
		public override void Setup()
		{
			foreach (MailInfo info in Configs.Mails.mails)
				RegisterMail(info);
		}

		// REGISTERS A NEW MAIL
		private void RegisterMail(MailInfo info)
		{
			MailRegistry.MailEntry entry = MailRegistry.RegisterMailEntry(info.key);
			entry.SetSubjectTranslation(info.subject);
			entry.SetFromTranslation(info.author);
			entry.SetBodyTranslation(Utils.GetTextFromEmbbededFile(RESOURCE_KEY + info.key + ".txt"));
		}

		/// <summary>
		/// Information about the emails
		/// </summary>
		public struct MailInfo
		{
			public string key;
			public string subject;
			public string author;

			public MailInfo(string key, string sub, string aut)
			{
				this.key = key;
				subject = sub;
				author = aut;
			}
		}
	}
}
