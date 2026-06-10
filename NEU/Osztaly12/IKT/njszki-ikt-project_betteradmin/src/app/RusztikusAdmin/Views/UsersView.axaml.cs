using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RusztikusAdmin.Views
{
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}