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
    /// Interaction logic for TraineeWindow.xaml
    /// </summary>
    public partial class TraineeWindow : Window
    {
        IBL bl;
        Trainee trainee;
        public Trainee trainee1;
        public Trainee trainee2;
        /// <summary>
        /// updating the grid on the window
        /// </summary>
        public TraineeWindow()//ctor
        {
            InitializeComponent();
            bl = BLFactory.GetBL();
            trainee = new Trainee();
            this.DataContext = trainee;
            trainee1 = new Trainee();
            trainee2 = new Trainee();
            trainee1.DateOfBirth = DateTime.Now;
            trainee2.DateOfBirth = DateTime.Now;
            Addgrid1.DataContext = trainee1;
            grid3.DataContext = trainee2;
            UpdateAllComboBox();
        }
        /// <summary>
        /// Updating all the combo box on the """"" window
        /// </summary>
        public void UpdateAllComboBox()
        {
          
            this.genderTraineeComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.carLearnedComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarLearnedType));
            this.gearBoxTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));

            this.updateidComboBox.ItemsSource = bl.GetAllTrainees();
            this.updateidComboBox.DisplayMemberPath = "ID";
            this.updateidComboBox.SelectedValuePath = "ID";

            this.gendertraineeComboBox1.ItemsSource = Enum.GetValues(typeof(BE.Gender));
           

            this.removeidComboBox.ItemsSource = bl.GetAllTrainees();
            this.removeidComboBox.DisplayMemberPath = "ID";
            this.removeidComboBox.SelectedValuePath = "ID";

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // System.Windows.Data.CollectionViewSource TraineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("TraineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // TraineeViewSource.Source = [generic data source]
        }
        /// <summary>
        /// adding a trainee to the system, sends an exeption if something is typed wrongly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddTrainee(trainee1);
                trainee1 = new Trainee();
                trainee1.DateOfBirth = DateTime.Now;
                Addgrid1.DataContext = trainee1;
                UpdateAllComboBox();
                MessageBox.Show("The trainee was added successfully");
            }
            catch(Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        /// <summary>
        ///  Updating a Trainee to the system, sends an exeption if something is typed wrongly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (trainee2 != null)
                {
                    bl.UpdateTrainee(trainee2);
                }
                trainee2 = new Trainee();
                updategrid2.DataContext = trainee2;
                trainee2.DateOfBirth = DateTime.Now;
                UpdateAllComboBox();
                MessageBox.Show("The Trainee was updated successfully");
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        /// <summary>
        /// copying all the relevent data to a trainee so we could use it to update all the data when a trainee is choosen 
        /// in the relevent combo box
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>

        private Trainee Copyc(Trainee c)
        {
            if (c == null)
            {
                return null;
            }
            return (new Trainee()
            {
                ID = c.ID,
                Name = c.Name,
                LastName=c.LastName,
                DateOfBirth = c.DateOfBirth,
                PhoneNum=c.PhoneNum,
                CarLearnedRn=c.CarLearnedRn,
                NameOfschool=c.NameOfschool,
                TeacherName=c.TeacherName,
                TraineeGender = c.TraineeGender,
                HasTester = c.HasTester
            });
        }
        /// <summary>
        /// When Trainee selected in combo box- all the fields are updated to the selected trainee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateidComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           Trainee c = updateidComboBox.SelectedItem as Trainee;
            if (c != null)
            {
               trainee2 = Copyc(c);
                updategrid2.DataContext = trainee2;
            }
        }
        /// <summary>
        /// removing a Trainee from the system, sends an exeption if no trainee chas been choosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Trainee c = removeidComboBox.SelectedItem as Trainee;
                if (c == null)
                    throw new Exception("You must choose a Trainee first");
                // trainee1.HasTester = true;
                bl.RemoveTrainee(c.ID);
                MessageBox.Show("The Trainee was removed successfully");
                this.removeidComboBox.ItemsSource = null;
                UpdateAllComboBox();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }

        }

        
    }   
}
