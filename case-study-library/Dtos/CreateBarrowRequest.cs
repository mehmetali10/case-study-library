namespace case_study_library.Dtos
{
    public class CreateBarrowRequest
    {
        public int BookId { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public DateTime? BarrowEndDate { get; set; }
    }
}
