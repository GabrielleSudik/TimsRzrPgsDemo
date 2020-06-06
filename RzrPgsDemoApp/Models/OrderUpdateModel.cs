using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//we created this model for the Updating Data lesson
//we could have used OrderModel
//but better to have each model do exactly what it needed
//instead of trying to make one model do many thing
//that don't quite fit.

namespace RzrPgsDemoApp.Models
{
    public class OrderUpdateModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
