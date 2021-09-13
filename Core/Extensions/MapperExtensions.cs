using Core.Utilities.IoC;
using Core.Utilities.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class MapperExtensions
    {
        private static readonly IMapper _mapper;

        static MapperExtensions()
        {
            _mapper = ServiceTool.ServiceProvider.GetService<IMapper>();
        }

        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }

        public static TDestination Map<TDestination>(this object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public static object Map(this object source)
        {
            return _mapper.Map(source);
        }
    }
}