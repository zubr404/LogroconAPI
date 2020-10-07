using LogroconAPI.Models;
using LogroconAPI.ModelsDTO;
using LogroconAPI.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace LogroconAPI.Services
{
    public class EmployeeService : IDtoService<EmployeeDto>
    {
        readonly EmployeeRepository db;
        public EmployeeService(UnitOfWork unitOfWork)
        {
            db = unitOfWork.EmployeeRepository;
        }

        public async Task<EmployeeDto> Create(EmployeeDto item)
        {
            if(item != null)
                await db.Create(new Employee() { FullName = item.FullName, Birthdate = item.Birthdate}, item.Positions.Select(x => x.ID).ToList());
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            return await db.Delete(id);
        }

        public async Task<EmployeeDto> Get(int id)
        {
            var employee = await db.Get(id);

            EmployeeDto result = null;
            if (employee != null)
            {
                result = new EmployeeDto()
                {
                    ID = employee.ID,
                    FullName = employee.FullName,
                    Birthdate = employee.Birthdate
                };
                foreach (var item in employee.EmployeePositions)
                {
                    if (item.Position != null)
                    {
                        var positionDto = new PositionDto()
                        {
                            ID = item.Position.ID,
                            Name = item.Position.Name,
                            Grad = item.Position.Grad
                        };
                        result.Positions.Add(positionDto);
                    }
                }
            }
            return result;
        }

        public async Task Update(EmployeeDto item)
        {
            Employee result = null;
            if (item != null)
            {
                result = new Employee()
                {
                    ID = item.ID,
                    FullName = item.FullName,
                    Birthdate = item.Birthdate
                };
                foreach (var position in item.Positions)
                {
                    result.EmployeePositions.Add(new EmployeePosition() { EmployeeId = result.ID, PositionId = position.ID });
                }
            }

            await db.Update(result);
        }
    }
}
