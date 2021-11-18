using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee
    {
        public int ID { get; set; }
        public int TesterID { get; set; }//ADDED
        public string LastName { get; set; }
        public string Name { get; set; }
        public  Gender TraineeGender{ get; set; }
        public string PhoneNum { get; set; }
        public Adress TraineeAdress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public  CarLearnedType CarLearnedRn { get; set; }
        public  GearBox GearBoxType { get; set; }
        public string NameOfschool { get; set; }
        public string TeacherName { get; set; }
        public int NumOfLessons { get; set; }//the num of lessons taken
        public bool HasTester { get; set; }
        public bool PayedOrNot { get; set; }//true payed
        public string City { get; set; }
        //Struct for payment in Credit card
        

        public override string ToString()
        {
            return "Student's ID: " + ID + "Students full name: " + LastName + ' ' + Name
                + " Student's gender: " + TraineeGender + "Phone number: " + PhoneNum + "Adress: " + TraineeAdress
                + "Date of birth: " + DateOfBirth + "the learned Car's Type" + CarLearnedRn + " The Gearboxe's type: " + GearBoxType
               + "School's name: " + NameOfschool + "Tester's name: " + TeacherName + "The lesson's num: " + NumOfLessons + "/n";


        }

    }
}
