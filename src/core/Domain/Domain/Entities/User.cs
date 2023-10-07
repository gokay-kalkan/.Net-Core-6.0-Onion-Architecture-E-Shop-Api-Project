

using Microsoft.AspNetCore.Identity;
using Shared.Commons;


namespace Domain.Entities
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
       

        public virtual List<Cart>Carts { get; set; }
    }
}
