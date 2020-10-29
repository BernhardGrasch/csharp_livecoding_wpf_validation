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
    private void Application_Startup(object sender, StartupEventArgs e)
    {
      IController controller = new MainController();
      controller.ShowWindow(new EmployeeViewModel(controller));
    }
  }
}
