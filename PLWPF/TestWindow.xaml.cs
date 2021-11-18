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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        IBL bl;
        public Test test1;
        public Test test2;
        public Trainee trainee1;//to check if the trainee deserves a licens
        public DateTime wantedDate;

        public double tess;
       // public string source;
       // public int distance;

        public TestWindow()
        {
            InitializeComponent();
            bl = BLFactory.GetBL();
            test1 = new Test();
            test1.TestDayNHour = DateTime.Now;
            test2 = new Test();
            wantedDate = new DateTime();
            wantedDate.AddHours(8);
            trainee1 = new Trainee();//check if he pased
            
           // wantedDate= test1.TestDayNHour ;
             test2.TestDayNHour = DateTime.Now;
           // test2.TestDayNHour = wantedDate;
             this.carTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarLearnedType));
            this.carTypeComboBox1.ItemsSource = Enum.GetValues(typeof(BE.CarLearnedType));
            Addgrid.DataContext = test1;
            Updategrid.DataContext = test2;
         //  tess = dateOfTest;
            UpdateAllCombox();
        }
        
        /// Updating all the combo box on the test window

        private void UpdateAllCombox()
        {
            //i changed!!!
            this.idTraineeComboBox.ItemsSource = bl.GetAllTrainees();
            this.idTraineeComboBox.DisplayMemberPath = "ID";
            this.idTraineeComboBox.SelectedValuePath = "ID";

            this.idTesterComboBox.ItemsSource = bl.GetAllTesters();
            this.idTesterComboBox.DisplayMemberPath = "ID";
            this.idTesterComboBox.SelectedValuePath = "ID";

            this.testNumComboBox.ItemsSource = bl.GetAllTests();
            this.testNumComboBox.DisplayMemberPath = "NumOfTest";
            this.testNumComboBox.SelectedValuePath = "NumOfTest";


            this.removeTest.ItemsSource = bl.GetAllTests();
            this.removeTest.DisplayMemberPath = "NumOfTest";
            this.removeTest.SelectedValuePath = "NumOfTest";

            this.updateTesterComboBox.ItemsSource = bl.GetAllTesters();
            this.updateTesterComboBox.DisplayMemberPath = "ID";
            this.updateTesterComboBox.SelectedValuePath = "ID";

            this.updateTraineeComboBox.ItemsSource = bl.GetAllTrainees();
            this.updateTraineeComboBox.DisplayMemberPath = "ID";
            this.updateTraineeComboBox.SelectedValuePath = "ID";

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource TestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("TestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // TestViewSource.Source = [generic data source]
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddTest(test1, test1.TestDayNHour);
                // test1 = bl.GetAllTests(t => t.StudentID == test1.StudentID && t.NumOfTest == test1.NumOfTest).FirstOrDefault();
               // test1 = bl.GetAllTests(t => t.StudentID == test1.StudentID && t.TesterID == test1.TesterID ).FirstOrDefault();
                test1 = bl.GetAllTests(t => t.StudentID == test1.StudentID && t.TesterID == test1.TesterID && t.TestDayNHour==test1.TestDayNHour).FirstOrDefault();
                
                Addgrid.DataContext = test1;
                MessageBox.Show("The test " + test1.NumOfTest.ToString() + " was added successfully");
                test1 = new Test();
                Addgrid.DataContext = test1;
                UpdateAllCombox();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (test2 != null) 
                    //CheckSuccsess(test2);
                bl.UpdateTest(test2);
                test2 = new Test();
                trainee1 = new Trainee();
                Updategrid.DataContext = test2;
                UpdateAllCombox();
                MessageBox.Show("The test was updated successfully");
               Update.IsEnabled = true;
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private Test CopyT(Test t)
        {
            if (t == null)
            {
                return null;
            }
            return (new Test
            {
                TestDayNHour = t.TestDayNHour,
                TesterID = t.TesterID,
                StudentID = t.StudentID,
                CarTestedRn = t.CarTestedRn,
                PastOrFailed = t.PastOrFailed,
                NoteOfTester = t.NoteOfTester,
                DistanceMaintainance = t.DistanceMaintainance,
                StartingAdress = t.StartingAdress,
                NumOfTest=t.NumOfTest


            });
        }

        //private void TraineeIDCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if(idTraineeComboBox.SelectedIndex!=-1)
        //    {
        //       // trainee = (Trainee)this.idTraineeComboBox.SelectedItem;
        //    }
        //}
        //    private void TesterIdCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        /// When trainee id selected in combo box- all field updates to the trainee selected

        //private void TestNumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Test t = idTraineeComboBox.SelectedItem as Test;
        //    if (t != null)
        //    {
        //        test2 = CopyT(t);
        //        Updategrid.DataContext = test2;

        //    }
        //}

        private void testNumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Test t = testNumComboBox.SelectedItem as Test;
                 if (t != null)
                 {
                test2 = CopyT(t);
                Updategrid.DataContext = test2;
                 }
        }

        private void CheckSuccsess(Test test)
        {
            if (MOcheckbox.IsChecked == true)
                test2.SuccsesfulCriteria[0] = true;
            if (SCcheckBox.IsChecked == true)
                test2.SuccsesfulCriteria[1] = true;
            if (RPcheckBox.IsChecked == true)
                test2.SuccsesfulCriteria[2] = true;
            if (DMcheckBox.IsChecked == true)
                test2.SuccsesfulCriteria[3] = true;
            if (SignalCheckBox.IsChecked == true)
                test2.SuccsesfulCriteria[4] = true;

        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Test t = removeTest.SelectedItem as Test;
                if (t == null)
                    throw new Exception("You must choose a num of test first");
                bl.RemoveTest(t.NumOfTest);
                MessageBox.Show("The test was removed successfully");
                this.removeTest.ItemsSource = null;
                UpdateAllCombox();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

    }
}
