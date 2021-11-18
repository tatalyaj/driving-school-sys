using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
  public  class Test
    {
        public int NumOfTest { get; set; }
        public int TesterID { get; set; }
        public int StudentID { get; set; }
        //public string ScheduledTest { get; set; }
        public DateTime TestDayNHour { get; set; }
        public Adress StartingAdress { get; set; }//the starting adress for the test itself
        public bool DistanceMaintainance { get; set; }
        public bool ReverseParking { get; set; }
        public bool MirrorsObservation { get; set; }
        public bool Signal { get; set; }
        public bool StearingControl { get; set; }
        public CarTestedType CarTestedRn { get; set; }
        public bool PastOrFailed { get; set; }
        public string NoteOfTester { get; set; }
        [XmlIgnore]
        public bool[] SuccsesfulCriteria = new bool[5];//if past-true//@@@@@maybe we will change that//didnt do to string for that!!



        public override string ToString()
        {
            return/* "The test's number: " + NumOfTest +*/ "Tester's ID: " + TesterID
                + "Student's ID: " + StudentID //+ "The date that wad scheduled for the test: " + ScheduledTest
                + "Test's day that was scheduled for the test: " +  TestDayNHour.Year+ "/" + TestDayNHour.Month + "/" + TestDayNHour.Day 
                +"The test's hour is :" + TestDayNHour.Hour + ":" + TestDayNHour.Minute
               + "the test's starting adress: " + StartingAdress + "Car tested Type: " + CarTestedRn
               + "Did the Trainee passed or Failed " + PastOrFailed + "Tester's notes: " + NoteOfTester + "/n";
        }

        public string TempSuccsesfulCriteria
        {
            get
            {
                if (SuccsesfulCriteria == null)
                    return null;
                string result = "";
                if (SuccsesfulCriteria != null)
                {
                    // dimension of the matrix 
                    int sizeA = SuccsesfulCriteria.GetLength(0);//5
                   
                    result += "" + sizeA ;
                    // regroup all the values of the matrix in result 
                    for (int i = 0; i < sizeA; i++)  
                            result += "," + SuccsesfulCriteria[i];
                }
                return result;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    // dimension 
                    int sizeA = int.Parse(values[0]);//5

                    // definition of the matrix 
                    SuccsesfulCriteria = new bool[sizeA];
                    int index = 2;
                    // enter all values in NeedShedule
                    for (int i = 0; i < sizeA; i++)

                        SuccsesfulCriteria[i] = bool.Parse(values[index++]);
                }
            }
        }

        public bool this[int i, int j]
        {
            get { return SuccsesfulCriteria[i]; }
            set { SuccsesfulCriteria[i] = value; }
        }

    }
}                                                                                                                                                                                                                              
