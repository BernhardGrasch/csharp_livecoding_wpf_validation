using ActReport.Core.Contracts;
using ActReport.Core.Entities;
using ActReport.Persistence;
using System;
using System.Windows.Input;

namespace ActReport.ViewModel
{
  public class NewActivityViewModel : BaseViewModel
  {
    private string _activityText;

    private readonly Employee _employee;
    public Activity Activity { get; }

    public string ActivityText
    {
      get => _activityText;
      set
      {
        _activityText = value;
        OnPropertyChanged();
        //Validate();
      }
    }

    private ICommand _cmdSaveActivityCommand;
    public ICommand CmdSaveActivityCommand
    {
      get
      {
        if (_cmdSaveActivityCommand == null)
        {
          _cmdSaveActivityCommand = new RelayCommand(
            execute: _ => SaveNewActivity(),
            canExecute: _ => IsSaveAllowed());
        }

        return _cmdSaveActivityCommand;
      }
    }

    private bool IsSaveAllowed() => !string.IsNullOrEmpty(_activityText);

    private void SaveNewActivity()
    {
      using IUnitOfWork uow = new UnitOfWork();
      #region UoW-Code

      Activity.Employee_Id = _employee.Id;
      Activity.ActivityText = ActivityText;
      uow.ActivityRepository.Insert(Activity);

      #endregion

      uow.Save();

      _controller.CloseWindow(this);
    }

    private ICommand _cmdCloseWindowCommand;
    public ICommand CmdCloseWindowCommand
    {
      get
      {
        if (_cmdCloseWindowCommand == null)
        {
          _cmdCloseWindowCommand = new RelayCommand(
            execute: _ => _controller.CloseWindow(this),
            canExecute: _ => true);
        }
        return _cmdCloseWindowCommand;
      }
    }

    public NewActivityViewModel(IController controller, Employee employee) : base(controller)
    {
      _employee = employee;
      Activity = new Activity()
      {
        StartTime = DateTime.Now.Date.AddHours(8),
        EndTime = DateTime.Now.Date.AddHours(16)
      };

      //Validate();
    }

    //public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //  if (!string.IsNullOrEmpty(ActivityText) && ActivityText.Length <= 3)
    //  {
    //    yield return new ValidationResult("Der Name der Aktivität muss mind. 3 Zeichen lang sein!", new string[] { "ActivityText" });
    //  }

    //  if (string.IsNullOrEmpty(ActivityText))
    //  {
    //    yield return new ValidationResult("Der Name der Aktivität muss angegeben werden!", new string[] { "ActivityText" });
    //  }

    //}
  }
}
