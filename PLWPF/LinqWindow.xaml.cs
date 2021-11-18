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
using BL;
using BE;

namespace PLWPF
{

    /// <summary>
    /// Interaction logic for Linq.xaml
    /// </summary>
    public partial class LinqWindow : Window
    {
        IBL bl;

        public LinqWindow()
        {
            InitializeComponent();
            bl = BLFactory.GetBL();


            this.CarSpeciality.ItemsSource = Enum.GetValues(typeof(BE.CarLearnedType));

            this.idTraineeComboBox.ItemsSource = bl.GetAllTrainees();
            this.idTraineeComboBox.DisplayMemberPath = "ID";
            this.idTraineeComboBox.SelectedValuePath = "ID";

        }


        private void MatchTester_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Trainee trainee = idTraineeComboBox.SelectedItem as Trainee;
                if (trainee == null)
                    throw new Exception("You must choose a trainee first");
                this.listTester.ItemsSource = bl.MatchTester(trainee);
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }
        }



        // Shows all the trainee that has no tester yet

        private void TraineeNoTester_Click(object sender, RoutedEventArgs e)
        {
            this.listTrainee.ItemsSource = bl.TraineeNoTesterFound();
            return;
        }


        // Shows all the trainee that no payed yet

        private void PayedOrNot_Click(object sender, RoutedEventArgs e)
        {
            this.listTrainee.ItemsSource = bl.PayedOrNot();
            return;
        }







        private void GroupByExp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool? sort = sorted.IsChecked;
                bool? byExp = sortByExp.IsChecked;
                this.listViewG.ItemsSource = bl.GroupingTesterSpeciality((bool)sort);
                return;
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void GroupByName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool? sort = sorted.IsChecked;
                this.listViewG.ItemsSource = bl.GroupingTraineeSchoolName((bool)sort);
                return;
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void ListTester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


}
