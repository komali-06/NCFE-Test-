namespace Ncfe.CodeTest
{
    public interface ILearnerDataAccess
    {
        LearnerResponse LoadLearner(int learnerId);
    }
    public class LearnerDataAccess: ILearnerDataAccess
    {
        public LearnerResponse LoadLearner(int learnerId)
        {
            // rettrieve learner from 3rd party webservice
            return new LearnerResponse();
        }
    }
}
