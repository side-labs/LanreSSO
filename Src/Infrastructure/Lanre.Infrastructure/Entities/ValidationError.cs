namespace Lanre.Infrastructure.Entities
{
    public class ValidationError
    {
        public ValidationError(string field, string key)
        {
            this.Field = field != string.Empty ? field : null;
            this.Key = key;
        }

        public string Field { get; }

        public string Key { get; }
    }
}