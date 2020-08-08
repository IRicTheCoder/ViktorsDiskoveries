using UnityEngine;

namespace SRML.Debug
{
	/// <summary>
	/// A marker for largos
	/// </summary>
	public class LargoMarker : Marker, IMarkerTarget<SlimeDefinition>
	{
		/// <summary>The target for the marker</summary>
		public SlimeDefinition Target { get; private set; }

		/// <summary>Sets up this marker</summary>
		public override void Setup()
		{
			Identifiable id = gameObject.GetComponent<Identifiable>();
			if (id == null)
				id = gameObject.GetComponentInChildren<Identifiable>();

			Target = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(id.id);
		}

		/// <summary>Draws the Gizmo itself</summary>
		public override void DrawGizmo(Vector2 position)
		{
			Texture2D secIcon = GameContext.Instance.LookupDirector.GetIcon(Target.BaseSlimes[1].IdentifiableId).texture;

			position += (ICON_SIZE / 2);
			Rect rect = new Rect(position, ICON_SIZE / 1.5f);
			GUI.DrawTexture(rect, secIcon);
		}

		/// <summary>The icon for the marker</summary>
		public override Texture2D GetIcon()
		{
			return GameContext.Instance.LookupDirector.GetIcon(Target.BaseSlimes[0].IdentifiableId).texture;
		}
	}
}
