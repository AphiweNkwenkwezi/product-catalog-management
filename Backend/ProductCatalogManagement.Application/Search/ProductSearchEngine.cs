namespace ProductCatalogManagement.Application.Search
{
    public class ProductSearchEngine<T>
    {
        public IEnumerable<T> Search(
            IEnumerable<T> items,
            string query,
            Func<T, string> primarySelector,
            Func<T, string>? secondarySelector = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                return items;

            query = query.ToLowerInvariant();

            var results = items
                .Select(item => new
                {
                    Item = item,
                    Score = CalculateScore(item, query, primarySelector, secondarySelector)
                })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Item);

            return results;
        }

        private int CalculateScore(
            T item,
            string query,
            Func<T, string> primarySelector,
            Func<T, string>? secondarySelector)
        {
            int score = 0;

            var primary = primarySelector(item)?.ToLowerInvariant() ?? string.Empty;

            if (primary.Contains(query))
            {
                score += 50;
            }
            else
            {
                int distance = LevenshteinDistance(primary, query);

                if (distance <= 2)
                {
                    score += 40 - (distance * 10);
                }
            }

            
            if (secondarySelector is not null)
            {
                var secondary = secondarySelector(item);

                if (secondary.Contains(query))
                    score += 20;
            }

            return score;
        }

        private int LevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return target.Length;

            if (string.IsNullOrEmpty(target))
                return source.Length;

            int[,] matrix = new int[source.Length + 1, target.Length + 1];

            for (int i = 0; i <= source.Length; i++)
                matrix[i, 0] = i;

            for (int j = 0; j <= target.Length; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= source.Length; i++)
            {
                for (int j = 1; j <= target.Length; j++)
                {
                    int cost = source[i - 1] == target[j - 1] ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(
                            matrix[i - 1, j] + 1,
                            matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost
                    );
                }
            }

            return matrix[source.Length, target.Length];
        }
    }
}
