using Microsoft.eShopWeb.ApplicationCore.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class BasketGuards
    {
        public static void NullBasket(this IGuardClause guardClause, string basketId)
        {
            //if (basket == null)
               // throw new BasketNotFoundException(basketId);
        }
    }
}