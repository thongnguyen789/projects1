using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MayinOnline.Models
{
    [Table("KHACHHANG")]
    public partial class Khachhang
    {
        public Khachhang()
        {
            Diachi = new HashSet<Diachi>();
            Hoadon = new HashSet<Hoadon>();
        }

        [Key]
        [Column("MaKH")]
        public int MaKh { get; set; }
        [Required]
        [StringLength(100)]
        public string Ten { get; set; }
        [StringLength(20)]
        public string DienThoai { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(255)]
        public string MatKhau { get; set; }
        [NotMapped] // Đánh dấu thuộc tính này không tương ứng với cột trong cơ sở dữ liệu
        public string CurrentPassword { get; set; }

        [NotMapped] // Đánh dấu thuộc tính này không tương ứng với cột trong cơ sở dữ liệu
        public string NewPassword { get; set; }


        [InverseProperty("MaKhNavigation")]
        public virtual ICollection<Diachi> Diachi { get; set; }
        [InverseProperty("MaKhNavigation")]
        public virtual ICollection<Hoadon> Hoadon { get; set; }
    }
}
