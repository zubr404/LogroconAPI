using LogroconAPI.Models;
using LogroconAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace LogroconAPI.Services
{
    public class UnitOfWork : IDisposable
    {
        readonly DataBaseContext db;
        public UnitOfWork(DataBaseContext context)
        {
            db = context;
        }

        private EmployeeRepository employeeRepository;
        public EmployeeRepository EmployeeRepository
        {
            get
            {
                return employeeRepository ?? (employeeRepository = new EmployeeRepository(db));
            }
        }

        private PositionRepository positionRepository;
        public PositionRepository PositionRepository
        {
            get
            {
                return positionRepository ?? (positionRepository = new PositionRepository(db));
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
