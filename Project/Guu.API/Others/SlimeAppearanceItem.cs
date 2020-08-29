using System.Collections.Generic;
using System.Linq;
using Guu.Utils;
using SRML.SR;
using UnityEngine;

namespace Guu.API.Others
{
	/// <summary>
	/// This is the base class for all Slime Appearance Items
	/// </summary>
	public abstract class SlimeAppearanceItem : OtherItem
	{
		/// <summary>Base appearance to base this in (set null to build the appearance from scratch, overriding the Build method will be required for this)</summary>
		protected abstract SlimeAppearance BaseApp { get; }

		/// <summary>The slime appearance of this item</summary>
		public SlimeAppearance Appearance { get; set; }

		/// <summary>The translate key of the name of this appearance</summary>
		public virtual string NameXlateKey { get; } = "l.custom_appearance";

		/// <summary>The save set for this appearance</summary>
		protected virtual SlimeAppearance.AppearanceSaveSet? SaveSet => BaseApp?.SaveSet;

		/// <summary>The death appearance</summary>
		protected virtual DeathAppearance DeathAppearance => BaseApp?.DeathAppearance;

		/// <summary>The explosion appearance</summary>
		protected virtual ExplosionAppearance ExplosionAppearance => BaseApp?.ExplosionAppearance;

		/// <summary>The vines appearance</summary>
		protected virtual VineAppearance VineAppearance => BaseApp?.VineAppearance;

		/// <summary>The tornado appearance</summary>
		protected virtual TornadoAppearance TornadoAppearance => BaseApp?.TornadoAppearance;

		/// <summary>The glint appearance</summary>
		protected virtual GlintAppearance GlintAppearance => BaseApp?.GlintAppearance;

		/// <summary>The crystal appearance</summary>
		protected virtual CrystalAppearance CrystalAppearance => BaseApp?.CrystalAppearance;

		/// <summary>The shocked appearance</summary>
		protected virtual SlimeAppearance ShockedAppearance => BaseApp?.ShockedAppearance;

		/// <summary>The qubit appearance</summary>
		protected virtual SlimeAppearance QubitAppearance => BaseApp?.QubitAppearance;

		/// <summary>The dependent appearances</summary>
		protected virtual SlimeAppearance[] DependentAppearances => BaseApp?.DependentAppearances;

		/// <summary>The face of the slime</summary>
		protected virtual SlimeFace Face => BaseApp?.Face;

		/// <summary>The structures for this appearance</summary>
		protected virtual SlimeAppearanceStructure[] Structures => BaseApp?.Structures;

		/// <summary>The override for the slime animator</summary>
		protected virtual RuntimeAnimatorController AnimatorOverride => BaseApp?.AnimatorOverride;

		/// <summary>The icon for this appearance</summary>
		protected virtual Sprite Icon => BaseApp?.Icon;

		/// <summary>The color palette of this slime</summary>
		protected virtual SlimeAppearance.Palette? ColorPalette => BaseApp?.ColorPalette;

		/// <summary>Builds this Appearance</summary>
		protected override void Build()
		{
			if (BaseApp != null)
			{
				Appearance.SaveSet = SaveSet ?? SlimeAppearance.AppearanceSaveSet.CLASSIC;
				Appearance.DeathAppearance = DeathAppearance;
				Appearance.ExplosionAppearance = ExplosionAppearance;
				Appearance.VineAppearance = VineAppearance;
				Appearance.TornadoAppearance = TornadoAppearance;
				Appearance.GlintAppearance = GlintAppearance;
				Appearance.CrystalAppearance = CrystalAppearance;
				Appearance.DependentAppearances = DependentAppearances;
				Appearance.ShockedAppearance = ShockedAppearance;
				Appearance.QubitAppearance = QubitAppearance;
				Appearance.Face = Face;

				Appearance.Structures = Structures.Select(SlimeUtils.CloneAppearanceStructure).ToArray();
				
				Appearance.AnimatorOverride = AnimatorOverride;
				Appearance.Icon = Icon;
				Appearance.ColorPalette = ColorPalette ?? SlimeAppearance.Palette.Default;
			}

			foreach (SlimeAppearanceStructure str in Appearance.Structures)
			{
				foreach (Material mat in str.DefaultMaterials)
				{
					mat.SetTripleColor(Appearance.ColorPalette);
				}
			}
		}

		/// <summary>Registers the slime appearance into it's registry</summary>
		public override OtherItem Register()
		{
			Appearance = ScriptableObject.CreateInstance<SlimeAppearance>();
			Appearance.name = Name;

			Build();

			return this;
		}
	}
}
