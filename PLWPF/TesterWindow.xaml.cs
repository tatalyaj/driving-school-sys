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
    /// Interaction logic for TesterWindow.xaml
    /// </summary>
    public partial class TesterWindow : Window
    {
        IBL bl;
        Tester tester;
        public Tester tester1;
        public Tester tester2;
        /// <summary>
        /// updating the grid on the window
        /// </summary>
        public TesterWindow()//ctor
        {
            InitializeComponent();
            bl = BLFactory.GetBL();
            tester = new Tester();
            this.DataContext = tester;
            tester1 = new Tester();
            tester2 = new Tester();
            tester1.DateOfBirth = DateTime.Now;
            tester2.DateOfBirth = DateTime.Now;
            Addgrid1.DataContext = tester1;
            grid3.DataContext = tester2;
            //updategrid2.DataContext = tester2;
            UpdateAllComboBox();
        }
        /// <summary>
        /// Updating all the combo box on the """"" window
        /// </summary>
        public void UpdateAllComboBox()
        {
            this.updateidComboBox.ItemsSource = bl.GetAllTesters();
            this.updateidComboBox.DisplayMemberPath = "ID";
            this.updateidComboBox.SelectedValuePath = "ID";

            this.genderTesterComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.carSpecialityComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarSpeciality));
            

            this.removeidComboBox.ItemsSource = bl.GetAllTesters();
            this.removeidComboBox.DisplayMemberPath = "ID";
            this.removeidComboBox.SelectedValuePath = "ID";

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // System.Windows.Data.CollectionViewSource TesterViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("TesterViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // TesterViewSource.Source = [generic data source]
        }
        /// <summary>
        /// adding a tester to the system, sends an exeption if something is typed wrongly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScheduleFirst(tester1);  ///To see whitch tester is available
                bl.AddTester(tester1);
                tester1 = new Tester();
                tester1.DateOfBirth = DateTime.Now;
                Addgrid1.DataContext = tester1;
                UpdateAllComboBox();
                MessageBox.Show("The tester was added successfully");
               
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
   
        }
       

        /// <summary>
        ///  Updating a Tester to the system, sends an exeption if something is typed wrongly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tester2 != null)
                {
                    bl.UpdateTester(tester2);
                }
                tester2 = new Tester();
                updategrid2.DataContext = tester2;
                tester2.DateOfBirth = DateTime.Now;
                UpdateAllComboBox();
                MessageBox.Show("The Tester was updated successfully");
                ScheduleSecond(tester1);
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }

           
        }
        /// <summary>
        /// copying all the relevent data to a tester so we could use it to update all the data when a tester is choosen 
        /// in the relevent combo box
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>

        private Tester Copyc(Tester c)
        {
            if (c == null)
            {
                return null;
            }
            return (new Tester()
            {
                ID = c.ID,
                Name = c.Name,
                LastName = c.LastName,
                DateOfBirth = c.DateOfBirth,
                PhoneNum = c.PhoneNum,
                TesterCarSpeciality = c.TesterCarSpeciality,
                YearsOfExperiance = c.YearsOfExperiance,
                DrinksAlcohol = c.DrinksAlcohol,
                TesterGender = c.TesterGender,
                MaxDistance = c.MaxDistance,
                Smoker=c.Smoker,
                TesterWorkDays=c.TesterWorkDays,
                Cash =c.Cash,
                MaxTests=c.MaxTests
            });
        }
        /// <summary>
        /// When tester selected in combo box- all the fields are updated to the selected tester
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateidComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Tester c = updateidComboBox.SelectedItem as Tester;
            if (c != null)
            {
                tester2 = Copyc(c);
                updategrid2.DataContext = tester2;
            }
        }
        /// <summary>
        /// removing a tester from the system, sends an exeption if no tester chas been choosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tester c = removeidComboBox.SelectedItem as Tester;
                if (c == null)
                    throw new Exception("You must choose a Tester first");
                bl.RemoveTester(c.ID);
                MessageBox.Show("The Tester was removed successfully");
                this.removeidComboBox.ItemsSource = null;
                UpdateAllComboBox();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }

        }
        //********************************************************************************//

        /// <summary>
        /// /sunday
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void ScheduleFirst(Tester tester)
        {
            //Sunday
            if (SunNineCheckBox.IsChecked==true)
                tester1.TesterWorkDays[0, 0] = true;
            if (SunTenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[0, 1] =true;
            if (SunElevenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[0, 2] = true;
            if (SunTwelveCheckBox.IsChecked == true)
                tester1.TesterWorkDays[0, 3] = true;
            if (SunTherteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[0, 4] = true;
            if (SunFourteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[0, 5] = true;


            //Monday
            if (MonNineCheckBox.IsChecked == true)
                tester1.TesterWorkDays[1, 0] = true;
            if (MonTenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[1, 1] = true;
            if (MonElevenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[1, 2] = true;
            if (MonTwelveCheckBox.IsChecked == true)
                tester1.TesterWorkDays[1, 3] = true;
            if (MonTherteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[1, 4] = true;
            if (MonFourteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[1, 5] = true;
            //Tuesday
            if (TuesNineCheckBox.IsChecked == true)
                tester1.TesterWorkDays[2, 0] = true;
            if (TuesTenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[2, 1] = true;
            if (TuesElevenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[2, 2] = true;
            if (TuesTwelveCheckBox.IsChecked == true)
                tester1.TesterWorkDays[2, 3] = true;
            if (TuesTherteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[2, 4] = true;
            if (TuesFourteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[2, 5] = true;

            //Wednsday
            if (WednsNineCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 0] = true;
            if (WednsTenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 1] = true;
            if (WednsElevenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 2] = true;
            if (WednsTwelveCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 3] = true;
            if (WednsTherteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 4] = true;
            if (WednsFourteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 5] = true;

            //THursday
            if (ThursNineCheckBox.IsChecked == true)
                tester1.TesterWorkDays[4, 0] = true;
            if (ThursTenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[4, 1] = true;
            if (ThursElevenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[4, 2] = true;
            if (ThursTwelveCheckBox.IsChecked == true)
                tester1.TesterWorkDays[4, 3] = true;
            if (ThursTherteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[4, 4] = true;
            if (ThursFourteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[4, 5] = true;


        }
        private void ScheduleSecond(Tester tester)
        {
            //Sunday
            if (SunNineCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[0, 0] = true;
            if (SunTenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[0, 1] = true;
            if (SunElevenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[0, 2] = true;
            if (SunTwelveCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[0, 3] = true;
            if (SunTherteenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[0, 4] = true;
            if (SunFourteenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[0, 5] = true;


            //Monday
            if (MonNineCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[1, 0] = true;
            if (MonTenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[1, 1] = true;
            if (MonElevenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[1, 2] = true;
            if (MonTwelveCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[1, 3] = true;
            if (MonTherteenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[1, 4] = true;
            if (MonFourteenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[1, 5] = true;
            //Tuesday
            if (TuesNineCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[2, 0] = true;
            if (TuesTenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[2, 1] = true;
            if (TuesElevenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[2, 2] = true;
            if (TuesTwelveCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[2, 3] = true;
            if (TuesTherteenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[2, 4] = true;
            if (TuesFourteenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[2, 5] = true;

            //Wednsday
            if (WednsNineCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 0] = true;
            if (WednsTenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 1] = true;
            if (WednsElevenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 2] = true;
            if (WednsTwelveCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 3] = true;
            if (WednsTherteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 4] = true;
            if (WednsFourteenCheckBox.IsChecked == true)
                tester1.TesterWorkDays[3, 5] = true;

            //THursday
            if (ThursNineCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[4, 0] = true;
            if (ThursTenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[4, 1] = true;
            if (ThursElevenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[4, 2] = true;
            if (ThursTwelveCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[4, 3] = true;
            if (ThursTherteenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[4, 4] = true;
            if (ThursFourteenCheckBox1.IsChecked == true)
                tester1.TesterWorkDays[4, 5] = true;
        }



    }
}
