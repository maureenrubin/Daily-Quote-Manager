using AutoMapper;
using DailyQuoteManager.Application.Contracts.Interfaces.Quote;
using DailyQuoteManager.Application.DTOs.Quote.Quotes;
using DailyQuoteManager.Client.DTOs.Quotes;
using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Application.Common.Validators;
using DailyQuoteManager.Domain.Entities;
namespace DailyQuoteManager.Application.Services.Quote
{
    public class QuoteService : IQuoteService
    {
        #region Fields

        private readonly IQuotesRepository quotesRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly QuotesValidor quotesValidor;        

        #endregion Fields

        #region Public Constructors

        public QuoteService(
            IQuotesRepository quotesRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            QuotesValidor quotesValidor
            )
        {
            this.quotesRepository = quotesRepository ?? throw new ArgumentNullException(nameof(quotesRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mapper = mapper ?? throw new ArgumentNullException (nameof(mapper));
            this.quotesValidor = quotesValidor ?? throw new ArgumentNullException(nameof(quotesValidor));
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ValidationResult<QuotesOutputDto>> CreateQuotesAsync(QuotesInputReqDto request)
        {
            if(request == null) return ValidationResult<QuotesOutputDto>.Fail(nameof(request));

            quotesValidor.QuotesCreate(request);

            var quote = new Quotes
            {
                Text = request.Text,
                Author = request.Author,
                //Category = request.Category?.ToString(),
                AddedByUserId = request.AddedByUserId.Value,
                IsPublic = request.IsPublic,
                CreatedAt = request.CreatedAt
            };

            var createQuote = await quotesRepository.AddAsync(quote);
            await unitOfWork.SaveChangesAsync();

            var quotesDto = mapper.Map<QuotesOutputDto>(createQuote);

            return ValidationResult<QuotesOutputDto>.Success(quotesDto);
        }

        #endregion Public Methods

    }
}
