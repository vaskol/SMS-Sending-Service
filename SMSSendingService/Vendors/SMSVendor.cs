namespace SMSSendingService.Vendors
{
    public abstract class SMSVendor
    {
        public const int MaxMessageLengthAllowed = 480;

        public bool ValidateMessageLength(string message)
        {
            if (message.Length > MaxMessageLengthAllowed)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public abstract void Send(string recipient, string message);
    }

}

