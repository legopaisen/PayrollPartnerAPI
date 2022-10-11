namespace PayrollPartnerAPI.Model
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string AccLevel { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy{ get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Email { get; set; }
        public string Active { get; set; }
    }
}
