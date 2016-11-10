using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictonary.Commands
{
	public interface IAsyncCommand : ICommand
	{
		Task ExecuteAsync(object parameter);
	}
}
