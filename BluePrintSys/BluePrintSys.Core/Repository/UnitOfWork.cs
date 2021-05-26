using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluePrintSys.Core.IRepository;
using BluePrintSys.DataAccess;


namespace BluePrintSys.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlueprintsysContext _context; 
        private IGenericRepository<Employee> _employees;
        private IGenericRepository<Position> _positions;

        public UnitOfWork(BlueprintsysContext context)
        {
            _context = context;
        }


        public IGenericRepository<Employee> Employees => _employees ??= new GenericRepository<Employee>(_context);


        public IGenericRepository<Position> Positions => _positions ??= new GenericRepository<Position>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }

}
