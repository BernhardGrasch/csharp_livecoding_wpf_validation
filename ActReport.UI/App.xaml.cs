using ActReport.ViewModel;
using ActReport.ViewModel.Contacts;
using System.Windows;

namespace ActReport.UI
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private async void Application_Startup(object sender, StartupEventArgs e)
    {
      IController controller = new MainController();
      EmployeeViewModel viewModel = await EmployeeViewModel.CreateAsync(controller);
      controller.ShowWindow(viewModel);
    }
  }
}
