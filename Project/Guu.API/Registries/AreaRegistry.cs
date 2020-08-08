using System;
using System.Collections.Generic;
using MonomiPark.SlimeRancher.Regions;
using SRML.Areas;
using UnityEngine;

namespace SRML.Registries
{
	// TODO: Area Registry, check if works, build the callbacks
	/// <summary>
	/// The registry for areas
	/// </summary>
	public static class AreaRegistry
	{
		/// <summary>The class that contains the info on an Area</summary>
		public class Area
		{
			public Vector3 position;
			public GameObject area;
			public Action<GameObject> spawnAction;
			public Action<ZoneDirector> directorSetup;
			public Action<CellDirector, Region> cellSetup;
		}

		/// <summary>The class that contains the info on an Area Cell</summary>
		public class AreaCell
		{
			public ZoneDirector.Zone zone;
			public Vector3 position;
			public GameObject cell;
			public Action<GameObject> spawnAction;
			public Action<CellDirector, Region> cellSetup;
		}

		/// <summary>The class that contains the info on Area Cell Content</summary>
		public class AreaCellContent
		{
			public ZoneDirector.Zone zone;
			public string cellName;
			public Vector3 position;
			public GameObject content;
			public Action<GameObject> spawnAction;
		}

		/// <summary>List of areas registered</summary>
		public static List<Area> Areas { get; } = new List<Area>();

		/// <summary>List of area cells registered</summary>
		public static List<AreaCell> AreaCells { get; } = new List<AreaCell>();

		/// <summary>List of area cell content registered</summary>
		public static List<AreaCellContent> CellContent { get; } = new List<AreaCellContent>();

		/// <summary>
		/// Registers a new area
		/// </summary>
		/// <param name="pos">The position to spawn the area in</param>
		/// <param name="area">The area to spawn</param>
		/// <param name="spawn">The action to run for each marker after spawn</param>
		/// <param name="director">The action to run when building the Zone Director</param>
		/// <param name="cell">The action to run when building each Cell Director</param>
		/// <returns>The area registered</returns>
		public static Area RegisterArea(Vector3 pos, GameObject area, Action<GameObject> spawn, Action<ZoneDirector> director, Action<CellDirector, Region> cell)
		{
			Area item = new Area()
			{
				position = pos,
				area = area,
				spawnAction = spawn,
				directorSetup = director,
				cellSetup = cell
			};

			Areas.Add(item);

			return item;
		}

		/// <summary>
		/// Registers a new area cell
		/// </summary>
		/// <param name="zone">The zone to add this cell to</param>
		/// <param name="pos">The position to spawn the area in</param>
		/// <param name="cellArea">The area to spawn</param>
		/// <param name="spawn">The action to run for each marker after spawn</param>
		/// <param name="cell">The action to run when building each Cell Director</param>
		/// <returns>The area cell registered</returns>
		public static AreaCell RegisterAreaCell(ZoneDirector.Zone zone, Vector3 pos, GameObject cellArea, Action<GameObject> spawn, Action<CellDirector, Region> cell)
		{
			AreaCell item = new AreaCell()
			{
				zone = zone,
				position = pos,
				cell = cellArea,
				spawnAction = spawn,
				cellSetup = cell
			};

			AreaCells.Add(item);

			return item;
		}

		/// <summary>
		/// Registers new area cell content
		/// </summary>
		/// <param name="zone">The zone to add this cell to</param>
		/// <param name="objName">The name of the cell to add to</param>
		/// <param name="pos">The position to spawn the area in</param>
		/// <param name="contentArea">The area to spawn</param>
		/// <param name="spawn">The action to run for each marker after spawn</param>
		/// <returns>The area cell content registered</returns>
		public static AreaCellContent RegisterAreaCellContent(ZoneDirector.Zone zone, string objName, Vector3 pos, GameObject contentArea, Action<GameObject> spawn)
		{
			AreaCellContent item = new AreaCellContent()
			{
				zone = zone,
				cellName = objName,
				position = pos,
				content = contentArea,
				spawnAction = spawn,
			};

			CellContent.Add(item);

			return item;
		}

		/// <summary>
		/// Spawns all areas in the reigstry list
		/// </summary>
		internal static void SpawnAreas()
		{
			// Register full areas
			foreach (Area area in Areas)
			{
				// Create object
				GameObject obj = UnityEngine.Object.Instantiate(area.area, area.position, Quaternion.identity);
				obj.name = area.area.name;

				// Setup Zone Director
				ZoneDirector director = obj.AddComponent<ZoneDirector>();
				area.directorSetup?.Invoke(director);

				// Setup Each Cell
				foreach (Transform child in obj.transform)
				{
					if (area.cellSetup == null)
						break;

					if (!child.name.StartsWith("cell"))
						continue;

					CellDirector cell = child.gameObject.AddComponent<CellDirector>();
					Region region = child.gameObject.AddComponent<Region>();
					child.gameObject.AddComponent<RegionInitializer>();

					area.cellSetup.Invoke(cell, region);
				}

				// Setup Each Marker
				foreach (AreaObjMarker marker in obj.GetComponentsInChildren<AreaObjMarker>())
				{
					if (area.spawnAction == null)
						break;

					GameObject mo = SRObjects.GetInst<GameObject>(marker.objName);
					mo.transform.parent = marker.transform.parent;
					mo.transform.localPosition = marker.transform.localPosition;

					if (!marker.runSpawnAction)
						continue;

					area.spawnAction.Invoke(mo);
				}
			}

			// Register area cells
			foreach (AreaCell area in AreaCells)
			{
				// Create object
				GameObject obj = UnityEngine.Object.Instantiate(area.cell, area.position, Quaternion.identity, ZoneDirector.zones[area.zone].transform);
				obj.name = area.cell.name;

				// Setup Cell
				CellDirector cell = obj.AddComponent<CellDirector>();
				Region region = obj.AddComponent<Region>();
				obj.AddComponent<RegionInitializer>();

				area.cellSetup.Invoke(cell, region);

				// Setup Each Marker
				foreach (AreaObjMarker marker in obj.GetComponentsInChildren<AreaObjMarker>())
				{
					if (area.spawnAction == null)
						break;

					GameObject mo = SRObjects.GetInst<GameObject>(marker.objName);
					mo.transform.parent = marker.transform.parent;
					mo.transform.localPosition = marker.transform.localPosition;

					if (!marker.runSpawnAction)
						continue;

					area.spawnAction.Invoke(mo);
				}
			}

			// Register area cell content
			foreach (AreaCellContent area in CellContent)
			{
				// Get parent cell
				Transform parentCell = ZoneDirector.zones[area.zone].transform.Find(area.cellName);
				if (parentCell == null)
					continue;

				// Create object
				GameObject obj = UnityEngine.Object.Instantiate(area.content, area.position, Quaternion.identity, parentCell.transform);
				obj.name = area.content.name;

				// Setup Each Marker
				foreach (AreaObjMarker marker in obj.GetComponentsInChildren<AreaObjMarker>())
				{
					if (area.spawnAction == null)
						break;

					GameObject mo = SRObjects.GetInst<GameObject>(marker.objName);
					mo.transform.parent = marker.transform.parent;
					mo.transform.localPosition = marker.transform.localPosition;

					if (!marker.runSpawnAction)
						continue;

					area.spawnAction.Invoke(mo);
				}
			}
		}
	}
}
