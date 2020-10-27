using ActReport.Persistence;
using System;
using System.Threading.Tasks;

namespace ActReport.ConsoleFillDb
{
  class Program
  {
    static async Task Main()
    {
      using UnitOfWork uow = new UnitOfWork();

      await uow.FillDbAsync();
      var res = await uow.EmployeeRepository.GetAsync();
      foreach (var emp in res)
      {
        Console.WriteLine(emp.LastName);
      }

      Console.WriteLine("<Eingabe drücken>");
      Console.ReadLine();
    }
  }
}
