using LogroconAPI.Models;
using LogroconAPI.ModelsDTO;
using LogroconAPI.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace LogroconAPI.Services
{
    public class PositionService : IDtoService<PositionDto>
    {
        readonly PositionRepository db;
        public PositionService(UnitOfWork unitOfWork)
        {
            db = unitOfWork.PositionRepository;
        }
        public async Task<PositionDto> Create(PositionDto item)
        {
            if (item != null)
                await db.Create(new Position() { Name = item.Name, Grad = item.Grad }, item.Employees.Select(x => x.ID).ToList());
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            return await db.Delete(id);
        }

        public async Task<PositionDto> Get(int id)
        {
            var position = await db.Get(id);

            PositionDto result = null;
            if (position != null)
            {
                result = new PositionDto()
                {
                    ID = position.ID,
                    Name = position.Name,
                    Grad = position.Grad
                };
                foreach (var item in position.EmployeePositions)
                {
                    if (item.Position != null)
                    {
                        var employeeDto = new EmployeeDto()
                        {
                            ID = item.Employee.ID,
                            FullName = item.Employee.FullName,
                            Birthdate = item.Employee.Birthdate
                        };
                        result.Employees.Add(employeeDto);
                    }
                }
            }
            return result;
        }

        public async Task Update(PositionDto item)
        {
            Position result = null;
            if (item != null)
            {
                result = new Position()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Grad = item.Grad
                };
                foreach (var employee in item.Employees)
                {
                    result.EmployeePositions.Add(new EmployeePosition() { EmployeeId = employee.ID, PositionId = result.ID });
                }
            }

            await db.Update(result);
        }
    }
}
