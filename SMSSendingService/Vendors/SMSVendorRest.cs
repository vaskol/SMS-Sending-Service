using Microsoft.EntityFrameworkCore;
using SMSSendingService.Data;
using SMSSendingService.Models;

namespace SMSSendingService.Vendors
{
    public class SMSVendorRest : SMSVendor
    {
        private readonly SMSSendingServiceContext _context;

        public SMSVendorRest(SMSSendingServiceContext context)
        {
            _context = context;
        }

        public override void Send(string recipient, string message)
        {
            var internalDTO = new SMS
            {
                Number = recipient,
                Message = message,
                SenderNumber = "REST",
                SentDate = DateTime.UtcNow,
                Status = "Sent"
            };

            _context.SMS.Add(internalDTO);
            _context.SaveChanges();
        }
    }
}

