using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace TyreDegradation.MainApp.Views
{
    public partial class TemperatureInput : UserControl
    {
        public TemperatureInput()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}