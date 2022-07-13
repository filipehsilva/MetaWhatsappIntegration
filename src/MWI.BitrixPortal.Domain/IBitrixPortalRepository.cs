using MWI.BitrixPortal.Domain.Entities;
using MWI.Core.Data;
using System.Data.Common;

namespace MWI.BitrixPortal.Domain
{
    public interface IBitrixPortalRepository : IRepository<Portal>
    {
        void Add(Portal portal);
        void Update(Portal portal);
        void Remove(Portal portal);

        Task<Portal> GetPortalByMemberId(string memberId);
        Task<Portal> GetPortalById(Guid id);
        Task<IEnumerable<Portal>> GetAllPortals();

        DbConnection GetConnection();
    }
}
