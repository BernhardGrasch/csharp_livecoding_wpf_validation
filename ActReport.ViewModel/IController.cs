namespace ActReport.ViewModel
{
  public interface IController
  {
    void ShowWindow(BaseViewModel viewModel, bool showAsDialog);
    void CloseWindow(BaseViewModel viewModel);
  }
}
