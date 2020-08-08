using SRML.SR.Utils.BaseObjects;
using UnityEngine;

namespace VikDisk.SRML.Debug
{
	/// <summary>
	/// Displays the mesh for a Marker
	/// </summary>
	public class MarkerMesh : MonoBehaviour
	{
		/// <summary>The material used by markers</summary>
		public static Material FadeMat { get; } = new Material(Shader.Find("Standard")).Initialize((mat) =>
		{
			mat.SetFloat("_Mode", 2);
			mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			mat.SetInt("_ZWrite", 0);
			mat.DisableKeyword("_ALPHATEST_ON");
			mat.EnableKeyword("_ALPHABLEND_ON");
			mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			mat.renderQueue = 3000;
		}).SetInfo(new Color(0, 0, 0, 1f), "Base Marker Material");

		/// <summary>The mesh renderer for this mesh</summary>
		public MeshRenderer Render { get; private set; }

		/// <summary>
		/// Sets up this marker mesh
		/// </summary>
		public void Setup(MarkerMaterial mat)
		{
			GetComponent<MeshRenderer>().sharedMaterial = mat.Material;
		}

		// Updates the mesh
		private void Update()
		{
			if (Render == null)
				Render = GetComponent<MeshRenderer>();

			Render.enabled = MarkerController.ShowMarkers;
		}

		/// <summary>A material representation for markers</summary>
		public struct MarkerMaterial
		{
			/// <summary>The marker material for foods</summary>
			public static MarkerMaterial Generic { get; } = new MarkerMaterial(new Color(0, 0, 0, 0.5f), "Generic Marker");

			/// <summary>The actual material</summary>
			public Material Material { get; private set; }

			/// <summary>
			/// Creates a new Marker Material
			/// </summary>
			/// <param name="color">Color for the material</param>
			/// <param name="name">Name for the material</param>
			public MarkerMaterial(Color color, string name)
			{
				Material = new Material(FadeMat).SetInfo(color, name);
			}
		}
	}
}
