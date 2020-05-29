using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RzrPgsDemoApp
{
    public class CreateModel : PageModel
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public List<SelectListItem> FoodItems { get; set; } //SelectListItem is a dropdown list.

        [BindProperty] //allows writing to this object, even though it is a complex object (ie, a class)
        public OrderModel Order { get; set; }

        //create constructor:
        //we inject the data we'll need here.
        //ctrl-dot to bring in Usings and create fields.
        public CreateModel(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }
        public async Task OnGet()
        {
            var food = await _foodData.GetFood();
            //Tip from Tim: he'll use Var when he's creating quickie variables
            //and it's not so important to clearly define them. You can be more formal, though.

            FoodItems = new List<SelectListItem>();

            //Next block will take the list of from the GetFood method
            //and turn it into a list that will populate a dropdown.
            //We assign Id as the Value because that's what will get posted in the DB.
            //And Title as Text because that's what will display.
            food.ForEach(x =>
            {
                FoodItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Title });
            });
        }
    }
}