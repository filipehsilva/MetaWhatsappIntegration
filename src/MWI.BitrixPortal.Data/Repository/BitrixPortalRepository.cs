using Microsoft.EntityFrameworkCore;
using MWI.BitrixPortal.Domain;
using MWI.BitrixPortal.Domain.Entities;
using NetDevPack.Data;
using System.Data.Common;

namespace MWI.BitrixPortal.Data.Repository
{
    public class BitrixPortalRepository : IBitrixPortalRepository
    {
        private readonly BitrixPortalContext _context;

        public BitrixPortalRepository(BitrixPortalContext context)
        {
            _context = context;
        }

        #nullable disable

        public IUnitOfWork UnitOfWork => _context;

        public DbConnection GetConnection() => _context.Database.GetDbConnection();

        public void Add(Portal portal)
        {
            _context.Portals.Add(portal);
        }

        public async Task<IEnumerable<Portal>> GetAllPortals()
        {
            return await _context.Portals.AsNoTracking().ToListAsync();
        }

        public async Task<Portal> GetPortalById(Guid id)
        {
            return await _context.Portals.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Remove(Portal portal)
        {
            _context.Portals.Remove(portal);
        }

        public void Update(Portal portal)
        {
            _context.Portals.Update(portal);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
