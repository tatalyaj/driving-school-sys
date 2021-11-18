using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class BL_imp : IBL
    {

        DAL.IDAL dal;

        public BL_imp()
        {
            dal = DAL.DALFactory.GetDal();
            // Initialization();

        }
        
        #region TEST FUNC
        public delegate bool conditionDelegate(Test x); // define the structure of the functions

        //returns all the  tests that are suitable o a condition
        public List<Test> GetSpecialTests(conditionDelegate cond)
        {

            return (from Test t in GetAllTests()
                    where cond(t)
                    select t).ToList();
        }
        //get a specific test
        public Test GetTest(int tst)
        {
            if (dal.GetTest(tst) == null)
                throw new Exception("Test Does not exist\n");
            return dal.GetTest(tst);
        }


        public void AddTest(Test test, DateTime wantedDate)
        {
           
            if (test == null)
                throw new Exception("No such test exists");
            //get to trianee via test
            Trainee traineeCheck = GetTrainee(test.StudentID);
            if (traineeCheck == null)
                throw new Exception("This trainee does not exist in our Data");//make sure this student exists
            if (traineeCheck.NumOfLessons < 20)
                throw new Exception("The Trainee can't take this test, becuase he/she had less then 20 lessons\n");
            if (traineeCheck.CarLearnedRn == CarLearnedType.automatic && traineeCheck.CarLearnedRn == CarLearnedType.mechanic)
                throw new Exception("The trainee can't learn on an automatic car at the same time as he/she learns mechanic");
            if ((traineeCheck.CarLearnedRn == CarLearnedType.automatic && test.CarTestedRn == CarTestedType.mechanic)
                || (traineeCheck.CarLearnedRn == CarLearnedType.mechanic && test.CarTestedRn == CarTestedType.automatic))
                throw new Exception("If the Trainne has learned on car type X, he/she must get tested on this X type car");
           // DateTime wantedDate;
            List<Test> myTests = GetSpecialTests(t => t.StudentID == traineeCheck.ID);
             DateTime datelimit = wantedDate.AddDays(-7);
            if (myTests.FindAll(t => t.TestDayNHour > datelimit).Count() > 0)
                throw new Exception("you have to wait 7 days between 2 tests");



            //the "hour" is always 0- i couldn't find the way to send to the func(from WPF)
            //a date of a day plus an hour, and it was already too late to change this function
            //so this is the reason i cant add up any test.....
            //and on this specific part i was not in charge(on the matrix that goes over the days and hours
            //(and in the end ignores the hour completly)
            //it's not an exuse , just an explenation... :)



            int day = (int)wantedDate.DayOfWeek;
            int hour = (int)wantedDate.Hour;

            Tester myTester = new Tester();
            foreach (Tester tester in TestersFree(wantedDate))//free on this day
            {
                //this func checks if the tester did his max amount of tests,* the - transforms it into sunday * the 6- transforms it into thursday * (int)wantedDate.DayOfWeek) transforms it into day of the week and then that day into int ex:sunday=1
                //we take sunday and thursday because we want to check in the week
                if (GetSpecialTests(t => t.TesterID == tester.ID && t.TestDayNHour >= wantedDate.AddDays(-((int)wantedDate.DayOfWeek)) && t.TestDayNHour <= wantedDate.AddDays(6 - ((int)wantedDate.DayOfWeek))).Count() < tester.MaxTests)
                {
                    //checks if the tester has past all the tests he/she may do per week, the speaciltests returns in a certain condition, and that was our condition
                    myTester = tester;
                    break;
                }
            }
            if (GetAllTesters().Where(t => t.ID == myTester.ID).Count() != 0)//checks if that list is empty-if its empty it means that we didnt find, and now we are going to look for a suitable tetser for us
            {
                string str = "You can choose these days to pass test:";
                string str1 = " same day at : ";
                string str2 = "same hour in another day";
                foreach (Tester tester in GetAllTesters())
                {
                    List<Test> tTests = GetSpecialTests(t => t.TesterID == tester.ID);
                    //to come to sunday on the same week                           
                    if (tTests.FindAll(t => t.TestDayNHour >= wantedDate.AddDays(-((int)wantedDate.DayOfWeek)) && t.TestDayNHour <= wantedDate.AddDays(6 - ((int)wantedDate.DayOfWeek))).Count() < tester.MaxTests)
                        
                        { // THE HOURS!!!!!!!!
                        for (int i = 0; i < 6; i++)//the i checks the hours in the specific say he/she wanted
                            if (tester.TesterWorkDays[day, i] == true)
                                if (tTests.FindAll(f => f.TestDayNHour.Hour == i && f.TestDayNHour.Day == wantedDate.Day && f.TestDayNHour.Month == wantedDate.Month && f.TestDayNHour.Year == wantedDate.Year).Count() == 0)
                                    //the whole date dd/mm/yyyy
                                    str1 = str1 + (i + 9).ToString() + ":00 ";//returns string of all the hours that he/she is available
                        //* +9 cause i starts at 0, and i start to work at 9:00 so 0+9 
                        //************************************************************
                        for (int i = 0; i < 5; i++) // THIS IS THE DAYSSSSSSS
                            if (tester.TesterWorkDays[i, hour] == true)
                                if (tTests.FindAll(t => (int)t.TestDayNHour.DayOfWeek == i &&   //tests in day number i
                                 t.TestDayNHour.Hour == hour &&                                // test in hour
                                 t.TestDayNHour >= wantedDate.AddDays(-((int)wantedDate.DayOfWeek)) && t.TestDayNHour <= wantedDate.AddDays(6 - ((int)wantedDate.DayOfWeek)) // test in the same week
                               
                                ).Count() == 0)
                                {
                                    str2 = str2 + wantedDate.AddDays(-((int)wantedDate.DayOfWeek) + i).ToString("MMMM dd, yyyy");
                                }
                        //*****************************************************
                    }
                    str = str + str1 + str2;
                    throw new Exception("No tester free this day in this hour" + str); //if he didnt find in all the if's
                }

            }

            //makes sure the testers car speciality is te same as the trainees learnes type
            Tester testerCheck = GetTester(test.TesterID);
            if ((traineeCheck.CarLearnedRn == CarLearnedType.automatic) && (testerCheck.TesterCarSpeciality == CarSpeciality.mechanic) ||
                (traineeCheck.CarLearnedRn == CarLearnedType.mechanic) && (testerCheck.TesterCarSpeciality == CarSpeciality.automatic))
                throw new Exception("This test can't be scheduled beacuse the trianee's learned car type is different from the tester's car speciality type");

            if ((test.PastOrFailed == true && traineeCheck.CarLearnedRn == CarLearnedType.automatic) ||
                 (test.PastOrFailed == true && traineeCheck.CarLearnedRn == CarLearnedType.mechanic))
                //later will se if to change it to CARTESTEDRN or leave it like taht....
                throw new Exception("This trainee can't take a test on a car type which he/she past succesfully");

        }

        public List<Tester> TestersFree(DateTime d)
        {   //gives a list of all the free testers in the wanted date
            int day = (int)d.DayOfWeek;
            int hour = (int)d.Hour ;
           // int hour = (int)d.Hour-9;
            IEnumerable<Tester> testerFree = GetAllTesters().Where(t => t.TesterWorkDays[day, hour] == true);

           List<Tester> result = new List<Tester>();
            foreach (Tester tester in testerFree)
            {                                         //come to sunday of this week                         
                                                      //  if (getAllTests().FindAll(t => t.TesterID == tester.ID && t.dateTest >= d.AddDays(-((int)d.DayOfWeek)) && t.dateTest <= d.AddDays(6 - ((int)d.DayOfWeek))).Count() < tester.MaxTests)
                if (GetAllTests().Where(t => t.TesterID == tester.ID && t.TestDayNHour == d).Count() == 0)
                    result.Add(tester);
            }
            return result;
        }


        public void UpdateTest(Test test)
        {
            if (CheckSuccessfulCriteria() == false)
                test.PastOrFailed = false;
            else
                test.PastOrFailed = true;
            //updates if the trainee pasted or failed
            if (test.NoteOfTester == null)
                throw new Exception("Can't update the test as long as the tester didn't update his/her notes");
           

        }
        public void RemoveTest(int id)
        {
            if (GetTest(id)==null)//CHANGED SOMETHING HERE!11111111
                throw new Exception("The Test does not exist  \n");
            dal.RemoveTest(id);
        }


        public IEnumerable<Test> GetAllTests(Func<Test, bool> predicate = null)
        {
            return dal.GetAllTests(predicate);
            //throw new NotImplementedException();
        }
        #endregion

        #region TESTER FUNC
        public Tester GetTester(int id)
        {
            if (dal.GetTester(id) == null)
                throw new Exception("Tester not found\n");
            return dal.GetTester(id);

        }

        public void AddTester(Tester tester)
        {
            if (DateTime.Now.Year - tester.DateOfBirth.Year < 40)
                throw new Exception("The tester requires to be 40 years old\n ");
            if (DateTime.Now.Year - tester.DateOfBirth.Year > 90)
                throw new Exception("This Tester Has Past the Max age");
            if (dal.GetTester(tester.ID) == null)
            {
                dal.AddTester(tester);
                Console.WriteLine("Tester added to the list");
            }
            else
                throw new Exception("This tester already exists in our data base");
        }

        public void RemoveTester(int id)
        {
            //removes him when 
            //Tester tester = GetTester(id);
            //if (tester.TesterTrainee != null)
            //    throw new Exception("Please find another tester for each trainees that this tester tests\n");
            dal.RemoveTester(id);
        }

        public void UpdateTester(Tester tester)
        {   
            if (GetTester(tester.ID)==null)
                throw new Exception ("This tester doe's not exist");
            dal.UpdateTester(tester);
        }



        public IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicate = null)
        {
            return dal.GetAllTesters(predicate);

        }


        #endregion

        #region TRAINEE FUNC

        public Trainee GetTrainee(int id)
        {
            if (dal.GetTrainee(id) == null)
                throw new Exception("Trainee not found\n");
            return dal.GetTrainee(id);

        }
        public void AddTrainee(Trainee trainee)
        {
            int Age = (DateTime.Now.Year) - (trainee.DateOfBirth.Year);
            if (Age < (int)18)
                throw new Exception("The trainee must be 18 years old at least ,in order to pass the test\n");
                if(dal.GetTrainee(trainee.ID)==null)
                {
                      dal.AddTrainee(trainee);
                      Console.WriteLine("Trainee was added suuccesfuly");
                }
            else
                throw new Exception("This trainee already exists in our data base");
        }

        public void RemoveTrainee(int id)
        {
            if (GetTrainee(id).HasTester == true)
                throw new Exception("The Trainer is in an active test procedure \n");
            dal.RemoveTrainee(id);

        }

        public void UpdateTrainee(Trainee trainee)
        {
            dal.UpdateTrainee(trainee);
        }

        public IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicate = null)
        {
            return dal.GetAllTrainees(predicate);

        }
        #endregion

        #region HELPING FUNCS
        //funcs we do to help us
        public bool CheckSuccessfulCriteria(bool[] SuccsesfulCriteria)
        {//how does e know that the 1st array r properties?????
            int count_fails = 0;
            for (int i = 0; i < SuccsesfulCriteria.Length; i++)
            {
                if (SuccsesfulCriteria[i] == false)
                    count_fails++;
            }
            if (count_fails > 2)
                return false;
            else return true;
        }

        //thats connected to the func above*********
        public bool CheckSuccessfulCriteria()
        {
            throw new NotImplementedException();
        }
        //**************
        /// <summary>
        /// Get all Testers that don't smoke and don't drink alcool 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tester> TesterNoSmokeNoAlcool()
        {
            return GetAllTesters(t => t.Smoker == false && t.DrinksAlcohol == false);
        }
        //ANNONYMOUSE TYPE-USAGE OF NEW
        public void TypesOfCarsInSchool ()
        {
            var the_types = new
            {
                car1 = "CADILLAC",
                car2 = "BWW",
                car3 = "TOYOTA",
                car4 = "FORD",
                car5 = "NISAN",
                car6 = "HINDA"
            };
        }
        public void QuotaLessons(Trainee trainee)////check if the trainee do the minimum amount of lessons according to the type of license
        {
            switch (trainee.CarLearnedRn)
            {
                case CarLearnedType.automatic:
                    if (trainee.NumOfLessons < 28)
                        throw new Exception("You must learn at least 28 lessons before you take the exam");
                    break;

                case CarLearnedType.mechanic:
                    if (trainee.NumOfLessons < 32)
                        throw new Exception("You must learn at least 32 lessons before you take the exam");
                    break;

                default:
                    break;
            }
        }
        //added now
        public IEnumerable<Trainee> TraineeNoTesterFound()
        {
            IEnumerable<Trainee> NoTester = from trainee in dal.GetAllTrainees()
                                            where trainee.HasTester == false
                                            select trainee;
            return NoTester;
        }


        #endregion



        #region PAYMENT FUNC
        //PAYMENT METHOD- THE TRAINEE CAN PAY AT ALL GIVE TIME,ONCE HAVE A TESTER
        public IEnumerable<Tester> AcceptCash()
        {// Get all testers that accept to be paid in cash 
            return GetAllTesters(t => t.Cash == true);
        }
        public IEnumerable<CreditCardPayment> GetAllCreditCardPayment(Func<CreditCardPayment, bool> predicate = null)
        { //this is a method in oerder to create an ienumerable of credit cards
            return dal.GetAllCreditCardPayment(predicate);

        }
        public IEnumerable <CreditCardPayment> RequiresCreditCard (IEnumerable<Tester> AcceptCash, CreditCardPayment credit )
        { //if there are testers that dont accept cash,they will be payed by credit card
            //will call a FUNC that cfreates a new credit card info
            if (AcceptCash == null)
              CreditInfo();//if list empty,create a new credit card

         return GetAllCreditCardPayment(c => c.OwnerCreditCardID == credit.OwnerCreditCardID);

        }
        //the func that creates the credit info
        public CreditCardPayment CreditInfo ()
        {
            CreditCardPayment c1 = new CreditCardPayment();
            return c1;
        }
        //if the trainee doesnt pay, thers an exepyion that he MUST PAY
        public void TraineePayedOrNOt(Trainee trainee)
        {
          if ((trainee.PayedOrNot == false) && (trainee.HasTester==true))
                throw new Exception("The Trainee must pay!!!!");
        }
        //if the trainee has payed remove him from our list
        public void RemovePayingTrainee(Trainee trainee)
        {
          if (trainee.PayedOrNot == true)
                RemoveTrainee(trainee.ID);
        }
        //added now
        public IEnumerable<Trainee> PayedOrNot()
        {
            //NoPayed
            IEnumerable<Trainee> Payed = from trainee in dal.GetAllTrainees()
                                               //where trainee.PayedOrNot == false
                                           where trainee.PayedOrNot == true
                                           select trainee;
            // return NoPayed;
            return Payed;
        }
        //added now
        public IEnumerable<Tester> MatchTester(Trainee trainee)

        {
            if (trainee == null)
                throw new Exception("The trainee does not exist");
            bool flag = true;
            int i = 0;
            foreach (Tester item in dal.GetAllTesters())
            {
                // if the tester is in the same city and have specific car speciality 
                if (trainee.City != item.City)
                    flag = false;
                if ((trainee.CarLearnedRn == CarLearnedType.automatic && item.TesterCarSpeciality != CarSpeciality.automatic)
                    || (trainee.CarLearnedRn == CarLearnedType.mechanic && item.TesterCarSpeciality != CarSpeciality.mechanic))
                    flag = false;

                // if the tester correspond
                if (flag == true)
                {
                    yield return item;
                    i++;

                }
            }

        }

        #endregion


        #region OTHER FUNC
        //the func they have asked

        public int CheckNumOfTests(Trainee trainee )
        {
            //goes through the list, where finds the id, puts it in a list,then does count for the list itslef to get a number
            IEnumerable <Test> ListOfTestsTaken = GetAllTests(t => t.StudentID == trainee.ID);//passes all over the tests, where the trainee id is the same it counts!!
            return ListOfTestsTaken.Count();
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //WHAT WEE DID
        //recieves two adresse and the randomal range is the distance that is given
        public float Distance(Adress a, Adress b)
        {
            Random r = new Random();
            return r.Next(0, 50);
        }

        //x-distance of tester's adress, to the starting of the test's adress, and then uses the distance func and checks if the distance<=x
        public List<Tester> TesterInXkm(Adress address, int x)
        {
            return (from Tester t in GetAllTesters()
                    where Distance(t.TesterAdress, address) <= x
                    select t).ToList();
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


        public bool EntitledToALicense(Trainee trainee)
        {   //default is false-doesn't receive a license
            bool TraineeRecieveLicense=false;
            Test testCheck = GetTest( Convert.ToInt32((trainee.ID)));
            if (testCheck.PastOrFailed == true)
                TraineeRecieveLicense = true;
            else
                TraineeRecieveLicense = false;
            return TraineeRecieveLicense;

        }

        public List<Test> PlannedTest(DateTime d)
        {
            List<Test> result = new List<Test>();
            foreach (Test test in GetAllTests())
            {
                if (GetAllTests().Where(t => t.TestDayNHour.Hour == d.Hour && t.TestDayNHour.Day == d.Day && t.TestDayNHour.Month == d.Month && t.TestDayNHour.Year == d.Year).Count() == 0)
                    result.Add(test);
            }
            return result;

        }
    

    #endregion

        
        #region GROUPING

    //return GROUPING of tester's car speciality by the years of experience
    public IEnumerable<IGrouping<CarSpeciality, Tester>> GroupingTesterSpeciality(bool sort = false)
        {
            IEnumerable<IGrouping<CarSpeciality, Tester>> groupOfSpeciality=null;
            if (sort == true)
            {
                groupOfSpeciality = from item in GetAllTesters()
                                    group item by item.TesterCarSpeciality;
            }
            else
            { 
                groupOfSpeciality = from item in GetAllTesters()
                                    orderby item.YearsOfExperiance
                                    group item by item.TesterCarSpeciality;
            }
         return groupOfSpeciality;
        }

        public IEnumerable<IGrouping<string ,Trainee>> GroupingTraineeSchoolName(bool sort = false)
        {
            IEnumerable<IGrouping<string, Trainee>> groupOfSchoolName = null;
            if (sort == true)
            {
                groupOfSchoolName = from item in GetAllTrainees()
                                    orderby item.Name
                                    group item by item.NameOfschool;
                                     
            }
            else
            {
                groupOfSchoolName = from item in GetAllTrainees()
                                    group item by item.NameOfschool;
            }
            return groupOfSchoolName;
        }

        public IEnumerable<IGrouping<string, Trainee>> GroupingTraineeByTeacher(bool sort = false)
        {
            IEnumerable<IGrouping<string, Trainee>> groupingTraineeByTeacher = null;
            if (sort == true)
            {
                groupingTraineeByTeacher = from item in GetAllTrainees()
                                           orderby item.Name
                                           group item by item.TeacherName;
            }
            else
            {
                groupingTraineeByTeacher =  from Trainee t in GetAllTrainees()
                group t by t.TeacherName;
            }
            return groupingTraineeByTeacher;

        }


        public IEnumerable<IGrouping<int, Trainee>> GroupingNumOfTestsDone(bool sort = false)
        {
            IEnumerable<IGrouping<int, Trainee >> groupingNumOfTestsDone = null;
            if(sort==true)
            {

                groupingNumOfTestsDone = from item in GetAllTrainees()
                                         orderby item.ID
                                         let NumOfTestsDone = CheckNumOfTests(item)
                                         group item by NumOfTestsDone;

            }
            else
            {
                groupingNumOfTestsDone = from item in GetAllTrainees()
                                         let NumOfTestsDone = CheckNumOfTests(item)
                                         group item by NumOfTestsDone;

            }
            return groupingNumOfTestsDone;
        }
        
        #endregion

    }
}
