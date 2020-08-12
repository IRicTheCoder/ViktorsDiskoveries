using System;
using System.Collections.Generic;

using Guu.Utils;

using UnityEngine;

namespace SRML.Debug
{
	/// <summary>
	/// Controls all the markers on the world
	/// </summary>
	public static class MarkerController
	{
		/// <summary>
		/// Called when the Marker Controller is set up
		/// <para>Use this to add your custom markers to the setup</para>
		/// <para>This runs after the entire setup!</para>
		/// </summary>
		public static event Action OnMarkerSetup;

		// List of Identifiable IDs to Ignore
		private static readonly List<Identifiable.Id> toIgnoreIden = new List<Identifiable.Id>()
		{
			// THINGS WITH NO PREFABS
			Identifiable.Id.NONE,
			Identifiable.Id.PLAYER,
			Identifiable.Id.SCARECROW,
			Identifiable.Id.SPECIAL_EXCHANGE_CRATE,
			Identifiable.Id.FIRE_COLUMN,
			Identifiable.Id.PORTABLE_SCARECROW,

			// PLATFORM SPECIFIC CONTENT
			Identifiable.Id.PIRATEY_FASHION,
			Identifiable.Id.HEROIC_FASHION,
			Identifiable.Id.SCIFI_FASHION,
			Identifiable.Id.TREASURE_CHEST_TOY,
			Identifiable.Id.BOP_GOBLIN_TOY,
			Identifiable.Id.ROBOT_TOY,

			// SPECIAL CONTENT
			Identifiable.Id.GLITCH_TARR_PORTAL
		};

		// List of Gadget IDs to Ignore
		private static readonly List<Gadget.Id> toIgnoreGadget = new List<Gadget.Id>()
		{
			// THINGS WITH NO PREFABS
		};

		/// <summary>The missing image for marker icons</summary>
		public static Texture2D MissingImage { get; private set; }

		/// <summary>The image for gadget site markers</summary>
		public static Texture2D GadgetSite { get; private set; }

		/// <summary>Should the markers be shown?</summary>
		public static bool ShowMarkers { get; internal set; } = false;

		// To setup the controller
		internal static void Setup()
		{
			MissingImage = SRObjects.MissingIcon?.texture;
			GadgetSite = SRObjects.Get<Sprite>("iconPlaceGadget")?.texture;

			foreach (string name in Enum.GetNames(typeof(Identifiable.Id)))
			{
				Identifiable.Id ID = EnumUtils.Parse<Identifiable.Id>(name);
				if (toIgnoreIden.Contains(ID))
					continue;

				GameObject obj = Identifiable.GORDO_CLASS.Contains(ID)
					? ExceptionUtils.IgnoreErrors(() => GameContext.Instance.LookupDirector.GetGordo(ID))
					: ExceptionUtils.IgnoreErrors(() => GameContext.Instance.LookupDirector.GetPrefab(ID));

				if (obj == null)
					continue;

				InstallIDMarker(ID, obj);
			}

			foreach (string name in Enum.GetNames(typeof(Gadget.Id)))
			{
				Gadget.Id ID = EnumUtils.Parse<Gadget.Id>(name);
				if (toIgnoreGadget.Contains(ID))
					continue;

				GameObject obj = ExceptionUtils.IgnoreErrors(() => GameContext.Instance.LookupDirector.GetGadgetDefinition(ID).prefab);

				if (obj == null)
					continue;

				InstallGadgetMarker(ID, obj);
			}

			foreach (Identifiable found in UnityEngine.Object.FindObjectsOfType<Identifiable>())
			{
				if (found.gameObject.scene.buildIndex == 3)
					InstallIDMarker(found.id, found.gameObject);
			}

			foreach (GordoIdentifiable found in UnityEngine.Object.FindObjectsOfType<GordoIdentifiable>())
			{
				if (found.gameObject.scene.buildIndex == 3)
					InstallIDMarker(found.id, found.gameObject);
			}

			foreach (Gadget found in UnityEngine.Object.FindObjectsOfType<Gadget>())
			{
				if (found.gameObject.scene.buildIndex == 3)
					InstallGadgetMarker(found.id, found.gameObject);
			}

			foreach (GadgetSite found in UnityEngine.Object.FindObjectsOfType<GadgetSite>())
			{
				if (found.gameObject.scene.buildIndex == 3)
					found.gameObject.InstallMarker<GadgetSiteMarker>();
			}

			OnMarkerSetup?.Invoke();
		}

		// Install Identifiable Markers
		internal static void InstallIDMarker(Identifiable.Id ID, GameObject obj)
		{
			if (toIgnoreIden.Contains(ID))
				return;

			if (Identifiable.GORDO_CLASS.Contains(ID))
			{
				obj.InstallMarker<GordoMarker>();
				return;
			}

			if (Identifiable.IsToy(ID))
			{
				obj.InstallMarker<ToyMarker>();
				return;
			}

			if (Identifiable.IsLargo(ID))
			{
				obj.InstallMarker<LargoMarker>();
				return;
			}

			if (Identifiable.STANDARD_CRATE_CLASS.Contains(ID))
			{
				obj.InstallMarker<CrateMarker>();
				return;
			}

			obj.InstallMarker<IdentifiableMarker>();
		}

		// Install Gadget Markers
		internal static void InstallGadgetMarker(Gadget.Id ID, GameObject obj)
		{
			if (toIgnoreGadget.Contains(ID))
				return;

			/*if (Gadget.IsFashionPod(ID))
			{
				obj.InstallMarker<FashionPodMarker>();
				return;
			}*/
			// TODO: Figure out fashion pods

			obj.InstallMarker<GadgetMarker>();
		}
	}
}
