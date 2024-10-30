using AutoMapper;
using EventTicketBookingApi.Contract;
using EventTicketBookingApi.Data;

namespace EventTicketBookingApi.Service
{
    public class BookService
    {
        private readonly EventTicketBookingDbContext _dbContext;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventsRepository _eventRepository;
        private readonly IMapper _mapper;
        public BookService(EventTicketBookingDbContext context, IBookingRepository bookingRepository,
            IEventsRepository eventsRepository, IMapper mapper)
        {
            this._dbContext = context;
            this._bookingRepository = bookingRepository;
            this._eventRepository = eventsRepository;
            this._mapper = mapper;
        }
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            // Check booking limit
            if (await _bookingRepository.HasReachedBookingLimitAsync(booking.CustomerEmail, booking.EventId))
                throw new Exception("Booking limit reached (maximum 6 tickets)");
            var @event = await _eventRepository.GetEventWithCategoriesAsync(booking.EventId);
            var category = @event.TicketCategories.FirstOrDefault(c => c.Id == booking.CategoryId);

            if (category.AvailableSeats < booking.Quantity)
                throw new Exception("Insufficient available seats");
            category.AvailableSeats -= booking.Quantity;
            await _bookingRepository.AddAsync(booking);
            return booking;

        }
    }
