using System.Threading.Tasks;

namespace ProjectWPF.Interfaces
{
    public interface IAsyncInitialization
    {
        /// <summary>
        /// The result of the asynchronous initialization of this instance.
        /// </summary>
        Task Initialization { get; }
    }
}
