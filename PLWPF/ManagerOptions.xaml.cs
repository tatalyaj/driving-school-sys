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
using System.Windows.Shapes;
//PAY ATTENTION_THE PASSWORD IS-      n@poleon     !!!!!!!!!!!!!!!!!!!
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ManagerOptions.xaml (password check)
    /// </summary>
    public partial class ManagerOptions : Window
    {
        public ManagerOptions()
        {
            InitializeComponent();
        }
        /// <summary>
        /// if password is correct the next window opens, if wrong password will show an eroor box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PasswordBox.Password.ToString() != "n@poleon")
                {
                    PasswordBox.BorderBrush = Brushes.Red;
                    throw new Exception(" Incorrect password");
                }
                Close();
                new AllDetailsGrid().Show();
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message, m.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
