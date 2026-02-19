namespace ProductCatalogManagement.Application.Search
{
    public class SearchCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _cache = new();

        public bool TryGet(TKey key, out TValue value)
            => _cache.TryGetValue(key, out value!);

        public void Set(TKey key, TValue value)
            => _cache[key] = value;
    }
}
