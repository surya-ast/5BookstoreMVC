using System;
using System.ComponentModel.DataAnnotations;

namespace bookStoreV2.Models
{
//pastikan merupakan public class agar bisa diakses dr luar
    public class Category
    {
        //buat property Id dan Name
        //jika ada nama property Id, maka akan diidentifikasi langsung sbg Prim Key
        //jika namanya bukan Id namun ingin dijadikan Id, pastikan data annotation [Key]

        [Key]
        public int Id { get; set; }

        [Display(Name="Category Name")]
        [Required]
        [MaxLength(50)]
        public String Name { get; set; }
    }
}
