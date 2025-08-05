namespace DailyQuoteManager.Application.Common.Validators
{
    public class ValidationResult<T>
    {
        #region Properties

        public bool IsValid { get; set; }

        public string? ErrorMessage { get; set; }

        public T? Value { get; set; }

        #endregion Properties

        #region Public Methods

        public static ValidationResult<T> Success(T value) =>
            new() { IsValid = true, Value = value };

        public static ValidationResult<T> Fail(string error) =>
            new() { IsValid = false, ErrorMessage = error };

        #endregion Public Methods
    }
}
