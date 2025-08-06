using AutoMapper;
using DailyQuoteManager.Application.Contracts.Interfaces.Quote;
using DailyQuoteManager.Application.DTOs.Quote.Quotes;
using DailyQuoteManager.Client.DTOs.Quotes;
using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Application.Common.Validators;
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Enums;
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
            //    Category = Enum.Parse<QuoteCategory>(request.Category),
                AddedByUserId = request.AddedByUserId.Value,
                IsPublic = request.IsPublic,
                CreatedAt = request.CreatedAt
            };

            var createQuote = await quotesRepository.AddAsync(quote);
            await unitOfWork.SaveChangesAsync();

            var quotesDto = mapper.Map<QuotesOutputDto>(createQuote);

            return ValidationResult<QuotesOutputDto>.Success(quotesDto);
        }

        public async Task<ValidationResult<QuotesOutputDto>> UpdateQuotesAsync(Guid quotesId, QuotesInputReqDto request)
        {
            var existingQuote = await quotesRepository.GetByIdAsync(quotesId);
            if (existingQuote == null) return ValidationResult<QuotesOutputDto>.Fail($"Election with ID {quotesId} not found");

            existingQuote.Text = request.Text;
            existingQuote.Author = request.Author;
         //   existingQuote.Category = Enum.Parse<QuoteCategory>(request.Category);
            existingQuote.IsPublic = request.IsPublic;
            existingQuote.CreatedAt = request.CreatedAt;

            var updateQuotes = await quotesRepository.UpdateAsync(existingQuote);
            await unitOfWork.SaveChangesAsync();

            var updateQuotesDto = mapper.Map<QuotesOutputDto>(existingQuote);

            return ValidationResult<QuotesOutputDto>.Success(updateQuotesDto);
        }


        public async Task<ValidationResult<QuotesInputReqDto>> DeleteQuotesAsync(Guid quotesId)
        {
            var quotes = await quotesRepository.GetByIdAsync(quotesId);
            if(quotes == null) return ValidationResult<QuotesInputReqDto>.Fail($"Election with ID {quotesId} not found");

            await quotesRepository.DeleteByIdAsync(quotesId);
            await unitOfWork.SaveChangesAsync();

            var deletedQuotes = mapper.Map<QuotesInputReqDto>(quotes);

            return ValidationResult<QuotesInputReqDto>.Success(mapper.Map<QuotesInputReqDto>(deletedQuotes));

        }
        #endregion Public Methods

    }
}
