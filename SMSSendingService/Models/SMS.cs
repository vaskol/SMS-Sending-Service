namespace SMSSendingService.Models
{
    public class SMS
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Message { get; set; }
        public string SenderNumber { get; set; }
        public DateTime SentDate { get; set; }
        public string Status { get; set; }
    }

}
