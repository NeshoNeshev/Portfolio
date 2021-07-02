using System.Collections.Generic;

namespace Portfolio.Services.Data
{
    public interface IChangeInputToUpper<TEntity>
        where TEntity : class
    {
        T ToUpper<T>(T value, ICollection<string> forbidden);

    }
}
