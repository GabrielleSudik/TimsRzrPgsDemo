using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RzrPgsDemoApp.Models;

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

        //later, we add this property for the Updating Data lesson.
        //see notes in this model for more info.
        [BindProperty]
        public OrderUpdateModel OrderUpdate { get; set; }

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

        //this is for updating data on the page:
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            await _orderData.UpdateOrderName(OrderUpdate.Id, OrderUpdate.Name);
            //we'd already created the UpdateOrderName method
            //in the Common project

            return RedirectToPage("./Display", new { OrderUpdate.Id });
            //recall the "new" bit is the anonymous object.
            //the result of this line is to redisplay the same page,
            //but with the new info.
        }
    }
}