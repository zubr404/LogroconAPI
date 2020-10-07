using LogroconAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogroconAPI.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        readonly DataBaseContext db;
        public EmployeeRepository(DataBaseContext context)
        {
            this.db = context;
        }

        public async Task<Employee> Create(Employee item, List<int> navigationsId)
        {
            if (item != null)
            {
                db.Employees.Add(item);
                await db.SaveChangesAsync();

                if (navigationsId != null)
                {
                    foreach (var id in navigationsId)
                    {
                        item.EmployeePositions.Add(new EmployeePosition() { EmployeeId = item.ID, PositionId = id });
                    }
                }
                await db.SaveChangesAsync();
            }
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var employee = db.Employees.SingleOrDefault(x => x.ID == id);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Employee> Get(int id)
        {
            var employee = await db.Employees
                .Include(employee => employee.EmployeePositions)
                .ThenInclude(x => x.Position)
                .SingleOrDefaultAsync(x => x.ID == id);

            return employee;
        }

        public async Task Update(Employee item)
        {
            if(item != null)
            {
                var employee = await db.Employees.Include(employee => employee.EmployeePositions).SingleOrDefaultAsync(x => x.ID == item.ID);

                if (employee != null)
                {
                    employee.FullName = item.FullName;
                    employee.Birthdate = item.Birthdate;
                    employee.EmployeePositions = item.EmployeePositions;
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
