using ActReport.ViewModel;
using ActReport.ViewModel.Contracts;
using System.Windows;

namespace ActReport.UI
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private void Application_Startup(object sender, StartupEventArgs e)
    {
      IController controller = new MainController();
      var viewModel = new EmployeeViewModel(controller);
      controller.ShowWindow(viewModel);

      Dispatcher.Invoke(async () => await viewModel.InitAsync());
    }
  }
}
