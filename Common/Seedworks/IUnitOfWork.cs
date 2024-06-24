using Microsoft.EntityFrameworkCore.Storage;

namespace clothes.api.Common.Seedworks
{
    public interface IUnitOfWork
    {
        IDbContextTransaction Begin();
        void Complete();
        void Rollback();
    }
}
