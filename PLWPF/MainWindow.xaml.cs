using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BL.IBL bl;
        BackgroundWorker t;
        public MainWindow()
        {
            InitializeComponent();
            t = new BackgroundWorker();
            t.DoWork += melody;
            t.RunWorkerAsync();

            bl = BL.BLFactory.GetBL();
        }

        private void melody(object sender, DoWorkEventArgs e)
        {
            SoundPlayer playSound = new SoundPlayer("10 - La Belle au bois dormant - Suite de ballet  op. 66 - 04 - Panorama.wav");
            playSound.PlayLooping();
        }
        private void Trainee_Click(object sender, RoutedEventArgs e)
        {
            Window Trainee = new TraineeWindow();
            Trainee.Show();
        }

        private void Tester_Click(object sender, RoutedEventArgs e)
        {
           
           new  TesterWindow().Show();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {

            new TestWindow().Show();
        }
        private void Linq_Click(object sender, RoutedEventArgs e)
        {

            new LinqWindow().Show();
        }
        //we will do it it arrtibute 
        /*  private void Linq_Click(object sender, RoutedEventArgs e)
          {
              Window Linq = new LinqWindow();
              Linq.Show();*/
        // }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            new ManagerOptions().Show();

        }
       
    }
}


