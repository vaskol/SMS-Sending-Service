using SMSSendingService.Data;
using SMSSendingService.Models;
using System.Text.RegularExpressions;

namespace SMSSendingService.Vendors
{
    public class SMSVendorGR : SMSVendor
    {
        private readonly SMSSendingServiceContext _context;

        public SMSVendorGR(SMSSendingServiceContext context)
        {
            _context = context;
        }
        public override void Send(string recipient, string message)
        {
            // Check if the message contains only Greek characters  
            if (Regex.IsMatch(message, @"^[\u0370-\u03FF\s]+$"))
            {
                var internalDTO = new SMS
                {
                    Number = recipient,
                    Message = message,
                    SenderNumber = "GR",
                    SentDate = DateTime.UtcNow,
                    Status = "Sent"
                };

                _context.SMS.Add(internalDTO);
                _context.SaveChanges();
            }
        }

    }
}