using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMSSendingService.Data;
using SMSSendingService.Models;
using SMSSendingService.Vendors;
namespace SMSSendingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly SMSSendingServiceContext _context;
        private readonly IMapper _mapper;

        public SMSVendorSelector SMSVendorSelector;

        public SMSController(SMSSendingServiceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET: api/SMS
       [HttpGet]
        public async Task<ActionResult<IEnumerable<SMS>>> GetSMS()
        {
            if (_context.SMS == null)
            {
                return NotFound();
            }
            return await _context.SMS.ToListAsync();
        }

        // GET: api/SMS/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SMS>> GetSMS(int id)
        {
            if (_context.SMS == null)
            {
                return NotFound();
            }
            var sMS = await _context.SMS.FindAsync(id);

            if (sMS == null)
            {
                return NotFound();
            }

            return sMS;
        }

        public async Task<ActionResult<SMS>> PostSMS([FromBody] SMS requestDTO) 
        {
            if (requestDTO == null)
            {
                return BadRequest("SMS request object is null");
            }

            var internalDTO = _mapper.Map<SMS>(requestDTO);

            var vendorSelector = new SMSVendorSelector(
             new SMSVendorGR(_context),    //  "+306989999898"  
             new SMSVendorCY(_context),    //  "+35724622200" 
             new SMSVendorRest(_context)); //  "+441632960837" 

            var vendor = vendorSelector.SelectVendor(internalDTO.Number);

            if (vendor.ValidateMessageLength(internalDTO.Message) == false)
            {
                return BadRequest("SMS message length is invalid");
            }
            vendor.Send(internalDTO.Number, internalDTO.Message);

            var responseDTO = new SMSResponseDTO
            {
                Id = internalDTO.Id,
                Number = internalDTO.Number,
                Message = internalDTO.Message,
                Status = internalDTO.Status
            };

            return CreatedAtAction(nameof(GetSMS), new { id = responseDTO.Id }, responseDTO);
        }
    }

}
