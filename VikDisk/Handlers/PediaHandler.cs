using SRML.SR;

namespace VikDisk.Handlers
{
	/// <summary>
	/// Handles everything related to Slimepedia
	/// </summary>
	public class PediaHandler
	{
		/// <summary>
		/// Trys to fix entries to the slimepedia (to allow for changes caused by the mod)
		/// </summary>
		public void FixEntries()
		{
			// TODO: Add triggers

			// Fixes Slime Favorites
			TranslationPatcher.AddPediaTranslation("m.favorite.pink_slime", "Carrot, Pogofruit and Hen Hen");
			TranslationPatcher.AddPediaTranslation("m.favorite.saber_slime", "Any Elder Chicken");
			TranslationPatcher.AddPediaTranslation("m.favorite.gold_slime", "Gilded Ginger");

			// Fixes Food Entries
			TranslationPatcher.AddPediaTranslation("m.favored_by.carrot", "Pink Slime");
			TranslationPatcher.AddPediaTranslation("m.favored_by.pogo", "Pink Slime");
			TranslationPatcher.AddPediaTranslation("m.favored_by.henhen", "Pink Slime");
			TranslationPatcher.AddPediaTranslation("m.favored_by.elder_roostro", "Saber Slime");
			TranslationPatcher.AddPediaTranslation("m.favored_by.elder_hen", "Saber Slime");

			// Fixes to Lore
			TranslationPatcher.AddPediaTranslation("m.how_to_use.kookadoba", "Kookadoba can only grow in the soils of The Wilds. A garden's replicator will reject it. At least with current technology!");
			TranslationPatcher.AddPediaTranslation("m.desc.kookadoba", "Kookadoba are known as 'the king of fruit' by their enthusiasts, which include the jolly \n" +
				"gourmand, Ogden Ortiz. However, harvesting them can be quite perilous as they only grow in the feral domain of \n" +
				"The Wilds. Worse, the kookadoba smells and tastes horrible to most palates, with most who try it reporting it has \n" +
				"the flavor of 'stinky cheese meets stinkier cheese.'\n\n" +
				"ENTRY #2: According to Viktor Humphries, vaccinated slimes show strange behaviour when eating this fruit \n" +
				"as they seem to actually like it, independent of their normal diet.");

			TranslationPatcher.AddPediaTranslation("m.desc.ginger", "Curiously, gilded ginger cannot be replicated in a garden as its unusual biological properties prohibit it. However, there is perhaps another use for it...\n\n" +
				"ENTRY #2: With the new gilded replicator tech for gardens, they are replicatable, but at a cost of some good farming land!");

			// TODO: Add new entries to craft materials
			// TODO: Add new info to gardens and plort collectors
		}
	}
}
