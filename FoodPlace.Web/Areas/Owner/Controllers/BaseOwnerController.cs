namespace FoodPlace.Web.Areas.Owner.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstants.OwnerArea)]
    [Authorize(Roles = WebConstants.AdministratorRole + "," + WebConstants.OwnerRole)]
    public class BaseOwnerController : Controller
    {
    }
}
