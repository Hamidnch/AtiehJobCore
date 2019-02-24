using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using System;

namespace AtiehJobCore.Services.Jobseekers
{
    public class JobseekerService : IJobseekerService
    {
        private readonly IRepository<Jobseeker> _jobseekerRepository;
        private readonly IEventPublisher _eventPublisher;

        public JobseekerService(IRepository<Jobseeker> jobseekerRepository, IEventPublisher eventPublisher)
        {
            _jobseekerRepository = jobseekerRepository;
            _eventPublisher = eventPublisher;
        }

        public Jobseeker InsertJobseeker(Jobseeker jobseeker)
        {
            if (jobseeker == null)
                throw new ArgumentNullException(nameof(jobseeker));

            if (!string.IsNullOrEmpty(jobseeker.Email))
                jobseeker.Email = jobseeker.Email.ToLower();


            var currentJobseeker = _jobseekerRepository.Insert(jobseeker);

            //event notification
            _eventPublisher.EntityInserted(jobseeker);
            return currentJobseeker;
        }
    }
}
