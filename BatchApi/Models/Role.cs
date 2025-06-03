namespace BatchApi.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        // to link with child class    1-N Relationship
        public List<User>? Users { get; set; }
    }
}
