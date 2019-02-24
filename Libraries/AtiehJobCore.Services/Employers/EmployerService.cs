using AtiehJobCore.Core.Domain.Employers;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using System;

namespace AtiehJobCore.Services.Employers
{
    public class EmployerService : IEmployerService
    {
        private readonly IRepository<Employer> _employerRepository;
        private readonly IEventPublisher _eventPublisher;

        public EmployerService(IRepository<Employer> employerRepository, IEventPublisher eventPublisher)
        {
            _employerRepository = employerRepository;
            _eventPublisher = eventPublisher;
        }

        public Employer InsertEmployer(Employer employer)
        {
            if (employer == null)
                throw new ArgumentNullException(nameof(employer));

            var currentEmployer = _employerRepository.Insert(employer);

            //event notification
            _eventPublisher.EntityInserted(employer);
            return currentEmployer;
        }

        public bool IsDuplicateInsuranceCode(string insuranceCode)
        {
            return _employerRepository.Any(e => e.InsuranceCode == insuranceCode);
        }
    }
}
