namespace SULS.App.ViewModels.Users
{
    using SIS.MvcFramework.Attributes.Validation;
    public class LoginInputModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "Username should be between 5 and 20 characters")]
        public string Username { get; set; }

        [RequiredSis]
        [StringLengthSis(6, 20, "Password should be between 6 and 20 characters")]
        public string Password { get; set; }
    }
}
