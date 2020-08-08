using SRML.Console;

namespace VikDisk.SRML.Debug
{
	/// <summary>
	/// A command to teleport
	/// </summary>
	public class TPCommand : ConsoleCommand
	{
		public override bool Execute(string[] args)
		{
			if (ArgsOutOfBounds(args.Length, 3, 3))
				return false;

			SceneContext.Instance.Player.transform.position = 
				new UnityEngine.Vector3(float.Parse(args[0]), float.Parse(args[1]), float.Parse(args[2]));

			return true;
		}

		public override string ID { get; } = "tp";
		public override string Usage { get; } = "tp <x> <y> <z>";
		public override string Description { get; } = "Teleports to a given location";
	}
}
