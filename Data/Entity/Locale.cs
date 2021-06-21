using System.Collections.Generic;

namespace Mobiroller.Data.Entity
{
    public class Locale : BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<Incident> Incidents { get; set; }
    }
}