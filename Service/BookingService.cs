using AutoMapper;
using EventTicketBookingApi.Constant;
using EventTicketBookingApi.Contract;
using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;
using EventTicketBookingApi.Service.IService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EventTicketBookingApi.Service
{
    public class BookingService:IBookingService
    {
        private readonly EventTicketBookingDbContext _dbContext;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventsRepository _eventRepository;
        private readonly IMapper _mapper;
        public BookingService(EventTicketBookingDbContext context, IBookingRepository bookingRepository,
            IEventsRepository eventsRepository, IMapper mapper)
        {
            this._dbContext = context;
            this._bookingRepository = bookingRepository;
            this._eventRepository = eventsRepository;
            this._mapper = mapper;
        }
      
        public async Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request)
        {
            if (string.IsNullOrEmpty(request.CustomerEmail))
                throw new ArgumentException("Customer email is required.");
            var eventEntity = await _eventRepository.GetEventWithCategoriesAsync(request.EventId);
            if (eventEntity == null) throw new Exception("Event not found.");

            var category = eventEntity.TicketCategories.FirstOrDefault(c => c.Id == request.CategoryId);
            if (category == null) throw new InvalidOperationException("Ticket category not found.");
            if (category == null || category.AvailableSeats < request.Quantity)
                throw new InvalidOperationException("Insufficient available seats.");

            if (request.Quantity > 6) throw new InvalidOperationException("Cannot book more than 6 tickets at once.");

            var totalAmount = category.Price * request.Quantity;

            bool isPaymentValid = await ValidatePaymentAsync(request.PaymentToken, totalAmount);
            if (!isPaymentValid)
                throw new InvalidOperationException("Payment validation failed.");

            var booking = _mapper.Map<Booking>(request);
            booking.Status = BookingStatus.ACTIVE;
            booking.TotalAmount = totalAmount;
            booking.Category = category;
            booking.QRCode=GenerateQrCode($"{booking.CustomerName}_{booking.Id}_{DateTime.UtcNow}");

            category.AvailableSeats -= request.Quantity;
            booking.CreateTime = DateTime.UtcNow;
            //await _dbContext.SaveChangesAsync();

            var savedBooking = await _bookingRepository.AddAsync(booking);
            return _mapper.Map<BookingResponseDto>(savedBooking);
        }
        private async Task<bool> ValidatePaymentAsync(string paymentToken, decimal amount)
        {
            await Task.Delay(100); 
            return !string.IsNullOrEmpty(paymentToken) && amount > 0;
        }

        private string GenerateQrCode(string content)
        {
            byte[] qrBytes = Encoding.UTF8.GetBytes(content); 
            return Convert.ToBase64String(qrBytes);
        }
        public async Task<CancelBookingResponseDto> CancelBookingAsync(int bookingId)
        {
            var booking=await _bookingRepository.GetBookingByIdAsync(bookingId);
            if(booking == null)
            {
                throw new InvalidOperationException("Booking Not Found");
            }
            if ((DateTime.UtcNow - booking.CreateTime).TotalHours > 48)
            {
                throw new InvalidOperationException("Cannot cancel after 48 hours!!");
            }
            double refundAmount = CalculateRefund(booking);

            await _bookingRepository.CancelBookingAsync(booking);
            return new CancelBookingResponseDto
            {
                BookingId = booking.Id,
                IsCancelled = true,
                RefundAmount = refundAmount,
                CancellationDate = booking.CancellationDate ?? DateTime.UtcNow
            };
        }
        private double CalculateRefund(Booking booking)
        {
            var timeUntilEvent = (booking.Event.DateTime - DateTime.UtcNow).TotalHours;
            if (timeUntilEvent > 48) return booking.Quantity * 0.9;
            if (timeUntilEvent > 24) return booking.Quantity * 0.5;
            return 0;
        }
    }
}