
namespace Core.Exceptions
{
    public class BusinessNotFoundException : BusinessException
    {
        public BusinessNotFoundException() : base("Could not find the requested resource")
        { 
        }
        public BusinessNotFoundException(string message) : base(message)
        { 
        }

    }
}
