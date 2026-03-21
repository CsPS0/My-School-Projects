using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace solticsongor_Eletjatek;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeComboBoxes();
    }

    private void InitializeComboBoxes()
    {
        for (int i = 5; i <= 20; i++)
        {
            cbRows.Items.Add(i);
            cbCols.Items.Add(i);
        }
        cbRows.SelectedItem = 20;
        cbCols.SelectedItem = 20;
    }

    private void BtnCreate_Click(object sender, RoutedEventArgs e)
    {
        if (cbRows.SelectedItem == null || cbCols.SelectedItem == null) return;

        int rows = (int)cbRows.SelectedItem;
        int cols = (int)cbCols.SelectedItem;

        matrixGrid.Children.Clear();
        matrixGrid.Rows = rows;
        matrixGrid.Columns = cols;

        for (int i = 0; i < rows * cols; i++)
        {
            CheckBox cb = new CheckBox();
            cb.HorizontalAlignment = HorizontalAlignment.Center;
            cb.VerticalAlignment = VerticalAlignment.Center;
            cb.Margin = new Thickness(2);
            matrixGrid.Children.Add(cb);
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (matrixGrid.Children.Count == 0)
        {
            MessageBox.Show("Nincs létrehozott mátrix!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        int rows = matrixGrid.Rows;
        int cols = matrixGrid.Columns;

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int index = i * cols + j;
                if (index < matrixGrid.Children.Count)
                {
                    CheckBox cb = (CheckBox)matrixGrid.Children[index];
                    sb.Append(cb.IsChecked == true ? "1" : "0");
                }
            }
            sb.AppendLine();
        }

        string fileName = $"Eletjatek_{rows}x{cols}.txt";
        try
        {
            File.WriteAllText(fileName, sb.ToString());
            MessageBox.Show($"Állás sikeresen mentve: {fileName}", "Mentés", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hiba a mentés során: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
