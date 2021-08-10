using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace LSSD.Bookings.API.Services
{
    public class ResourceService
    {
        private readonly IRepository<Resource> _repository;


        public ResourceService(IRepository<Resource> Repository)
        {
            this._repository = Repository;
        }

        public IEnumerable<Resource> GetEnabled()
        {
            return _repository.GetAll().Where(x => x.IsEnabled == true);
        }

    }
}
