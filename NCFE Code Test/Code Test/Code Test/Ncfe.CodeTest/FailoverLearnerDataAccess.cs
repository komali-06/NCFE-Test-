namespace Ncfe.CodeTest
{
    public interface IFailoverLearnerDataAccess
    {
        LearnerResponse GetLearnerById(int id);
    }
    public class FailoverLearnerDataAccess: IFailoverLearnerDataAccess
    {
        public LearnerResponse GetLearnerById(int id)
        {
            // retrieve learner from database
            return new LearnerResponse();
        }
    }
}
