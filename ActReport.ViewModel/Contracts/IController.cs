namespace ActReport.ViewModel.Contracts
{
  public interface IController
  {
    void ShowWindow(BaseViewModel viewModel, bool showAsDialog = false);
    void CloseWindow(BaseViewModel viewModel);
  }
}
