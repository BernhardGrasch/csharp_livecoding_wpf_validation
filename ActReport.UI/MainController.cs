using ActReport.ViewModel;
using ActReport.ViewModel.Contracts;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ActReport.UI
{
  public class MainController : IController
  {
    // Speicherung der Zuordnung zw. ViewModel und Window
    private Dictionary<BaseViewModel, Window> _windows;

    public MainController()
    {
      _windows = new Dictionary<BaseViewModel, Window>();
    }

    public void ShowWindow(BaseViewModel viewModel, bool showAsDialog)
    {
      Window window = viewModel switch
      {
        // Wenn viewModel null ist -> ArgumentNullException
        null => throw new ArgumentNullException(nameof(viewModel)),

        // Das dem Type des ViewModel entsprechende Window instanzieren
        EmployeeViewModel _ => new MainWindow(),
        ActivityViewModel _ => new ActivityWindow(),
        NewActivityViewModel _ => new NewActivityWindow(),
        EditActivityViewModel _ => new EditActivityWindow(),

        // default -> InvalidOperationException
        _ => throw new InvalidOperationException($"Unknown ViewModel of type '{viewModel}'"),
      };

      _windows[viewModel] = window;
      window.DataContext = viewModel;

      if (showAsDialog)
      {
        window.ShowDialog();
      }
      else
      {
        window.Show();
      }
    }

    public void CloseWindow(BaseViewModel viewModel)
    {
      if (_windows.ContainsKey(viewModel))
      {
        _windows[viewModel].Close();
        _windows.Remove(viewModel);
      }
    }
  }
}


