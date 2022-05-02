namespace Models.Models
{
    public partial class LikedGoodNomenclature
    {
        public int Id { get; set; }
        public string IdUser { get; set; } = null!;
        public int IdNom { get; set; }

        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
        public virtual Nomenclature IdNomNavigation { get; set; } = null!;
    }
}
