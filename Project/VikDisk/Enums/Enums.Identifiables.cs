using SRML.Utils.Enum;

namespace VikDisk
{
	public static partial class Enums
	{
		/// <summary>
		/// All ids for the Identifiables
		/// </summary>
		[EnumHolder]
		public static class Identifiables
		{
			// VEGGIES
			public static Identifiable.Id DARK_GINGER_VEGGIE;

			// FRUITS
			public static Identifiable.Id GLITCH_POGO_FRUIT;

			// ANIMALS
			public static Identifiable.Id FIRE_HEN;
			public static Identifiable.Id FIRE_CHICK;

			// OTHER FOODS
			public static Identifiable.Id EXTRA_SPICY_TOFU;

			// PLORTS
			public static Identifiable.Id REAL_GLITCH_PLORT;
			public static Identifiable.Id TARR_PLORT;
			public static Identifiable.Id DREAM_PLORT;

			// SLIMES
			public static Identifiable.Id REAL_GLITCH_SLIME;
			
			// SYNERGISTIC LARGOS
			//public static Identifiable.Id ELETRIC_LARGO;

			// SLIME RESOURCES
			public static Identifiable.Id PINK_SLIME_EXTRACT_CRAFT; // TODO: Change this when the Extract base class is created, make the register method add the extract to the Craft class

			// CRATES
			public static Identifiable.Id GLITCH_CRATE;

			// ECHOS
			public static Identifiable.Id WHITE_ECHO;
			public static Identifiable.Id RAINBOW_ECHO;

			// EFFIES
			public static Identifiable.Id GLITCH_EFFY;
			public static Identifiable.Id CARTOON_STARS_EFFY;

			// ORNAMENTS
			public static Identifiable.Id DREAM_ORNAMENT;
			public static Identifiable.Id VIKTOR_ORNAMENT;

			// LIQUID
			public static Identifiable.Id GUU_LIQUID;

			// FASHIONS
			public static Identifiable.Id SANTA_STACHE_FASHION;

			// TOYS
			public static Identifiable.Id BASKET_BALL_TOY;

			// OTHERS
			public static Identifiable.Id WONDER_TICKET;
		}
	}
}
