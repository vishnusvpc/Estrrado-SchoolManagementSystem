using Estrrado_SchoolManagementSystem.Models;
using Estrrado_SchoolManagementSystem.Repository;

namespace Estrrado_SchoolManagementSystem.Service
{
    public class RegisterService
    {
        private readonly IRegistrationRepository _registrationRepository;
        public RegisterService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
        
        public int RegristerUser(RegistrationModel req)
        {
            if (req == null)
                throw new Exception("user registraion data cannot be empty");

            return _registrationRepository.AddUser(req);

        }
    }
}
