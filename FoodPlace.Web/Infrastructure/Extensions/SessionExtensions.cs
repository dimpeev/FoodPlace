namespace FoodPlace.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System;

    public static class SessionExtensions
    {
        public static string GetShoppingCartId(this ISession session)
        {
            var shoppingCardId = session.GetString(WebConstants.ShoppingCartIdName);
            if (shoppingCardId == null)
            {
                shoppingCardId = Guid.NewGuid().ToString();
                session.SetString(WebConstants.ShoppingCartIdName, shoppingCardId);
            }

            return shoppingCardId;
        }
    }
}