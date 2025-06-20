using MudBlazor;

namespace DailyQuoteManager.Client.Utilities
{
    public class ShowPasswordUtil
    {
        #region Properties
        public InputType InputType { get; private set; } = InputType.Password;
        public string Icon { get; private set; } = Icons.Material.Filled.VisibilityOff;
        public bool IsPasswordVisible => InputType == InputType.Text;
        #endregion Properties

        #region Public Methods

        public void Toggle()
        {
            if(InputType == InputType.Password)
            {
                InputType = InputType.Text;
                Icon = Icons.Material.Filled.Visibility;
            }
            else
            {
                InputType = InputType.Password;
                Icon = Icons.Material.Filled.VisibilityOff;
            }
        }

        #endregion Public Methods
    }
}
