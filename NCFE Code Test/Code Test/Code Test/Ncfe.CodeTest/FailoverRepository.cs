using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ncfe.CodeTest
{
    public interface IFailoverRepository
    {
        List<FailoverEntry> GetFailOverEntries();
    }
    public class FailoverRepository: IFailoverRepository
    {
        public List<FailoverEntry> GetFailOverEntries()
        {
            var jsonText = System.IO.File.ReadAllText(@"Data/FailoverEntries.json");
            var failoverEntries = JsonConvert.DeserializeObject<List<FailoverEntry>>(jsonText);
            // return all from fail entries from database
            return failoverEntries;
        }
    }
}
