namespace SMSSendingService.Models
{
    internal class SMSResponseDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}