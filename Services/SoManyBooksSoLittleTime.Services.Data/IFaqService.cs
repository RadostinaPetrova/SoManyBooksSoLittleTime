namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;

    public interface IFaqService
    {
        IEnumerable<T> GetAll<T>();
    }
}
