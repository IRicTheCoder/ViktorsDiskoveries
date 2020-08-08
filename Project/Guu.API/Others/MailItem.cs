using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Others
{
	/// <summary>
	/// This is the base class to make mails
	/// </summary>
	public abstract class MailItem : OtherItem
	{
		/// <summary>The mail entry for this mail</summary>
		protected MailRegistry.MailEntry MailEntry { get; set; }

		/// <summary>The mail key for the info on this mail</summary>
		protected abstract string MailKey { get; }

		/// <summary>The method called when this email is read</summary>
		protected virtual void OnReadMail(MailDirector dir, MailDirector.Mail mail) { }

		/// <summary>Builds this Item</summary>
		protected override void Build()
		{
			MailEntry = new MailRegistry.MailEntry(MailKey);
			MailEntry.SetReadCallback(OnReadMail);
		}

		/// <summary>Registers the item into it's registry</summary>
		public override OtherItem Register()
		{
			Build();

			MailRegistry.RegisterMailEntry(MailEntry);

			return this;
		}
	}
}
