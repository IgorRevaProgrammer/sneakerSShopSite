namespace Models.Models
{
    public partial class CartGood
    {
        public int Id { get; set; }
        public string IdUser { get; set; } = null!;
        public int IdGood { get; set; }

        public virtual Good IdGoodNavigation { get; set; } = null!;
        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
    }
}
