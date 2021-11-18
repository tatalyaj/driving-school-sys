using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class Tester
    {

        public int ID { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public Gender TesterGender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PhoneNum { get; set; }
        public Adress TesterAdress { get; set; }
        public int YearsOfExperiance { get; set; }
        public int MaxTests { get; set; }//this field contains the max nums of tests the tester is able to do per week
        public CarSpeciality TesterCarSpeciality { get; set; }
        public string City { get; set; }
        public double MaxDistance { get; set; }//the max distance from his adress that the tester may test a student
        public bool Smoker { get; set; }
        public bool DrinksAlcohol { get; set; }
        public bool Cash { get; set; }
        //public List<Trainee> TesterTrainee = new List<Trainee>();
        [XmlIgnore]
        public bool[,] TesterWorkDays = new bool[5, 6];
        //{
        //{ false, false, false, false, false,false },
        //{ false, false, false, false, false,false },
        //{ false, false, false, false, false,false },
        //{ false, false, false, false, false,false },
        //{ false, false, false, false, false,false },

        //};//days and then hours
     
        public string TempTesterWorkDays
        {
            get
            {
                if (TesterWorkDays == null)
                    return null;
                string result = "";
                if (TesterWorkDays != null)
                {
                    // dimension of the matrix 
                    int sizeA = TesterWorkDays.GetLength(0);//5
                    int sizeB = TesterWorkDays.GetLength(1);//6
                    result += "" + sizeA + "," + sizeB ;
                    // regroup all the values of the matrix in result 
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            result += "," + TesterWorkDays[i, j];
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
                    int sizeB = int.Parse(values[1]);//6
                    // definition of the matrix 
                    TesterWorkDays = new bool[sizeA, sizeB];
                    int index = 2;
                    // enter all values in NeedShedule
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            TesterWorkDays[i, j] = bool.Parse(values[index++]);
                }
            }
        }

        

        public override string ToString() {
            return "ID: " + ID + "Full Name: " + LastName + ' ' + Name + "Gender: " + TesterGender + "Date of birth: " + DateOfBirth + "Phone number: " + PhoneNum
                + "Adress: " + TesterAdress + "Experience: " + YearsOfExperiance + "Max amout of tests a tester is able to do per week: " + MaxTests
                + "The tester's car speciality: " + TesterCarSpeciality + "Days of work per week: " + TesterWorkDays + "The max distance from his house,a tester may teach(in Km): " + MaxDistance
               + "/n";
        }

        public bool this[int i,int j]
        {
            get { return TesterWorkDays[i, j]; }
            set { TesterWorkDays[i, j] = value; }
        }
    }
}
