namespace Mizore.ContentSerializer
{
    public interface IContentSerializerFactory
    {
        IContentSerializer DefaultSerializer { get; }

        IContentSerializer GetContentSerializer(string type = null);

        void RegisterContentSerializer(IContentSerializer serializer);
    }
}