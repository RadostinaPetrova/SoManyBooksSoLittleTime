namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;

    public interface IGenresService
    {
        IEnumerable<T> GetAllPopular<T>();
    }
}
