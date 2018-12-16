using System;
using System.Threading.Tasks;

namespace Assignment1
{
    class Program
    {
		static void Main(string[] args) {
			try {
				ICityBikeDataFetcher fetcher = null;
				if (args[1] == "realtime") {
					 fetcher = new RealTimeCityBikeDataFetcher();
				}else if(args[1] == "offline") {
					fetcher = new OfflineCityBikeDataFetcher();
				} else {
					Console.WriteLine("Invalid argument: " + args[1]);
					Console.ReadKey();
					return;
				}
				Task<int> task = fetcher.GetBikeCountInStation(args[0]);
				task.Wait();
				Console.WriteLine(task.Result);
			}catch(AggregateException e) {
				if (e.InnerException.GetType() == typeof(ArgumentException)) {
					Console.WriteLine("Invalid argument: " + e.InnerException.Message);
				}else if(e.InnerException.GetType() == typeof(NotFoundException)) {
					Console.WriteLine("Not found: " + e.InnerException.Message);
				} else {
					Console.WriteLine(e);
				}
			}
			Console.ReadKey();
		}
	}
}
