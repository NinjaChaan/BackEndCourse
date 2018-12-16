using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
	{
		public async Task<int> GetBikeCountInStation(string stationName) {
			if (stationName.Any(c => char.IsDigit(c))) {
				throw new System.ArgumentException("Station name contained number(s)");
			}
			string[] data = await System.IO.File.ReadAllLinesAsync("bikedata.txt");
			foreach (var line in data) {
				if (line.Contains(stationName)) {
					string s = line.Substring(line.IndexOf(": ")+2).Trim();
					return int.Parse(s);
				}
			}
			throw new NotFoundException(stationName);
		}
	}
}
