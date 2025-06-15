using SmartCab.Core.Models;

namespace SmartCab.Core.Interfaces
{
    public interface ICabRepository
    {
        List<Cab> GetAvailableCabs();
        Cab? GetCabById(string cabId);
        void UpdateCab(Cab cab);
        List<Cab> GetAllCabs();
        void SeedInitialCabs(List<Cab> cabs);
    }
}