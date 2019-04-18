using System.Reflection;
using Autofac;
using Xamarin.Forms;

namespace Evidences.ViewModel.Base
{
    public class ViewModelLocator
    {
        public static readonly BindableProperty ViewModelNameProperty =
                        BindableProperty.CreateAttached("ViewModelName", typeof(string), typeof(ViewModelLocator), default(string), propertyChanged: OnViewModelNameChanged);

        public static string GetViewModelName(BindableObject bindable)
        {
            return (string)bindable.GetValue(ViewModelNameProperty);
        }

        public static void SetViewModelName(BindableObject bindable, string value)
        {
            bindable.SetValue(ViewModelNameProperty, value);
        }

        private static void OnViewModelNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewModelType = Assembly.GetAssembly(typeof(ViewModelLocator)).GetType((string)newValue);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = App.Container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}