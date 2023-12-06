using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GenerateChessBoard();
        }

        private void GenerateChessBoard()
        {
            string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h" };

            for (int row = 0; row < 9; row++)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Background = new SolidColorBrush(Color.FromRgb(47, 48, 44));
                Label label = new Label();

                if (row != 8)
                {
                    label.Content = 8 - row;
                }
                else
                {
                    label.Content = "";
                }

                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.Foreground = new SolidColorBrush(Color.FromRgb(215, 215, 215));
                label.Height = 91;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.FontSize = 22;
                stackPanel.Children.Add(label);

                Grid.SetRow(stackPanel, row);
                Grid.SetColumn(stackPanel, 0);
                MainGrid.Children.Add(stackPanel);
            }
            for (int col = 1; col < 9; col++)
            {

                StackPanel stackPanel1 = new StackPanel();
                stackPanel1.Background = new SolidColorBrush(Color.FromRgb(47, 48, 44));

                Label label = new Label();
                label.Content = letters[col - 1];
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.Foreground = new SolidColorBrush(Color.FromRgb(215, 215, 215));
                label.Height = 47;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.FontSize = 22;
                stackPanel1.Children.Add(label);


                Grid.SetRow(stackPanel1, 8);
                Grid.SetColumn(stackPanel1, col);
                MainGrid.Children.Add(stackPanel1);
            }
            for (int str = 0; str<8; str++) 
                for (int col = 1; col<9; col++)
                {
                    StackPanel stackPanel2 = new StackPanel();
                    if (str%2==0)
                    {
                        if (col%2==0)
                            stackPanel2.Background = new SolidColorBrush(Color.FromRgb(58, 128, 43));
                        if (col%2==1)
                            stackPanel2.Background = new SolidColorBrush(Color.FromRgb(244, 255, 221));
                    }
                    else 
                    {
                        if (col%2==0)
                            stackPanel2.Background = new SolidColorBrush(Color.FromRgb(244, 255, 221));
                        if (col%2==1)
                            stackPanel2.Background = new SolidColorBrush(Color.FromRgb(58, 128, 43));
                    }
                    Grid.SetRow(stackPanel2, str);
                    Grid.SetColumn(stackPanel2, col);
                    MainGrid.Children.Add(stackPanel2);
                }


        }
    }
}
