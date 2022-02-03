using System.Threading.Tasks;

namespace Store.Shared.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
