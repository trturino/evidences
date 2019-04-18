using System.Runtime.CompilerServices;
using Evidences.Services;
using Xamarin.Forms;

namespace Evidences.ViewModel
{
    public class BaseViewModel : BindableObject
    {
        public BaseViewModel(IStateService stateService)
        {
            StateService = stateService;
        }

        public IStateService StateService { get; }

        public void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            OnPropertyChanged(name);
        }
    }
}