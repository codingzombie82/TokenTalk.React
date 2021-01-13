using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TokenTalk.Models
{
    public class BoardBase
    {
        /// <summary>
        /// 일련번호
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "번호")]
        public int Id { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        /// 
        [MaxLength(255)]
        [Required(ErrorMessage = "필수 입력사항 입니다.")]
        [Display(Name = "글 제목")]
        [Column(TypeName = "NVarChar(255)")]
        public string Title { get; set; }
    
        /// <summary>
        /// 내용
        /// </summary>
        /// 
        [Display(Name ="내용")]
        public string Description { get; set; }
    }

    [Table("Talks")]
    public class Talk : BoardBase
    {
        

    }
}
