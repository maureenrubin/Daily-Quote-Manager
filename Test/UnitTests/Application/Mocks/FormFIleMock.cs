using Microsoft.AspNetCore.Http;

namespace Test.UnitTests.Application.Mocks
{
    public class FormFileMock : IFormFile
    {
        #region Public Constructors

        public FormFileMock(string fileName)
        {
            FileName = fileName;
            Length = 1;
        }

        #endregion Public Constructors


        #region Properties

        public string ContentType => "image/jpeg";
        public string ContentDisposition => "form-data";
        public IHeaderDictionary Headers => new HeaderDictionary();
        public long Length { get; }
        public string Name => "file";
        public string FileName { get; }

        #endregion Properties


        #region Public Methods

        public void CopyTo(Stream target)
        { }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Stream OpenReadStream() => new MemoryStream();

        #endregion Public Methods
    }

}

