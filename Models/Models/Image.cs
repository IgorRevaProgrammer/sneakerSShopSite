using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public byte[] ImgData { get; set; } = null!;
        public string MimeType { get; set; } = null!;
        public int IdNom { get; set; }

        public virtual Nomenclature IdNomNavigation { get; set; } = null!;
    }
}
