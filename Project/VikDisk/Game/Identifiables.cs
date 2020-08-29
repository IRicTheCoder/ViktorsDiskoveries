using System.Collections.Generic;
using Guu.API;
using Guu.API.Identifiables;

using VikDisk.API;
using VikDisk.API.Identifiables;
using VikDisk.Utils;

namespace VikDisk.Game
{
	/// <summary>
	/// Contains all Identifiables in the Mod
	/// </summary>
	public static class Identifiables
	{
		// REGISTRY PRIORITY
		private static readonly List<System.Type> PRIORITIES = new List<System.Type>()
		{
			typeof(PlantFood),
			typeof(BirdBaby),
			typeof(BirdFood),
			typeof(SpecialFood),
			typeof(Plort),
			typeof(Slime),
			typeof(Largo),
			typeof(SynergyLargo),
			typeof(SlimeResource),
			typeof(Crate),
			typeof(Echo),
			typeof(Effy),
			typeof(Ornament),
			typeof(Liquid),
			typeof(FashionIcon),
			typeof(Toy),
			typeof(FloatingIcon)
		};

		// REGISTRY DICTIONARY
		internal static Dictionary<Identifiable.Id, IdentifiableItem> Items { get; } = new Dictionary<Identifiable.Id, IdentifiableItem>();

		// Registers all Identifiables
		internal static void RegisterAll()
		{
			// Register all
			RegistryUtils.RegisterAll<IdentifiableItem>(PRIORITIES, (item) =>
			{
				Items.Add(item.ID,
				          item is IPediaRegistry registry
					          ? item.Register().AddPediaMapping(registry.PediaID)
					          : item.Register());
				
				IdentifiableHandler.SetupIdentifiable(item.ID);
			});
		}

		/// <summary>
		/// Gets an identifiable item
		/// </summary>
		/// <typeparam name="T">Type of item</typeparam>
		/// <param name="id">ID of the item</param>
		/// <returns>Item found or null if nothing is found</returns>
		public static T Get<T>(Identifiable.Id id) where T : IdentifiableItem
		{
			return !Items.ContainsKey(id) || !(Items[id] is T) ? null : Items[id] as T;
		}
	}
}
