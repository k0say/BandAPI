using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Services
{
    public class PropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _bandProperyMapping = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
            {"Id", new PropertyMappingValue(new List<string>() {"Id"}) },
            {"Name", new PropertyMappingValue(new List<string>() {"Name"}) },
            {"MainGenre", new PropertyMappingValue(new List<string>() {"MainGenre"}) },
            {"FoundedYearsAgo", new PropertyMappingValue(new List<string>() {"Founded"}, true) }
        };
    }
}
