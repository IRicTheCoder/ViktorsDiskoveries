using SRML.SR;

namespace VikDisk.Handlers
{
	/// <summary>
	/// Handles all vac related stuff
	/// </summary>
	public class VacHandler : Handler<VacHandler>
	{
		/// <summary>
		/// Setups the handler
		/// </summary>
		public override void Setup()
		{
			// NO SETUP REQUIRED FOR NOW
		}

		public void RegisterNewVacSlimes()
		{
			AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.SABER_SLIME));
			AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.NIMBLE_VALLEY, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.SABER_SLIME));
		}
	}
}
