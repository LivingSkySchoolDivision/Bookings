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

        public void Update(Resource obj)
        {
            _repository.Update(obj);
        }

        public Resource Get(string id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Resource> GetForClaims(IEnumerable<string> Claims, string AdminGroupID)
        {
            IList<Resource> allResources = _repository.GetAll();
            IList<Resource> returnMe = new List<Resource>();

            foreach(Resource r in allResources)
            {
                foreach(string claim in Claims)
                {
                    if (r.OIDC_CanEditBookings.Contains(claim)) 
                    {
                        if (!returnMe.Contains(r)) 
                        {
                            returnMe.Add(r);
                        }
                    }

                    if (r.OIDC_CanViewResource.Contains(claim)) 
                    {
                        if (!returnMe.Contains(r)) 
                        {
                            returnMe.Add(r);
                        }
                    }

                    if (!string.IsNullOrEmpty(AdminGroupID))
                    {
                        if (AdminGroupID.Contains(claim)) 
                        {
                            if (!returnMe.Contains(r)) 
                            {
                                returnMe.Add(r);
                            }
                        }
                    }

                    
                }
            }

            return returnMe;

            // This doesn't work as expected
            /*
            return _repository.Find(x =>
                x.OIDC_CanEditBookings.Intersect(Claims).Any() ||
                x.OIDC_CanViewResource.Intersect(Claims).Any() ||
                x.OIDC_CanEditBookings.Count == 0 ||
                x.OIDC_CanViewResource.Count == 0
            ).ToList();
            */
        }


    }
}
