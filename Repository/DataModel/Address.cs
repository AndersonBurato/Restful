using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful.Repository.DataModel
{
    public class Address
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string StreetName { get; set; }
        public int? Number { get; set; }
        public virtual Person Person { get; set; }
    }
}
