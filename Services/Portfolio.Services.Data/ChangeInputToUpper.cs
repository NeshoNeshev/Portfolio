namespace Portfolio.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ChangeInputToUpper<TEntity> : IChangeInputToUpper<TEntity>
        where TEntity : class
    {
        public T ToUpper<T>(T value, ICollection<string> forbidden)
        {
            var t = value.GetType();
            var properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(c => c.PropertyType == typeof(string));
            foreach (var propertyInfo in properties)
            {
                var newValue = (string)propertyInfo.GetValue(value);
                if (forbidden.Contains(newValue))
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(newValue))
                {
                    newValue = newValue.ToUpper();
                }

                propertyInfo.SetValue(value, newValue);
            }

            return value;
        }
    }
}
