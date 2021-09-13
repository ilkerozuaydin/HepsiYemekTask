namespace Core.Utilities.Mapper
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        object Map<TSource>(TSource source, object destination);

        TDestination Map<TDestination>(object source);
    }
}