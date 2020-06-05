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
    public class ListModel : PageModel
    {
        private readonly IFoodData _foodData;

        public List<FoodModel> Food { get; set; }

        public ListModel(IFoodData foodData)
        {
            _foodData = foodData;
        }
        public async Task OnGet()
        {
            Food = await _foodData.GetFood();

            //async Task is like "void" but for awaiting things.
            //a benefit to returning SOMETHING like when we return a 
            //Page/IActionType in Display, is that if something goes wrong,
            //the user will see a bad page message.
        }
    }
}