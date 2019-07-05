using SRML.SR;
using System.Collections.Generic;
using static LookupDirector;
using UnityEngine;
using VikDisk.ConsoleSystem;

namespace VikDisk.Handlers
{
	/// <summary>
	/// Handles all vac related stuff
	/// </summary>
	public class AmmoHandler : Handler<AmmoHandler>
	{
		/// <summary>
		/// Setups the handler
		/// </summary>
		public override void Setup()
		{
			// Fixes Vac Entries
			/*Dictionary<Identifiable.Id, VacEntry> vacFixes = Utils.ProcessFixesFile<Identifiable.Id, VacEntry>("VacEntries", (key, value) =>
			{
				Identifiable.Id id = (Identifiable.Id)(System.Enum.Parse(typeof(Identifiable.Id), key, false) ?? Identifiable.Id.NONE);
				if (id == Identifiable.Id.NONE)
					return null;

				if (ColorUtility.TryParseHtmlString(value, out Color color))
				{
					return new KeyValuePair<Identifiable.Id, VacEntry>(id, new VacEntry()
					{
						color = color,
						icon = null
					});
				}

				return null;
			});

			foreach (VacEntry entry in GameContext.Instance.LookupDirector.vacEntries)
			{
				if (vacFixes.ContainsKey(entry.id))
				{
					entry.color = vacFixes[entry.id].color != Color.clear ? vacFixes[entry.id].color : entry.color;
					entry.icon = vacFixes[entry.id].icon ?? entry.icon;
				}
			}*/
		}

		/// <summary>
		/// Registers vac entries and ammos for slimes
		/// </summary>
		public void RegisterSlimes()
		{
			AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.SABER_SLIME));
			AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.NIMBLE_VALLEY, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.SABER_SLIME));
		}

		/// <summary>
		/// Registers ammos for Storage
		/// </summary>
		public void RegisterStorage()
		{

		}
	}
}
