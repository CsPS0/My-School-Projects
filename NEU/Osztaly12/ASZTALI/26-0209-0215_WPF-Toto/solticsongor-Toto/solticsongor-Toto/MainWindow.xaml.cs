using System.Windows;
using System.Windows.Controls;

namespace solticsongor_Toto;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Validate();
    }

    private void tbResults_TextChanged(object sender, TextChangedEventArgs e)
    {
        Validate();
    }

    private void Validate()
    {
        if (tbResults == null || cbLength == null || cbChars == null || btnSave == null) return;

        string input = tbResults.Text;
        bool lengthError = input.Length != 14;
        cbLength.IsChecked = lengthError;
        cbLength.Content = $"Nem megfelelő a karakterek száma ({input.Length})";

        string validChars = "12X";
        List<string> invalidChars = new List<string>();
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (!validChars.Contains(char.ToUpper(c)))
            {
                invalidChars.Add(c.ToString());
            }
        }

        bool charError = invalidChars.Count > 0;
        cbChars.IsChecked = charError;
        if (charError)
        {
            cbChars.Content = $"Helytelen karakter az eredményekben ({string.Join(";", invalidChars)})";
        }
        else
        {
            cbChars.Content = "Helytelen karakter az eredményekben ()";
        }

        btnSave.IsEnabled = !lengthError && !charError;
    }
}
