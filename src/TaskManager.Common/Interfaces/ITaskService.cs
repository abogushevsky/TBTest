using System.Threading.Tasks;

namespace TaskManager.Common.Interfaces
{
    public interface ITaskService
    {
        Task<string> AddTask(Task task);
    }
}