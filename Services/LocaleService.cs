using Mobiroller.Data;
using Mobiroller.Data.Entity;
using System;
using System.Linq;

namespace Mobiroller.Services
{
    public class LocaleService : ILocaleService
    {
        private readonly IRepository<int, Locale> _repository;

        public LocaleService(IRepository<int, Locale> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Locale GetLocale(string name)
        {
            return _repository.FindBy(x => x.Name == name).FirstOrDefault();
        }
    }
}