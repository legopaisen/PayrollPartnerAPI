namespace PayrollPartnerAPI.Model
{
    public class LoginModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmPass { get; set; }
        public string name { get; set; }
        public string accLevel { get; set; }
        public string createdOn { get; set; }
        public string modifiedBy{ get; set; }
        public string modifiedOn { get; set; }
        public string email { get; set; }
        public string active { get; set; }
    }
}
