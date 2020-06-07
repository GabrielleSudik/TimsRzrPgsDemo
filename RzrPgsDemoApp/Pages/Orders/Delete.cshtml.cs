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
    public class DeleteModel : PageModel
    {
        private readonly IOrderData _orderData;

        //you need to bind Id because in the html
        //you use it to update the property.
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public OrderModel Order { get; set; }

        public DeleteModel(IOrderData orderData)
        {
            _orderData = orderData;
        }

        //we still need a get method, to actually get the thing we'll delete.
        public async Task OnGet()
        {
            Order = await _orderData.GetOrderById(Id);
        }

        //then to call the method that will delete the order.
        public async Task<IActionResult> OnPost()
        {
            await _orderData.DeleteOrder(Id);

            return RedirectToPage("./Create");
        }
    }
}