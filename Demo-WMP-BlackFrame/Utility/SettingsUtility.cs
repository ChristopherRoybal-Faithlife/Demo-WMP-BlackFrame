using Microsoft.Extensions.Configuration;

namespace Demo_WMP_BlackFrame.Utility
{
	public static class SettingsUtility
    {
        public static T Get<T>(string filePath)
		{
            return new ConfigurationBuilder()
                .AddJsonFile(filePath)
                .Build()
                .Get<T>();
		}
    }
}
