using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Ncfe.CodeTest
{
    public interface IArchivedDataService
    {
        Learner GetArchivedLearner(int learnerId);
    }
    public class ArchivedDataService: IArchivedDataService
    {
        public Learner GetArchivedLearner(int learnerId)
        {

            var jsonText = System.IO.File.ReadAllText(@"Data/learners.json");
            var learners = JsonConvert.DeserializeObject<List<Learner>>(jsonText);
            // return all from fail entries from database
            return learners.FirstOrDefault(l=>l.Id==learnerId);
        }
    }
}
