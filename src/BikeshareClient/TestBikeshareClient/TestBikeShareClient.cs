﻿using Xunit;
using BikeshareClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestBikeshareClient
{
    public class TestBikeShareClient
    {
		[Theory]
        [InlineData(@"http://gbfs.urbansharing.com/trondheim/")]
		[InlineData(@"http://gbfs.urbansharing.com/bergen-city-bike/")]
        [InlineData(@"https://gbfs.bcycle.com/bcycle_aventura/")]
        [InlineData(@"http://hamilton.socialbicycles.com/opendata/")] 
		public async Task GetStationsAsync_GivenValidBaseUrl_ReturnsStations(string baseUrl)
		{
			var client = new Client(baseUrl);

			var clientResponse = await client.GetStationsAsync();
			var stations = clientResponse.ToList();

			Assert.True(stations.Any()); 
		}

		[Theory]
        [InlineData(@"http://gbfs.urbansharing.com/trondheim/")]
		[InlineData(@"http://gbfs.urbansharing.com/bergen-city-bike/")]
        [InlineData(@"https://gbfs.bcycle.com/bcycle_aventura/")]
        [InlineData(@"http://hamilton.socialbicycles.com/opendata/")] 
		public async Task GetSystemInformationAsync_GivenValidBaseUrl_ReturnsInformation(string baseUrl)
		{
			var client = new Client(baseUrl);

			var clientResponse = await client.GetSystemInformationAsync();

			Assert.False(string.IsNullOrEmpty(clientResponse.Id));
			Assert.False(string.IsNullOrEmpty(clientResponse.Name));
		}

		[Theory]
        [InlineData(@"http://hamilton.socialbicycles.com/opendata/")]
        [InlineData(@"http://coast.socialbicycles.com/opendata/")]
        public async Task GetBikeStatusAsync_GivenCorrectBaseUrl_ReturnsBikesStatus(string endpoint)
        {
			var client = new Client(endpoint);

			var clientRespons = await client.GetBikeStatusAsync();

			Assert.True(clientRespons.Any());
        }

		[Theory]
        [InlineData(@"http://hamilton.socialbicycles.com/opendata/gbfs.json")]
        [InlineData(@"http://coast.socialbicycles.com/opendata/gbfs.json")]
        public async Task GetBikeStatusAsync_GivenCorrectBaseUrlWithGbfsDiscoveryFile_ReturnsBikesStatus(string endpoint)
        {
            var client = new Client(endpoint);

            var clientRespons = await client.GetBikeStatusAsync();

            Assert.True(clientRespons.Any());
        }

		[Theory]
        [InlineData(@"http://gbfs.urbansharing.com/trondheim/")]
		[InlineData(@"http://gbfs.urbansharing.com/bergen-city-bike/")]
        [InlineData(@"https://gbfs.bcycle.com/bcycle_aventura/")]
        [InlineData(@"http://hamilton.socialbicycles.com/opendata/")]
        public async Task GetStationsStatusAsync_GivenCorrectBaseUrl_ReturnsStationsStatus(string endpoint)
        {
			var client = new Client(endpoint);

			var clientRespons = await client.GetStationsStatusAsync();

            Assert.True(clientRespons.Any());
        }

		[Theory]
        [InlineData(@"http://gbfs.urbansharing.com/trondheim/gbfs.json")]
        [InlineData(@"http://gbfs.urbansharing.com/bergen-city-bike/gbfs.json")]
        [InlineData(@"https://gbfs.bcycle.com/bcycle_aventura/gbfs.json")]
        [InlineData(@"http://hamilton.socialbicycles.com/opendata/gbfs.json")]
        public async Task GetStationsStatusAsync_GivenCorrectBaseUrlWithGbfsDiscoveryFile_ReturnsStationsStatus(string endpoint)
        {
            var client = new Client(endpoint);

            var clientRespons = await client.GetStationsStatusAsync();

            Assert.True(clientRespons.Any());
        }

		[Theory]
		[InlineData(@"https://gbfs.bcycle.com/bcycle_aventura/gbfs.json")]
        [InlineData(@"http://hamilton.socialbicycles.com/opendata/gbfs.json")]
		[InlineData(@"https://api.nextbike.net/maps/gbfs/v1/nextbike_sz/gbfs.json")]
		public async Task GetAvailableFeedsAsync_GivenBaseUrlWithGbfsJson_ReturnsListOfAvailableFeeds(string endpoint)
		{
			var client = new Client(endpoint);

			var clientResponse = await client.GetAvailableFeedsAsync();

			Assert.True(clientResponse.Any());
		}
  
		[Theory]
        [InlineData(@"https://gbfs.bcycle.com/bcycle_aventura/gbfs.json")]
		[InlineData(@"http://gbfs.urbansharing.com/trondheim/gbfs.json")]
        [InlineData(@"http://hamilton.socialbicycles.com/opendata/gbfs.json")]
        [InlineData(@"https://api.nextbike.net/maps/gbfs/v1/nextbike_sz/gbfs.json")]
		public async Task GetAvailableLanguagesAsync_GivenBaseUrlWithGbfsJson_ReturnsListOfAvailableLanguages(string endpoint)
		{
			var client = new Client(endpoint);

			var clientResponse = await client.GetAvailableLanguagesAsync();

			Assert.NotEmpty(clientResponse);
		}

		[Theory]
        [InlineData(@"http://gbfs.urbansharing.com/trondheim/gbfs.json")]
        public async Task GetAvailableLanguagesAsync_GivenBaseUrlWithGbfsJson_ReturnsExpectedLanguage(string endpoint)
        {
            var client = new Client(endpoint);
            
            var clientResponse = await client.GetAvailableLanguagesAsync();

			Assert.Equal("nb", clientResponse.First().Name);
        }

		[Theory]
        [InlineData(@"http://gbfs.urbansharing.com/trondheim/gbfs.json")]
		[InlineData(@"http://hamilton.socialbicycles.com/opendata/")]
        [InlineData(@"https://api.nextbike.net/maps/gbfs/v1/nextbike_sz/")]
        public async Task GetAvailableLanguagesAsync_GivenBaseUrlWithGbfsJson_ReturnsExpectedFeed(string endpoint)
        {
            var client = new Client(endpoint);
            
            var clientResponse = await client.GetAvailableLanguagesAsync();

			Assert.True(clientResponse.Any(f => f.Feeds.Any(n => n.Name.Equals("system_information"))));
        }

		[Theory]
		[InlineData(@"http://hamilton.socialbicycles.com/opendata/")]
        [InlineData(@"https://api.nextbike.net/maps/gbfs/v1/nextbike_sz/")]
		public async Task GetAvailableLanguagesAsync_GivenBaseUrlWithoutGbfsJson_ReturnsListOfAvailableLanguages(string endpoint)
        {
            var client = new Client(endpoint);

            var clientResponse = await client.GetAvailableLanguagesAsync();

            Assert.NotEmpty(clientResponse);
        }

    }
}
