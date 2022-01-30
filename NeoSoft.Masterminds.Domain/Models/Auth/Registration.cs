namespace NeoSoft.Masterminds.Domain.Models.Models.Auth
{
    public class Registration
    {
        protected Registration()
        {

        }
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? PhotoId { get; set; }
    }
}
