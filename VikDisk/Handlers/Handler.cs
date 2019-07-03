namespace VikDisk.Handlers
{
	/// <summary>
	/// Base class for handlers, contains some base methods and easy handler access
	/// </summary>
	public abstract class Handler<T> where T : Handler<T>, new()
	{
		/// <summary>
		/// Instance for the handler, auto generated
		/// </summary>
		public static T Instance { get; private set; } = new T();

		/// <summary>
		/// Sets this handler up, loading needed things and calling the required methods
		/// </summary>
		public abstract void Setup();
	}
}
