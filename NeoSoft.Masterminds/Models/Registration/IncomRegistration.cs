namespace NeoSoft.Masterminds.Models.Registration
{
    public class IncomRegistration
    {
        protected IncomRegistration()
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
