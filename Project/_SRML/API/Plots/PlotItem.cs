using System.Collections.Generic;
using SRML.SR;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class for all plot items
	/// </summary>
	public abstract class PlotItem : RegistryItem<PlotItem>
	{
		/// <summary>The prefab for this item</summary>
		public GameObject Prefab { get; protected set; }

		/// <summary>The name prefix for this object</summary>
		protected virtual string NamePrefix { get; } = "plot";

		/// <summary>The Plot ID of this item</summary>
		public abstract LandPlot.Id ID { get; }

		/// <summary>The UI to open when activated</summary>
		public abstract GameObject UIPrefab { get; }

		/// <summary>Should register this plot as a purchasable?</summary>
		public virtual bool RegisterAsPurchasable { get; } = true;

		/// <summary>Cost of buying this plot</summary>
		public virtual int PlotCost { get; } = 0;

		/// <summary>Icon for this plot</summary>
		public virtual Sprite PlotIcon { get; } = null;

		/// <summary>Image for this plot</summary>
		public virtual Sprite PlotImg { get; } = null;

		/// <summary>The pedia ID for this plot</summary>
		public abstract PediaDirector.Id PediaID { get; }

		/// <summary>Is the plot unlocked?</summary>
		public virtual bool IsUnlocked() { return true; }

		/// <summary>Registers the item into it's registry</summary>
		public override PlotItem Register()
		{
			Build();

			LookupRegistry.RegisterLandPlot(Prefab);
			if (RegisterAsPurchasable)
			{
				LandPlotRegistry.RegisterPurchasableLandPlot(new LandPlotRegistry.LandPlotShopEntry()
				{
					cost = PlotCost,
					icon = PlotIcon ?? SRObjects.MissingIcon,
					mainImg = PlotImg ?? PlotIcon ?? SRObjects.MissingImg,
					pediaId = PediaID,
					plot = ID,
					isUnlocked = IsUnlocked
				});
			}
			else
			{
				PediaRegistry.RegisterIdEntry(PediaID, PlotIcon ?? SRObjects.MissingIcon);
				PediaRegistry.SetPediaCategory(PediaID, PediaRegistry.PediaCategory.RANCH);
			}

			// TODO: Fix pedia behaviour

			return this;
		}

		/// <summary>
		/// Adds a new Upgrader to this plot
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="config"></param>
		public void AddUpgrader<T>(System.Action<T> config) where T : PlotUpgrader
		{
			T upgrader = Prefab.GetComponent<T>();
			config?.Invoke(upgrader);
		}

		/// <summary>
		/// Adds a new plot object to this plot
		/// </summary>
		/// <param name="templateObj">The template object</param>
		/// <param name="pos">The position to align with (or null for default)</param>
		/// <param name="rot">The rotation to rotate by (or null for default)</param>
		public void AddPlotObject(GameObject templateObj, Vector3? pos, Vector3? rot)
		{
			GameObject obj = Object.Instantiate(templateObj, Prefab.transform);
			obj.transform.position = pos ?? Vector3.one;
			obj.transform.rotation = Quaternion.Euler(rot ?? Vector3.one);
		}
	}
}
