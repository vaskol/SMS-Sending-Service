namespace SMSSendingService.Vendors
{
    public class SMSVendorSelector
    {
        private readonly Dictionary<string, ISMSVendorSelectionStrategy> strategies;

        public SMSVendorSelector(SMSVendorGR greeceVendor, SMSVendorCY cyprusVendor, SMSVendorRest restVendor)
        {
            strategies = new Dictionary<string, ISMSVendorSelectionStrategy>
        {
            { "GR", new GRVendorSelectionStrategy(greeceVendor) },
            { "CY", new CYVendorSelectionStrategy(cyprusVendor) },
            { "REST", new RestVendorSelectionStrategy(restVendor) },
        };
        }

        public SMSVendor SelectVendor(string phoneNumber)
        {
            string countryCode = GetCountryCode(phoneNumber);
            return strategies[countryCode].SelectVendor(phoneNumber);
        }

        private string GetCountryCode(string phoneNumber)
        {
            // Get the country code from the vendor selection strategy  
            foreach (var strategy in strategies.Values)
            {
                try
                {
                    if (phoneNumber.StartsWith("+30"))
                    {
                        return "GR";
                    }
                    else if (phoneNumber.StartsWith("+357"))
                    {
                        return "CY";
                    }
                    else
                    {
                        return "REST";
                    }
                }
                catch (NotSupportedException)
                {
                    // This vendor selection strategy does not support this phone number  
                }
            }

            throw new NotSupportedException($"Country code not supported for phone number {phoneNumber}");
        }
    }

}
