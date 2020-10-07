using LogroconAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogroconAPI.Repositories
{
    public class PositionRepository : IRepository<Position>
    {
        readonly DataBaseContext db;
        public PositionRepository(DataBaseContext db)
        {
            this.db = db;
        }

        public async Task<Position> Create(Position item, List<int> navigationsId)
        {
            if (item != null)
            {
                db.Positions.Add(item);
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
            var position = await db.Positions
                .Include(position => position.EmployeePositions)
                .ThenInclude(x => x.Employee)
                .SingleOrDefaultAsync(x => x.ID == id);
            if (position != null && !position.EmployeePositions.Exists(x => x.EmployeeId > 0))
            {
                db.Positions.Remove(position);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Position> Get(int id)
        {
            var position = await db.Positions
                .Include(position => position.EmployeePositions)
                .ThenInclude(x => x.Employee)
                .SingleOrDefaultAsync(x => x.ID == id);

            return position;
        }

        public async Task Update(Position item)
        {
            if (item != null)
            {
                var position = await db.Positions.Include(position => position.EmployeePositions).SingleOrDefaultAsync(x => x.ID == item.ID);

                if (position != null)
                {
                    position.Name = item.Name;
                    position.Grad = item.Grad;
                    position.EmployeePositions = item.EmployeePositions;
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
