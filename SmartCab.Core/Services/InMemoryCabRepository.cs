using SmartCab.Core.Interfaces;
using SmartCab.Core.Models;

namespace SmartCab.Core.Services
{
    public class InMemoryCabRepository : ICabRepository
    {
        private readonly List<Cab> _cabs = new ();

        public void SeedInitialCabs(List<Cab> cabs)
        {
            _cabs.Clear();
            _cabs.AddRange(cabs);
        }

        public List<Cab> GetAvailableCabs() => _cabs.Where(c => c.Status == CabStatus.Available).ToList();

        public Cab? GetCabById(string cabId) => _cabs.FirstOrDefault(c => c.Id == cabId);

        public void UpdateCab(Cab cab) { /* No-op for in-memory */ }

        public List<Cab> GetAllCabs() => _cabs;
    }
}