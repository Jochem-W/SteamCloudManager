using SteamTools.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SteamTools.Views
{
    partial class AchievementWindow : Window
    {
        public AchievementWindow()
        {
            InitializeComponent();
            DataContext = new AchievementWindowViewModel();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Icon")
            {
                var factory = new FrameworkElementFactory(typeof(Image));
                factory.SetValue(Image.SourceProperty, new Binding("Icon"));
                factory.SetValue(WidthProperty, (double)16);
                e.Column = new DataGridTemplateColumn
                {
                    Header = "Icon",
                    CellTemplate = new DataTemplate
                    {
                        VisualTree = factory
                    }
                };
            }
        }
    }
}
