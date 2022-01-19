namespace NeoSoft.Masterminds.Models
{
    public class ReviewView
    {
        public int Id { get; set; }

        public string ProfilePhoto { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Rating { get; set; }

        public string Text { get; set; }
    }
}
