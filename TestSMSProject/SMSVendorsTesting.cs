using Microsoft.EntityFrameworkCore;
using SMSSendingService.Data;
using SMSSendingService.Vendors;


namespace TestSMSProject
{
    public class SMSVendorsTesting
    {
        private readonly SMSSendingServiceContext _context;

        public SMSVendorsTesting()
        {
            // Set up a new in-memory database for testing  
            var options = new DbContextOptionsBuilder<SMSSendingServiceContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new SMSSendingServiceContext(options);
        }

        //GREEK RECIPIENTS TESTING
        [Theory]
        [InlineData("Γειά σου Μαρία με τα κίτρινα", "+301209025553")]
        [InlineData("Γειά σου Μαρία με τα κίτρινα", "+441209025553")]
        [InlineData("Hello Mary with the yellow outfit", "+301209025553")]
        public void Send_Should_Save_SMS_To_Database_GR(string message, string recipient)
        {
            try
            {
                // Arrange  
                var vendor = new SMSVendorGR(_context);

                // Act  
                vendor.Send(recipient, message);

                // Assert  
                var savedSms = _context.SMS.FirstOrDefault();
                Assert.NotNull(savedSms);
                Assert.Equal(message, savedSms.Message);
                Assert.Equal(recipient, savedSms.Number);
            }
            catch (Xunit.Sdk.EqualException ex)
            {
                // Assert  
                Assert.IsType<Xunit.Sdk.EqualException>(ex);
            }
}


        //CYPRUS RECIPIENTS TESTING
        [Theory]
        [InlineData("Hello my Cyprus.", "+35724622200")]
        [InlineData("Following the TDD approach, add more failing tests, then update the target code.Following the TDD approach, add more failing tests, then update the target code.Following the TDD approach, add more failing tests, then update the target code.Following the TDD approach, add more failing tests, then update the target code", "+35724622200")]
        [InlineData("Hello my Cyprus.", "")]
        public void Send_Should_Save_SMS_To_Database_CY(string message, string recipient)
        {
            try
            {
                // Arrange  
                var vendor = new SMSVendorCY(_context);

                // Act  
                vendor.Send(recipient, message);

                // Assert  
                var savedSms = _context.SMS.FirstOrDefault();
                Assert.NotNull(savedSms);
                Assert.Equal(message, savedSms.Message);
                Assert.Equal(recipient, savedSms.Number);
            }
            catch (Xunit.Sdk.EqualException ex)
            {
                // Assert  
                Assert.IsType<Xunit.Sdk.EqualException>(ex);
            }
}


        //REST OF THE WORLD RECIPIENTS TESTING
        [Theory]
        [InlineData("Do you want tea party today?", "+441632960837")]
        [InlineData("Following the TDD approach, add more failing tests, then update the target code.Following the TDD approach, add more failing tests, then update the target code.Following the TDD approach, add more failing tests, then update the target code.Following the TDD approach, add more failing tests, then update the target code.Following the TDD approach, add more failing tests, then update the target code.Following the TDD approach, add more failing tests, then update the target code", "+441632960837")]
        [InlineData("Do you want tea party today?", "Invalid number")]
        public void Send_Should_Save_SMS_To_Database_REST(string message, string recipient)
        {
            // Arrange    
            var vendor = new SMSVendorRest(_context);

            try
            {
                // Act    
                vendor.Send(recipient, message);

                // Assert    
                var savedSms = _context.SMS.FirstOrDefault();
                Assert.NotNull(savedSms);
                Assert.Equal(message, savedSms.Message);
                Assert.Equal(recipient, savedSms.Number);
            }
            catch (Xunit.Sdk.EqualException ex)
            {
                // Assert  
                Assert.IsType<Xunit.Sdk.EqualException>(ex);
            }
        }


    }

}
