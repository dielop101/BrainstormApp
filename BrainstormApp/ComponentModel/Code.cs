using System.ComponentModel.DataAnnotations;

namespace BrainstormApp.ComponentModel
{
    public class Code
    {

        [Required]
        [StringLength(6, ErrorMessage = "Code must have 6 characters.")]
        public string CodeValue { get; set; }
    }
}
