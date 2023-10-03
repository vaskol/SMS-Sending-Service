using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMSSendingService.Controllers;
using SMSSendingService.Data;
using SMSSendingService.Models;

namespace TestSMSProject
{
    public class SMSTestingClass
    {

        internal class SMSControllerTests
        {
            internal readonly SMSSendingServiceContext _context;
            //An in-memory database is created entirely in memory and only persists for the duration of the test.
            public SMSControllerTests()
            {
                // Set up a new in-memory database for testing  
                var options = new DbContextOptionsBuilder<SMSSendingServiceContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

                _context = new SMSSendingServiceContext(options);

                [Fact]
                async Task PostSMS_Should_Create_New_SMS()
                {
                    // Arrange  
                    var configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<SMS, SMSRequestDTO>();
                    });
                    var mapper = configuration.CreateMapper();
                    var controller = new SMSController(_context, mapper);

                    var sms = new SMS
                    {
                        Number = "+441632960837",
                        Message = "Test message"
                    };

                    // Act  
                    var result = await controller.PostSMS(sms);

                    // Assert  
                    Assert.IsType<CreatedAtActionResult>(result.Result);
                }
            }
        }
    }
}