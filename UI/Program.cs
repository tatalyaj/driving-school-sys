using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;
namespace Project01_5995_6670_dotNet5779
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("PL\n");
            try
            {
                BL.IBL bl = BLFactory.GetBL();
                #region CHECK THE BASIC FUNC
                //Tester func
                //Tester tr= bl.GetTester(056435598); // get
                // tr.PhoneNum = 0507212008;
                // bl.UpdateTester(tr); // update
                Tester tesr = new Tester();
                tesr.ID = "623639987";
                tesr.LastName = "Rickman";
                tesr.Name = "Alan";
                tesr.TesterGender = Gender.male;
                tesr.DateOfBirth = new DateTime(1958, 4, 12);
                tesr.TesterAdress = new Adress("Najara", 40, "jerusalem");
                tesr.YearsOfExperiance = 20;
                tesr.MaxTests = 8;
                tesr.TesterCarSpeciality = CarSpeciality.automatic;
                tesr.TesterWorkDays = new bool[5, 6] { { false,false,false,false,false,false},
{ false,false,true,true,true,false},
{ false,false,true,true,true,false},
{ false,false,true,true,true,false},
{ false,false,false,false,false,false}};
                tesr.MaxDistance = 3;
                tesr.Smoker = true;
                tesr.PhoneNum = 0583200838;
                tesr.Cash = true;
                tesr.DrinksAlcohol = true;
                //Tester t = new Tester();
                // bl.UpdateTester(tesr);// no such tester (yet)

                try
                {
                    bl.AddTester(tesr);//add her
                    Console.WriteLine(bl.GetTester(tesr.ID));
                    // IEnumerable<Tester> checkTESTER = bl.GetAllTesters();// get them all
                }
                catch (Exception ex) { Console.WriteLine(ex); }
                //try
                //{
                //    Trainee myTrainee = new Trainee
                //    {
                //        ID = 631147502,
                //        LastName = "Mcdermot",
                //        Name = "Dylan",
                //        TraineeGender = Gender.male,
                //        PhoneNum = "0545876312",//maybe will change to int later
                //        TraineeAdress = new Adress("Najara", 47, "jerusalem"),
                //        DateOfBirth = new DateTime(1987, 10, 16),
                //        CarLearnedRn = CarLearnedType.mechanic,
                //        GearBoxType = GearBox.wormReduction,
                //        NameOfschool = "Best Drivers inc",
                //        TeacherName = "Dwight",
                //        NumOfLessons = 21,
                //        HasTester = true,
                //        PayedOrNot = false

                //    };
                //    bl.AddTrainee(myTrainee);
               // }
               // catch (Exception ex) { Console.WriteLine(ex); }
                #endregion
            }
            catch (Exception str)
            {
                Console.WriteLine(str);
            }
            Console.ReadKey();
        }
    }
}