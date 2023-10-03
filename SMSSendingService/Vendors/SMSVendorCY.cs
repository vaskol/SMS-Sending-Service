using SMSSendingService.Data;
using SMSSendingService.Models;

namespace SMSSendingService.Vendors
{
    public class SMSVendorCY : SMSVendor
    {
        private readonly SMSSendingServiceContext _context;

        public SMSVendorCY(SMSSendingServiceContext context)
        {
            _context = context;
        }

        public override void Send(string recipient, string message)
        {
            const int maxMessageLength = 160;

            // Split the message into multiple SMS messages if necessary  
            string[] messages = SplitMessage(message, maxMessageLength);
            foreach (string msg in messages)
            {
                var internalDTO = new SMS
                {
                    Number = recipient,
                    Message = msg,
                    SenderNumber = "CY",
                    SentDate = DateTime.UtcNow,
                    Status = "Sent"
                };

                _context.SMS.Add(internalDTO);
                _context.SaveChanges();
            }
        }

        private string[] SplitMessage(string message, int maxLength)
        {
            List<string> messages = new List<string>();

            if (message.Length <= maxLength)
            {
                messages.Add(message);
            }
            else
            {
                int numMessages = (int)Math.Ceiling((double)message.Length / maxLength);
                for (int i = 0; i < numMessages; i++)
                {
                    int startIndex = i * maxLength;
                    int length = Math.Min(maxLength, message.Length - startIndex);
                    string msg = message.Substring(startIndex, length);
                    messages.Add(msg);
                }
            }

            return messages.ToArray();
        }
    }

}
