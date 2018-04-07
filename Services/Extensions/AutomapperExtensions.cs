namespace Services.Extensions
{
    static class AutomapperExtensions
    {
        public static T MapTo<T>(this object entity) =>
            AutoMapper.Mapper.Map<T>(entity);
    }
}
