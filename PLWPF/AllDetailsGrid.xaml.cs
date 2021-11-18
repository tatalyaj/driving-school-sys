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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AllDetailsGrid.xaml
    /// </summary>
    public partial class AllDetailsGrid : Window
    {
        BL.IBL bl;
        //ctor
        public AllDetailsGrid()//ctor
        {
            InitializeComponent();
            bl = BL.BLFactory.GetBL();
        }
        /// <summary>
        /// Shows all testers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tester_Click(object sender, RoutedEventArgs e)
        {
            this.ShowAll.ItemsSource = bl.GetAllTesters();
            return;
        }
        /// <summary>
        /// Shows all trainee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Trainee_Click(object sender, RoutedEventArgs e)
        {
            this.ShowAll.ItemsSource = bl.GetAllTrainees();
            return;
        }
        /// <summary>
        /// Shows all childs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Test_Click(object sender, RoutedEventArgs e)
        {
            this.ShowAll.ItemsSource = bl.GetAllTests();
            return;
        }

       
    }
}

