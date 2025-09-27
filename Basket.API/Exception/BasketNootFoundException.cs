

namespace Basket.API.Exception
{
    public class BasketNootFoundException : NotFoundException
    {
        public BasketNootFoundException( string userName) : base("Basket", userName)
        {
            
        }
    }
}
