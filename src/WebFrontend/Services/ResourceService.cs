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
    public class ResourceService
    {
        private readonly IRepository<Resource> _repository;


        public ResourceService(IRepository<Resource> Repository)
        {
            this._repository = Repository;
        }

        public IEnumerable<Resource> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Resource> GetEnabled()
        {
            return _repository.GetAll().Where(x => x.IsEnabled == true);
        }

        public void Insert(Resource obj) 
        {
            _repository.Insert(obj);
        }

        public void Update(Resource obj) 
        {
            _repository.Update(obj);
        }

        public Resource Get(string id)
        {
            return _repository.GetById(id);
        }
        

    }
}
