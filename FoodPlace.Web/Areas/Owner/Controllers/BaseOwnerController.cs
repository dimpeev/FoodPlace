namespace FoodPlace.Web.Areas.Owner.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstants.OwnerArea)]
    [Authorize(Roles = WebConstants.OwnerRole)]
    public class BaseOwnerController : Controller
    {
    }
}
