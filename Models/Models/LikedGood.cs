namespace Models.Models
{
    public partial class LikedGood
    {
        public LikedGood()
        {
            LikedGoodNomenclatures = new HashSet<LikedGoodNomenclature>();
        }

        public int Id { get; set; }
        public string IdUser { get; set; } = null!;

        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
        public virtual ICollection<LikedGoodNomenclature> LikedGoodNomenclatures { get; set; }
    }
}
