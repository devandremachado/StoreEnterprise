using System.Threading.Tasks;

namespace Store.Shared.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
