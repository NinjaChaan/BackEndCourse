using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace Assignment1
{
    class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
		public async Task<int> GetBikeCountInStation(string stationName) {
			if(stationName.Any(c => char.IsDigit(c))) {
				throw new System.ArgumentException("Station name contained number(s)");
			}
			HttpClient client = new HttpClient();
			string data = await client.GetStringAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
			BikeRentalStationList list = JsonConvert.DeserializeObject<BikeRentalStationList>(data);
			Station station = list.stations.FirstOrDefault(x => x.name == stationName);
			if (station != null) {
				return station.bikeCount;
			} else {
				throw new NotFoundException(stationName);
			}
		}
	}
}
