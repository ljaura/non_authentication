using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace UserService.API.Utility
{
    public static class LocalizationCollectionExtension
    {
        public static IServiceCollection AddLocalizationServices(this IServiceCollection services)
        {
            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("ar-KW")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

                    options.SupportedCultures = supportedCultures;

                    options.SupportedUICultures = supportedCultures;

                });

            return services;
        }
    }
}
