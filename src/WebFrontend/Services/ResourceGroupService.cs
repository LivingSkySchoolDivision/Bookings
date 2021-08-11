using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using LSSD.Bookings;

namespace WebFrontend
{
    public class ResourceGroupService
    {
        private readonly IRepository<ResourceGroup> _repository;


        public ResourceGroupService(IRepository<ResourceGroup> Repository)
        {
            this._repository = Repository;
        }

        public IEnumerable<ResourceGroup> GetAll()
        {
            return _repository.GetAll();
        }
        
        public void Update(ResourceGroup obj) 
        {
            _repository.Update(obj);
        }

        public ResourceGroup Get(string id)
        {
            return _repository.GetById(id);
        }
        

    }
}
