using Mapster;

namespace Core.Utilities.Mapper.Mapster
{
    public class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return source.Adapt<TSource, TDestination>(destination);
        }

        public object Map<TSource>(TSource source, object destination)
        {
            return source.Adapt(destination);
        }

        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>();
        }
    }
}