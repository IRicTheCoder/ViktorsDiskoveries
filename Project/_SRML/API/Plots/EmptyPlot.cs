using SRML.Utils;
using UnityEngine;

namespace VikDisk.SRML.API
{
	/// <summary>
	/// This is the base class to make empty or custom plots
	/// </summary>
	public abstract class EmptyPlot : PlotItem
	{
		/// <summary>The base item to use when creating the one</summary>
		public GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPlotPrefab(LandPlot.Id.EMPTY) ?? SRObjects.Get<GameObject>("patchEmpty");

		/// <summary>Builds this Item</summary>
		public override void Build()
		{
			// Get GameObjects
			Prefab = PrefabUtils.CopyPrefab(BaseItem);
			Prefab.name = NamePrefix + Name;

			GameObject uiAct = Prefab.transform.Find("techActivator/triggerActivate").gameObject;

			// Load Components
			LandPlot plot = Prefab.GetComponent<LandPlot>();
			UIActivator activator = uiAct.GetComponent<UIActivator>();

			// Setup Components
			plot.typeId = ID;
			activator.uiPrefab = UIPrefab ?? activator.uiPrefab;
		}
	}
}
