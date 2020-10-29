namespace ActReport.ViewModel.Contacts
{
  public interface IController
  {
    void ShowWindow(BaseViewModel viewModel, bool showAsDialog = false);
    void CloseWindow(BaseViewModel viewModel);
  }
}
