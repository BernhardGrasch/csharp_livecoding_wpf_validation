using ActReport.Core.Contracts;
using ActReport.Core.Entities;
using ActReport.Persistence;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;

namespace ActReport.ViewModel
{
  public class ActivityViewModel : BaseViewModel
  {
    private readonly Employee _employee;
    private ObservableCollection<Activity> _activities;
    private Activity _currentActivity;

    private ICommand _cmdNewActivityCommand;
    public ICommand CmdNewActivityCommand
    {
      get
      {
        if (_cmdNewActivityCommand == null)
        {
          _cmdNewActivityCommand = new RelayCommand(
            execute: _ =>
            {
              _controller.ShowWindow(new NewActivityViewModel(_controller, _employee));
              LoadActivities();
            },
            canExecute: _ => true);
        }
        return _cmdNewActivityCommand;
      }
    }

    private ICommand _cmdEditActivityCommand;
    public ICommand CmdEditActivityCommand
    {
      get
      {
        if (_cmdEditActivityCommand == null)
        {
          _cmdEditActivityCommand = new RelayCommand(
            execute: _ => _controller.ShowWindow(new EditActivityViewModel(_controller)),
            canExecute: _ => CurrentActivity != null);
        }
        return _cmdEditActivityCommand;
      }
    }

    private ICommand _cmdDeleteActivityCommand;
    public ICommand CmdDeleteActivityCommand
    {
      get
      {
        if (_cmdDeleteActivityCommand == null)
        {
          _cmdDeleteActivityCommand = new RelayCommand(
            execute: _ => DeleteActivity(CurrentActivity),
            canExecute: _ => CurrentActivity != null);
        }
        return _cmdDeleteActivityCommand;
      }
    }

    private void DeleteActivity(Activity currentActivity)
    {
      using IUnitOfWork uow = new UnitOfWork();
      uow.ActivityRepository.Delete(currentActivity);
      uow.Save();

      LoadActivities();
    }

    public ObservableCollection<Activity> Activities
    {
      get => _activities;
      set
      {
        _activities = value;
        OnPropertyChanged(nameof(Activities));
      }
    }

    public Activity CurrentActivity
    {
      get => _currentActivity;
      set
      {
        _currentActivity = value;
        OnPropertyChanged(nameof(CurrentActivity));
      }
    }

    public string FullName => $"{_employee.FirstName} {_employee.LastName}";

    public ActivityViewModel(IController controller, Employee employee) : base(controller)
    {
      _employee = employee;
      LoadActivities();
    }

    private void LoadActivities()
    {
      IUnitOfWork uow = new UnitOfWork();
      Activities = new ObservableCollection<Activity>(uow.ActivityRepository.Get(
        filter: x => x.Employee_Id == _employee.Id,
        orderBy: coll => coll.OrderBy(activity => activity.Date).ThenBy(activity => activity.StartTime)));

      Activities.CollectionChanged += Activities_CollectionChanged;
    }

    private void Activities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        using IUnitOfWork uow = new UnitOfWork();
        foreach (var item in e.OldItems)
        {
          uow.ActivityRepository.Delete((item as Activity).Id);
        }
        uow.Save();
      }
    }

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      return new ValidationResult[0];
    }
  }
}
