using SRML;
using SRML.Utils;
using UnityEngine;

namespace Guu.API.Plots
{
	/// <summary>
	/// This is the base class to make corrals
	/// </summary>
	public abstract class CorralPlot : PlotItem
	{
		// Base item to create this one
		private static GameObject BaseItem => GameContext.Instance.LookupDirector?.GetPlotPrefab(LandPlot.Id.CORRAL) ?? 
		                                      SRObjects.Get<GameObject>("patchCorral");

		// Methods
		protected override void Build()
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
