using SRML.Console;
using SRML.Debug;

namespace Guu.Debug
{
	public class DumperCommand : ConsoleCommand
	{
		public override bool Execute(string[] args)
		{
			if (ArgsOutOfBounds(args.Length, 2, 2))
				return false;

			Dumper.DumpObject(args[1], System.Type.GetType(args[0]));

			return true;
		}

		public override string ID { get; } = "dumper";
		public override string Usage { get; } = "dumper <type> <name>";
		public override string Description { get; } = "Dumps an object with <name> of the given <type>";
	}
}
