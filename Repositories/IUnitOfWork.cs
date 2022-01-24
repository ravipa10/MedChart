using System.Threading.Tasks;

namespace MedChart.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}