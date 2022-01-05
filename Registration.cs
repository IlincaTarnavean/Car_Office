namespace AutoLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Registration")]
    public partial class Registration
    {
        [Key]
        public int Reg_Id { get; set; }

        public int? Car_Id { get; set; }

        public int? Payment { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Expiry_Date { get; set; }

        public int? Perm_Id { get; set; }

        public virtual Car Car { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
