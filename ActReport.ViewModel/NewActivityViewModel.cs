using ActReport.Core.Contracts;
using ActReport.Core.Entities;
using ActReport.Persistence;
using ActReport.ViewModel.Contacts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        Validate();
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
      Validate();

      if (IsValid)
      {
        using IUnitOfWork uow = new UnitOfWork();
        #region UoW-Code

        Activity.Employee_Id = _employee.Id;
        Activity.ActivityText = ActivityText;
        await uow.ActivityRepository.InsertAsync(Activity);

        #endregion

        try
        {
          await uow.SaveAsync();
          _controller.CloseWindow(this);
        }
        catch (ValidationException validationException)
        {
          if (validationException.Value is List<string> properties)
          {
            foreach (var property in properties)
            {
              AddErrorsToProperty(property, new List<string> { validationException.ValidationResult.ErrorMessage });
            }
          }
          else
          {
            DbError = validationException.ValidationResult.ToString();
          }
        }
      }
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
      if (!string.IsNullOrEmpty(ActivityText) && ActivityText.Length <= 3)
      {
        yield return new ValidationResult("Der Name der Aktivität muss mind. 3 Zeichen lang sein!", new string[] { "ActivityText" });
      }

      if (string.IsNullOrEmpty(ActivityText))
      {
        yield return new ValidationResult("Der Name der Aktivität muss angegeben werden!", new string[] { "ActivityText" });
      }

    }
  }
}
