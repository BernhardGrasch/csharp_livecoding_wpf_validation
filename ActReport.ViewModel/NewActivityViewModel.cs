using ActReport.Core.Contracts;
using ActReport.Core.Entities;
using ActReport.Persistence;
using ActReport.ViewModel.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
            execute: async _ => await SaveNewActivity(),
            canExecute: _ => IsSaveAllowed());
        }

        return _cmdSaveActivityCommand;
      }
    }

    private bool IsSaveAllowed() => IsValid;

    private async Task SaveNewActivity()
    {
      using IUnitOfWork uow = new UnitOfWork();
      #region UoW-Code

      Activity.Employee_Id = _employee.Id;
      Activity.ActivityText = ActivityText;
      await uow.ActivityRepository.InsertAsync(Activity);

      #endregion

      await uow.SaveAsync();

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

      Validate();
    }

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      return Enumerable.Empty<ValidationResult>();
    }
  }
}
