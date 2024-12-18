namespace Revision.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocSinh")]
    public partial class HocSinh
    {
        [Key]
        [StringLength(10)]
        public string sbd { get; set; }
        [Required(ErrorMessage = "Không bỏ trống tên")]
        [StringLength(50)]
        public string hoten { get; set; }

       
        [StringLength(50)]
        public string anhduthi { get; set; }

        public int? malop { get; set; }
        [Required(ErrorMessage = "Điểm thi từ 0 -> 10")]
        [Range(0, 10)]
        public double? diemthi { get; set; }

        public virtual LopHoc LopHoc { get; set; }
    }
}
