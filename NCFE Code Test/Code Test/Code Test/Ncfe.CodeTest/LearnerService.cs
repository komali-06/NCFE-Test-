﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncfe.CodeTest
{
    public interface ILearnerService
    {
        Learner GetLearner(int learnerId, bool isLearnerArchived);
    }
    public class LearnerService: ILearnerService
    {
        private readonly IArchivedDataService _archivedDataService;
        private readonly IFailoverRepository _failoverRepository;
        private readonly ILearnerDataAccess _learnerDataAccess;
        private readonly IFailoverLearnerDataAccess _failoverLearnerDataAccess;
        public LearnerService(IArchivedDataService archivedDataService, 
            IFailoverRepository failoverRepository,
            ILearnerDataAccess learnerDataAccess,
            IFailoverLearnerDataAccess failoverLearnerDataAccess)
        {
            _archivedDataService = archivedDataService;
            _failoverRepository = failoverRepository;
            _learnerDataAccess = learnerDataAccess;
            _failoverLearnerDataAccess = failoverLearnerDataAccess;
        }
        public Learner GetLearner(int learnerId, bool isLearnerArchived)
        {
            Learner archivedLearner = null;

            if (isLearnerArchived)
            {
                archivedLearner = _archivedDataService.GetArchivedLearner(learnerId);

                return archivedLearner;
            }
            else
            {

                var failoverEntries = _failoverRepository.GetFailOverEntries();


                var failedRequests = 0;

                foreach (var failoverEntry in failoverEntries)
                {
                    if (failoverEntry.DateTime > DateTime.Now.AddMinutes(-10))
                    {
                        failedRequests++;
                    }
                }

                LearnerResponse learnerResponse = null;
                Learner learner = null;

                if (failedRequests > 100 && (ConfigurationManager.AppSettings["IsFailoverModeEnabled"] == "true" || ConfigurationManager.AppSettings["IsFailoverModeEnabled"] == "True"))
                {
                    learnerResponse = _failoverLearnerDataAccess.GetLearnerById(learnerId);
                }
                else
                {
                    learnerResponse = _learnerDataAccess.LoadLearner(learnerId);
                }

                if (learnerResponse.IsArchived)
                {
                    learner = _archivedDataService.GetArchivedLearner(learnerId);
                }
                else
                {
                    learner = learnerResponse.Learner;
                }


                return learner;
            }
        }

    }
}
