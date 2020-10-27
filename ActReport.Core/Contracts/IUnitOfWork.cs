using ActReport.Core.Entities;
using System;
using System.Threading.Tasks;

namespace ActReport.Core.Contracts
{
  public interface IUnitOfWork : IDisposable
  {
    /// <summary>
    /// Standard Repositories 
    /// </summary>
    IGenericRepository<Activity> ActivityRepository { get; }
    IGenericRepository<Employee> EmployeeRepository { get; }

    /// <summary>
    /// Erweiterte Repositories
    /// </summary>
    //IAddressRepository AddressRepository { get; }

    Task SaveAsync();

    Task DeleteDatabaseAsync();
    Task MigrateDatabaseAsync();

    Task FillDbAsync();
  }
}
