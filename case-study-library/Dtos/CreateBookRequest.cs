namespace case_study_library.Dtos
{

    public class CreateBookRequest
    {
        public string BookName { get; set; }
        public string AboutBook { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime? PublicationYear { get; set; }
        public string ImageLink { get; set; }
    }
}
