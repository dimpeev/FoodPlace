namespace FoodPlace.Web.Areas.Owner.Models.Menus
{
    using Services.Owner.Models.Menu;
    using System.Collections.Generic;

    public class MenusListingViewModel
    {
        public IEnumerable<OwnerMenuListingServiceModel> Menus { get; set; }

        public int TotalMenus { get; set; }
    }
}