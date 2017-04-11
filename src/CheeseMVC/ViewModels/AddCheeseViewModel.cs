using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public AddCheeseViewModel()
        { }

        public AddCheeseViewModel(IEnumerable<CheeseCategory> categories)

        //public AddCheeseViewModel(cheeseCategories)
        {
            Categories =  new List<SelectListItem>();

            foreach (var category in categories)
            {
                Categories.Add(new SelectListItem
                {
                    Value = category.ID.ToString(),
                    Text = category.Name
                });
            }
        }

        //public CheeseType Type { get; set; }

        //public List<SelectListItem> CheeseTypes { get; set; }

        //public AddCheeseViewModel()
        //{

        //     CheeseTypes = new List<SelectListItem>();

        //     // <option value="0">Hard</option>
        //
        /////////// repeat the following for each cheese type
        //     CheeseTypes.Add(new SelectListItem
        //     {
        //         Value = ((int)CheeseType.Hard).ToString(),
        //         Text = CheeseType.Hard.ToString()
        //     });


    }
}
