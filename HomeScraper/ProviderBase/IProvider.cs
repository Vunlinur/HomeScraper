namespace HomeScraper.ProviderBase {
    interface IProvider {
        HomeData GetHomeData(string link);
    }
}
