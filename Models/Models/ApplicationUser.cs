using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Requests = new HashSet<Request>();
            CartGoods = new HashSet<CartGood>();
            LikedGoodNomenclatures = new HashSet<LikedGoodNomenclature>();
        }
        [Required]
        [PersonalData]
        public string FirstName { get; set; } = null!;
        [Required]
        [PersonalData]
        public string LastName { get; set; } = null!;
        [Required]
        [PersonalData]
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<CartGood> CartGoods { get; set; }
        public virtual ICollection<LikedGoodNomenclature> LikedGoodNomenclatures { get; set; }
    }
}
