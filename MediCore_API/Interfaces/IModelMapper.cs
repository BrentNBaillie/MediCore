namespace MediCore_API.Interfaces
{
	public interface IModelMapper
	{
		public TDestination Map<TSource, TDestination>(TSource source);
	}
}
