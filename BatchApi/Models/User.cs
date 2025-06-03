using System.ComponentModel.DataAnnotations.Schema;

namespace BatchApi.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role? Role { get; set; }

    }
}
