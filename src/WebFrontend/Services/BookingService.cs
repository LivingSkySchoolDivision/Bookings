using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using LSSD.Bookings;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace WebFrontend
{
    public class BookingService
    {
        private readonly IRepository<SingleBooking> _repository;
        private readonly IConfiguration configuration;


        public BookingService(IRepository<SingleBooking> Repository, IConfiguration Configuration)
        {
            this._repository = Repository;
            configuration = Configuration;
        }
        public void Insert(SingleBooking obj) 
        {
            // Adjust time zones to UTC
            // Adjust booking date (if applicable)
            // Adjust booking hour and minute

            _repository.Insert(obj);
        }
        public void Update(SingleBooking obj) 
        {
            // Adjust time zones to UTC

            _repository.Update(obj);
        }

        public SingleBooking Get(string id)
        {
            // Adjust time zones to UTC

            return _repository.GetById(id);
        }

        public IList<SingleBooking> Find(Expression<Func<SingleBooking, bool>> predicate)
        {
            // Adjust time zones to UTC

            return _repository.Find(predicate);
        }
        

    }
}
