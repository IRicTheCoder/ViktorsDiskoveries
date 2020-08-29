namespace VikDisk.Core
{
    internal abstract class Injector
    {
        internal virtual void SetupMod() { }
        internal virtual void PopulateMod() { }
        internal virtual void IntermodComms() { }
    }
}