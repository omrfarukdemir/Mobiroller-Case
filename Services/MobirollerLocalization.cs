using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Mobiroller.Data.Entity;
using System;

namespace Mobiroller.Services
{
    public class AcceptLanguageHeaderLocalization : IMobirollerLocalization
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILocaleService _localeService;

        public AcceptLanguageHeaderLocalization(IHttpContextAccessor httpContextAccessor, ILocaleService localeService)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _localeService = localeService ?? throw new ArgumentNullException(nameof(localeService));
        }

        public Locale GetLocalization()
        {
            var requestCulture = _httpContextAccessor?
                .HttpContext?
                .Features
                .Get<IRequestCultureFeature>()
                .RequestCulture;

            return _localeService.GetLocale(requestCulture?.Culture.Name);
        }
    }
}