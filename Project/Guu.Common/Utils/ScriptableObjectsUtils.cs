using UnityEngine;

namespace SRML
{
	/// <summary>
	/// An utility class to help with Scriptable Objects
	/// </summary>
	public static class ScriptableObjectsUtils
	{
		/// <summary>
		/// Creates an Upgrade Definition
		/// </summary>
		/// <param name="upgrade">The upgrade for this definition</param>
		/// <param name="icon">The icon to be displayed</param>
		/// <param name="cost">The cost of this upgrade</param>
		public static UpgradeDefinition CreateUpgradeDefinition(PlayerState.Upgrade upgrade, Sprite icon, int cost)
		{
			UpgradeDefinition definition = ScriptableObject.CreateInstance<UpgradeDefinition>();
			definition.SetPrivateField("upgrade", upgrade);
			definition.SetPrivateField("icon", icon);
			definition.SetPrivateField("cost", cost);

			return definition;
		}

		/// <summary>
		/// Creates a Liquid Definition
		/// </summary>
		/// <param name="id">The ID of the liquid for this definition</param>
		/// <param name="inFX">The effect when vacced in</param>
		/// <param name="vacFailFX">The effect when vaccing fails</param>
		public static LiquidDefinition CreateLiquidDefinition(Identifiable.Id id, GameObject inFX, GameObject vacFailFX)
		{
			LiquidDefinition definition = ScriptableObject.CreateInstance<LiquidDefinition>();
			definition.SetPrivateField("id", id);
			definition.SetPrivateField("inFX", inFX);
			definition.SetPrivateField("vacFailFX", vacFailFX);

			return definition;
		}

		/// <summary>
		/// Creates a Toy Definition
		/// </summary>
		/// <param name="toyId">The ID of the toy for this definition</param>
		/// <param name="icon">The icon to be displayed</param>
		/// <param name="cost">The cost of this toy</param>
		/// <param name="nameKey">The name for the language key</param>
		public static ToyDefinition CreateToyDefinition(Identifiable.Id toyId, Sprite icon, int cost, string nameKey = null)
		{
			ToyDefinition definition = ScriptableObject.CreateInstance<ToyDefinition>();
			definition.SetPrivateField("toyId", toyId);
			definition.SetPrivateField("icon", icon);
			definition.SetPrivateField("cost", cost);
			definition.SetPrivateField("nameKey", nameKey?.ToLower() ?? toyId.ToString().ToLower());

			return definition;
		}
	}
}
