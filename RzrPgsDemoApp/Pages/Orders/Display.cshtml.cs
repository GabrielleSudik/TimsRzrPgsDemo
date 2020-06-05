using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RzrPgsDemoApp
{
    public class DisplayModel : PageModel
    {
        private readonly IOrderData _orderData;
        private readonly IFoodData _foodData;

        [BindProperty(SupportsGet = true)] //this allows us to pass the Id as part of the URL.
        //More generally, allowing binding allows the Property to be changed in the view.
        //Otherwise, it can only be read in the view.
        public int Id { get; set; }

        public OrderModel Order { get; set; }
        public string ItemPurchased { get; set; }

        public DisplayModel(IOrderData orderData, IFoodData foodData)
        {
            _orderData = orderData;
            _foodData = foodData;
        }
        public async Task<IActionResult> OnGet()
        {
            Order = await _orderData.GetOrderById(Id);

            if (Order != null)
            {
                var food = await _foodData.GetFood();
                ItemPurchased = food.Where(x => x.Id == Order.FoodId).FirstOrDefault()?.Title;
                //if food is not null, return its title.
                //if null, just return the page below.
            }

            return Page(); //returning a Page requires an IActionResult return type.
        }
    }
}