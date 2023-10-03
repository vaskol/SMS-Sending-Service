using SMSSendingService.Vendors;


//interface for handling the selection of vendors
public interface ISMSVendorSelectionStrategy
{
    SMSVendor SelectVendor(string phoneNumber);
}



//GREEK NUMBER SELECTION
public class GRVendorSelectionStrategy : ISMSVendorSelectionStrategy
{
    private readonly SMSVendorGR greeceVendor;

    public GRVendorSelectionStrategy(SMSVendorGR greeceVendor)
    {
        this.greeceVendor = greeceVendor;
    }

    public SMSVendor SelectVendor(string phoneNumber)
    {
        if (phoneNumber.StartsWith("+30"))
        {
            return greeceVendor;
        }

        throw new NotSupportedException($"Country code not supported for phone number {phoneNumber}");
    }
}


//CYPRUS NUMBER SELECTION

public class CYVendorSelectionStrategy : ISMSVendorSelectionStrategy
{
    private readonly SMSVendorCY cyprusVendor;

    public CYVendorSelectionStrategy(SMSVendorCY cyprusVendor)
    {
        this.cyprusVendor = cyprusVendor;
    }

    public SMSVendor SelectVendor(string phoneNumber)
    {
        if (phoneNumber.StartsWith("+357"))
        {
            return cyprusVendor;
        }

        throw new NotSupportedException($"Country code not supported for phone number {phoneNumber}");
    }
}


//REST NUMBER SELECTION
public class RestVendorSelectionStrategy : ISMSVendorSelectionStrategy
{
    private readonly SMSVendorRest restVendor;

    public RestVendorSelectionStrategy(SMSVendorRest restVendor)
    {
        this.restVendor = restVendor;
    }

    public SMSVendor SelectVendor(string phoneNumber)
    {
      
       return restVendor;

        throw new NotSupportedException($"Country code not supported for phone number {phoneNumber}");
    }
}


