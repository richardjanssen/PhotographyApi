using Business.Entities;
using Common.Common;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace Data.Proxies.GarminExploreMapShare;

public class GarminExploreMapShareManager(IOptions<AppSettings> appSettings) : IGarminExploreMapShareManager
{
    private readonly XNamespace _ns = "http://www.opengis.net/kml/2.2";

    public async Task<SatelliteMessengerLocation?> GetSatelliteMessengerLocation()
    {
        var xmlReader = XmlReader.Create(appSettings.Value.GarminExploreRawKmlFeed, new XmlReaderSettings { Async = true });
        var document = await XDocument.LoadAsync(xmlReader, LoadOptions.None, new CancellationToken(false));

        // XML properties are case sensitive
        return document.Descendants(_ns + "Placemark").Select(pm =>
        {
            // Get XML properties
            var timestamp = pm.Element(_ns + "TimeStamp")?.Element(_ns + "when")?.Value;
            var properties = pm.Element(_ns + "ExtendedData")?.Descendants(_ns + "Data");
            var latString = properties?.FirstOrDefault(p => p.Attribute("name")?.Value == "Latitude")?.Element(_ns + "value")?.Value;
            var lonString = properties?.FirstOrDefault(p => p.Attribute("name")?.Value == "Longitude")?.Element(_ns + "value")?.Value;

            // Get SatelliteMessengerLocation properties
            DateTime? date = timestamp != null ? DateTime.Parse(timestamp).ToUniversalTime() : null;
            // CultureInfo required to keep decimal separator
            double? lat = latString != null ? double.Parse(latString, CultureInfo.GetCultureInfo("en-US")) : null; 
            double? lon = lonString != null ? double.Parse(lonString, CultureInfo.GetCultureInfo("en-US")) : null;

            // All properties are required
            if (date == null || lat == null || lon == null)
            {
                return null;
            }

            return new SatelliteMessengerLocation((double)lat, (double)lon, (DateTime)date);
        })
            .Where(location => location is not null)
            .FirstOrDefault();
    }
}
