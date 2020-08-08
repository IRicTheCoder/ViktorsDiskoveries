namespace Guu.API
{
	/// <summary>
	/// Represents a Registry Item, it is a base for all
	/// items that can be registered.
	/// </summary>
	/// <typeparam name="T">The type of RegistryItem</typeparam>
	public abstract class RegistryItem<T> where T : RegistryItem<T>
	{
		/// <summary>The name of this object</summary>
		public abstract string Name { get; }

		/// <summary>Builds this Item</summary>
		protected abstract void Build();

		/// <summary>Registers the item into it's registry</summary>
		public abstract T Register();
	}
}
