using SRML.Utils.Enum;

namespace VikDisk
{
	public static partial class Enums
	{
		/// <summary>
		/// All ids for the Gadgets
		/// </summary>
		[EnumHolder]
		public static class Gadgets
		{
			// EXTRACTORS
			public static Gadget.Id NOVICE_AGGREGATOR;

			// FUNCTIONING TECHS
			public static Gadget.Id INFUSER;
			public static Gadget.Id MIXER;

			// FASHION PODS
			public static Gadget.Id FASHION_POD_SANTA_STACHE;
		}
	}
}
