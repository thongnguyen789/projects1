using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MayinOnline.Models
{
    [Table("DIACHI")]
    public partial class Diachi
    {
        [Key]
        [Column("MaDC")]
        public int MaDc { get; set; }
        [Column("MaKH")]
        public int MaKh { get; set; }
        [Required]
        [Column("DiaChi")]
        [StringLength(100)]
        public string DiaChi1 { get; set; }
        [StringLength(20)]
        public string PhuongXa { get; set; }
        [StringLength(50)]
        public string QuanHuyen { get; set; }
        [StringLength(50)]
        public string TinhThanh { get; set; }
        public int? MacDinh { get; set; }

        [ForeignKey(nameof(MaKh))]
        [InverseProperty(nameof(Khachhang.Diachi))]
        public virtual Khachhang MaKhNavigation { get; set; }
    }
}
