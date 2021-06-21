using Mobiroller.Data.Entity;

namespace Mobiroller.Services
{
    public interface ILocaleService
    {
        Locale GetLocale(string name);
    }
}