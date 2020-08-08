
namespace VikDisk.SRML.Debug
{
	/// <summary>A representation of a marker</summary>
	public interface IMarkerTarget<T>
	{
		/// <summary>The target for the marker</summary>
		T Target { get; }
	}
}
