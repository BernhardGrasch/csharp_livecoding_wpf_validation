using System;
using ActReport.Core.Contracts;
using ActReport.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ActReport.Persistence
{
  public class UnitOfWork : IUnitOfWork
  {

    private readonly ApplicationDbContext _context = new ApplicationDbContext();
    private bool _disposed;


    /// <summary>
    ///     Konkrete Standard-Repositories. Keine Ableitung nötig
    /// </summary>
    public IGenericRepository<Activity> ActivityRepository { get; }
    public IGenericRepository<Employee> EmployeeRepository { get; }


    /// <summary>
    ///     Konkrete Repositories. Mit Ableitung nötig
    /// </summary>
    //  public IAddressRepository AddressRepository { get; }



    public UnitOfWork()
    {
      _context = new ApplicationDbContext();

      ActivityRepository = new GenericRepository<Activity>(_context);
      EmployeeRepository = new GenericRepository<Employee>(_context);

      //Bsp.: Konkretes Repository mit Überschreibung
      //AddressRepository = new AddressRepository(_context);
    }

    public void Save()
    {
      var entities = _context.ChangeTracker.Entries()
          .Where(entity => entity.State == EntityState.Added
                           || entity.State == EntityState.Modified)
          .Select(e => e.Entity);
      foreach (var entity in entities)
      {
        ValidateEntity(entity);
      }
      _context.SaveChanges();
    }

    /// <summary>
    /// Validierungen durchführen
    /// </summary>
    /// <param name="entity"></param>
    private void ValidateEntity(object entity)
    {
      if (entity is Activity activity)
      {
        if (_context.Activities.Any(a => a.Id != activity.Id 
              && a.Employee_Id == activity.Employee_Id
              && a.ActivityText == activity.ActivityText))
        {
          throw new ValidationException(
            $"Der Mitarbeiter hat bereits eine derartige Aktivität eingebucht!", 
            null, new string[] { "ActivityName" });
        }
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposed)
      {
        if (disposing)
        {
          _context.Dispose();
        }
      }
      _disposed = true;
    }

    public void DeleteDatabase()
    {
      _context.Database.EnsureDeleted();
    }

    public void MigrateDatabase()
    {
      try
      {
        _context.Database.Migrate();
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public void FillDb()
    {

      DeleteDatabase();
      MigrateDatabase();


      Employee emp = new Employee() { FirstName = "Max", LastName = "Mustermann" };
      _context.Employees.Add(emp);
      _context.Employees.Add(new Employee() { FirstName = "Sarah", LastName = "Aigner" });
      _context.Activities.Add(new Activity() { ActivityText = "Vorbereitung Schulung", Date = Convert.ToDateTime("10.10.2017"), StartTime = DateTime.Parse("01.01.1900 12:00:00"), EndTime = DateTime.Parse("01.01.1900 14:00:00"), Employee = emp });
      _context.Activities.Add(new Activity() { ActivityText = "Durchführung Schulung", Date = Convert.ToDateTime("10.10.2017"), StartTime = DateTime.Parse("01.01.1900 14:00:00"), EndTime = DateTime.Parse("01.01.1900 17:00:00"), Employee = emp });
      _context.SaveChanges();

    }

  }
}
