using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluePrintSys.DataAccess;

namespace BluePrintSys.Core.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Employee> Employees { get; }

        IGenericRepository<Position> Positions { get; }

        Task Save();

    }

}
