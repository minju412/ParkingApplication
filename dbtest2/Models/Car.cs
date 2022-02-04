using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dbtest2.Models
{
    public class Car
    {
        [Key]
        public int Car_U { get; set; }

        [DisplayName("차번호")]
        [Required(ErrorMessage = "차량 번호를 입력하세요.")]
        public string CarNum { get; set; }

        [DisplayName("입차시각")]
        public string InTime { get; set; }

        [DisplayName("출차시각")]
        public string OutTime { get; set; }
    }
}