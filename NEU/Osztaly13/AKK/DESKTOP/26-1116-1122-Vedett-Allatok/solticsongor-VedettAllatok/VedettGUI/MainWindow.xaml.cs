using System.Windows;
using System.Windows.Controls;
using VedettLibrary;

namespace VedettGUI;

public partial class MainWindow : Window
{
    AnimalApiHandler api = new AnimalApiHandler("http://localhost:5052/");

    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            CategoryListBox.ItemsSource = await api.GetCategories();
        }
        catch
        {
            MessageBox.Show("Nem sikerült elérni az API-t! Indítsd el előbb a solticsongor-VedettAllatok projektet.");
        }
    }

    private async void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CategoryListBox.SelectedItem == null)
        {
            return;
        }
        string category = CategoryListBox.SelectedItem.ToString()!;
        List<Animal> animals = await api.GetAnimalsByCategory(category);
        AnimalListBox.ItemsSource = animals.OrderBy(a => a.Name).ToList();
        DetailsText.Text = "";
    }

    private void AnimalListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (AnimalListBox.SelectedItem == null)
        {
            return;
        }
        Animal animal = (Animal)AnimalListBox.SelectedItem;
        DetailsText.Text = "ID: " + animal.Id + "\nNév: " + animal.Name + "\nÉrték: " + animal.Value + " Ft\nÉv: " + animal.Year + "\nKategória: " + animal.Category;
    }

    private async void LoadButton_Click(object sender, RoutedEventArgs e)
    {
        EditMessage.Text = "";
        int id;
        if (!int.TryParse(IdTextBox.Text, out id))
        {
            EditMessage.Text = "Hibás ID!";
            return;
        }
        Animal? animal = await api.GetAnimalById(id);
        if (animal == null)
        {
            EditMessage.Text = "Nincs ilyen ID-jű állat!";
            EditNameTextBox.Text = "";
            EditValueTextBox.Text = "";
            EditYearTextBox.Text = "";
            EditCategoryTextBox.Text = "";
            return;
        }
        EditNameTextBox.Text = animal.Name;
        EditValueTextBox.Text = animal.Value;
        EditYearTextBox.Text = animal.Year.ToString();
        EditCategoryTextBox.Text = animal.Category;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        int id;
        int year;
        if (!int.TryParse(IdTextBox.Text, out id) || !int.TryParse(EditYearTextBox.Text, out year))
        {
            EditMessage.Text = "Hibás ID vagy év!";
            return;
        }
        Animal animal = new Animal();
        animal.Id = id;
        animal.Name = EditNameTextBox.Text;
        animal.Value = EditValueTextBox.Text;
        animal.Year = year;
        animal.Category = EditCategoryTextBox.Text;

        bool success = await api.UpdateAnimal(animal);
        if (success)
        {
            EditMessage.Text = "Sikeres mentés!";
            CategoryListBox.ItemsSource = await api.GetCategories();
        }
        else
        {
            EditMessage.Text = "Nem sikerült a mentés!";
        }
    }

    private async void AddButton_Click(object sender, RoutedEventArgs e)
    {
        int year;
        if (NewNameTextBox.Text == "" || NewValueTextBox.Text == "" || NewCategoryTextBox.Text == "" || !int.TryParse(NewYearTextBox.Text, out year))
        {
            AddMessage.Text = "Tölts ki minden mezőt helyesen!";
            return;
        }
        Animal animal = new Animal();
        animal.Name = NewNameTextBox.Text;
        animal.Value = NewValueTextBox.Text;
        animal.Year = year;
        animal.Category = NewCategoryTextBox.Text;

        Animal? created = await api.AddAnimal(animal);
        if (created != null)
        {
            AddMessage.Text = "Sikeres felvitel! Az új ID: " + created.Id;
            NewNameTextBox.Text = "";
            NewValueTextBox.Text = "";
            NewYearTextBox.Text = "";
            NewCategoryTextBox.Text = "";
            CategoryListBox.ItemsSource = await api.GetCategories();
        }
        else
        {
            AddMessage.Text = "Nem sikerült a felvitel!";
        }
    }
}
