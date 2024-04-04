using Google.Apis.Services;

namespace Infrastructure.Services.SpreadSheetRead
{
    public class SpreadSheetReader : ISpreadSheetReader
    {
        public SpreadSheetReader(string apiKey)
        {
            var service = new BaseClientService.Initializer()
            {
                ApplicationName = "Configuration Loader",
                ApiKey = apiKey,
            };
        }
    }
}