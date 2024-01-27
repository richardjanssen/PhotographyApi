
using Business.Entities;

namespace Data.Proxies.GarminExploreMapShare;

public interface IGarminExploreMapShareManager
{
    Task<SatelliteMessengerLocation?> GetSatelliteMessengerLocation();
}