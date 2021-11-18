using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using BE;
using System.Reflection;
using System.ComponentModel;
using System.Xml.Serialization;
/*The files are located in PLWPF->bin->Debug*/

//sometimes whie adding a tester from here- the number wont give us the option to add the tester
//please just choose a different number(it usually works, sometimes for certain numbers it doesnt....

namespace DAL
{
    class Dal_XML_imp : IDAL
    {
        //Roots and Paths
        private XElement testRoot;
        private string testPath = @"TestXml.xml";//his path
        private XElement configRoot;
        private string configPath = @"ConfigXml.xml";//his path
        private XElement testerRoot;
        private string testerPath = @"TesterXml.xml";
        private XElement traineeRoot;
        private string traineePath = @"TraineeXml.xml";
        //private XElement creditRoot;
        private string creditPath = @"creditCardXml.xml";

        //constructor
        public Dal_XML_imp()//maybe later will change into private
        {
            #region OPEN/CREATE All FILES
            //CREATES THE FILES
            try
            {
                if (!File.Exists(testPath))//if path doesnt exist, create a new one
                {
                    testRoot = new XElement("tests");
                    testRoot.Save(testPath);
                }
                else
                {
                    LoadTest();//LOADING FOR TEST
                }

                if (!File.Exists(configPath))//if path doesnt exist, create a new one
                {
                    configRoot = new XElement("config");
                    configRoot.Save(configPath);
                }
                else
                {
                    LoadConfig();//LOADING FOR CONFIG
                }

                if (!File.Exists(testerPath))//if path doesnt exist, create a new one
                {
                    testerRoot = new XElement("testers");
                    testerRoot.Save(testerPath);
                }
                else
                {
                    LoadTester();//LOADING FOR TESTER
                }

               
                if (!File.Exists(creditPath))//LoadFor credit
                {
                    List<CreditCardPayment> creditList = new List<CreditCardPayment>();//empty list to create file
                    SaveToXML<List<CreditCardPayment>>(creditList, creditPath);
                }

                if (!File.Exists(traineePath))//if path doesnt exist, create a new one
                {
                    traineeRoot = new XElement("trainees");
                    traineeRoot.Save(traineePath);
                }
                else
                {
                    LoadTrainee();//LOADING FOR TRAINEE
                }

                #endregion
            }
            catch
            {
                throw new Exception("File upload problem");
            }

        }
        #region COnfigLoad
        //Loading Path for Config
        private void LoadConfig()
        {
            try
            {
                configRoot = XElement.Load(configPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        #endregion
        //Loading Path for testers
        #region TESTERLOAD
        private void LoadTester()
        {
            try
            {
                testerRoot = XElement.Load(testerPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        #endregion
        #region TestLoad
        //Loading Path for Test
        private void LoadTest()
        {
            try
            {
                testRoot = XElement.Load(testPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        #endregion

        
        #region Load For Trainee
        private void LoadTrainee()
        {
            try
            {
                traineeRoot = XElement.Load(traineePath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        #endregion
        //SERIALIZE AND DESERIALIZE
        public static void SaveToXML<T>(T source, string path)//save to xml file ,save object like element from program to file
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }
        
        public static T LoadFromXML<T>(string path)//load object from element, from  file to program 
        {
            try {
                FileStream file = new FileStream(path, FileMode.Open);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                T result = (T)xmlSerializer.Deserialize(file);
                file.Close();
                return result;
            }
            catch (Exception m)
            {
                throw new Exception (m.Message);
            }
        }
            

        #region TEST_LONG WAY
        //Linq to XML
        /// <summary>
        /// converts a test type to XML elemnt
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        XElement TestToXML(Test test)
        {
            if (test == null)
                return null;
            XElement NumOfTest = new XElement("NumOfTest", test.NumOfTest);
            XElement TesterID = new XElement("TesterID", test.TesterID);
            XElement StudentID = new XElement("StudentID", test.StudentID);
            XElement TestDayNHour = new XElement("TestDayNHour", test.TestDayNHour);
            XElement StartingAdress = new XElement("StartingAdress", test.StartingAdress);
            XElement DistanceMaintainance = new XElement("DistanceMaintainance", test.DistanceMaintainance);
            XElement ReverseParking = new XElement("ReverseParking", test.ReverseParking);
            XElement MirrorsObservation = new XElement("MirrorsObservation", test.MirrorsObservation);
            XElement Signal = new XElement("Signal", test.Signal);
            XElement StearingControl = new XElement("StearingControl", test.StearingControl);
            XElement TempSuccsesfulCriteria = new XElement("TempSuccsesfulCriteria", test.TempSuccsesfulCriteria);
            XElement CarTestedRn = new XElement("CarTestedRn", test.CarTestedRn);
            XElement PastOrFailed = new XElement("PastOrFailed", test.PastOrFailed);
            XElement NoteOfTester = new XElement("NoteOfTester", test.NoteOfTester);

            XElement testElement = new XElement("test", NumOfTest, TesterID, StudentID, TestDayNHour, StartingAdress, DistanceMaintainance, ReverseParking,
              MirrorsObservation, Signal, StearingControl, TempSuccsesfulCriteria, CarTestedRn, PastOrFailed, NoteOfTester);
            return testElement;
        }
        /// <summary>
        /// convrets a XML elemnt to child
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Test XMLToTest(XElement t)
        {
            if (t == null)
                return null;
            Test test = new Test()
            {
                NumOfTest = int.Parse(t.Element("NumOfTest").Value),
                TesterID = int.Parse(t.Element("TesterID").Value),
                StudentID = int.Parse(t.Element("StudentID").Value),
                TestDayNHour = DateTime.Parse(t.Element("TestDayNHour").Value),
                StartingAdress = new BE.Adress(t.Element("Adress").Element("Street").Value,
                int.Parse(t.Element("Adress").Element("NumOfBUilding").Value),
                t.Element("Adress").Element("City").Value),
                DistanceMaintainance = Convert.ToBoolean(t.Element("DistanceMaintainance").Value),
                ReverseParking = Convert.ToBoolean(t.Element("ReverseParking").Value),
                MirrorsObservation = Convert.ToBoolean(t.Element("MirrorsObservation").Value),
                Signal = Convert.ToBoolean(t.Element("Signal").Value),
                StearingControl = Convert.ToBoolean(t.Element("StearingControl").Value),
                TempSuccsesfulCriteria = t.Element("TempSuccsesfulCriteria").Value,
                CarTestedRn = (CarTestedType)Enum.Parse(typeof(CarTestedType), t.Element(" CarTestedRn").Value),
                PastOrFailed = Convert.ToBoolean(t.Element("PastOrFailed").Value),
                NoteOfTester = t.Element("NoteOfTester").Value
            };

            return test;
        }
        /// <summary>
        /// returns a test from the list according to the numoftest,if it doesnt exist it return null
        /// </summary>
        /// <param name="NumOfTest"></param>
        /// <returns></returns>
        public Test GetTest(int id)//the id is the num of the test
        {//conect the num into the congig.....
            LoadTest();
            //should i do try and catch???''
            XElement tst = (from t in testRoot.Elements()
                where (int.Parse(t.Element("NumOfTest").Value)) == id
                            select t).FirstOrDefault();
            if (tst == null)
                return null;
            return XMLToTest(tst);
        }
        /// <summary>
        /// adds a test to the list, if the test already exists it throws an exeption
        /// </summary>
        /// <param name="test"></param>
        public void AddTest(Test test)
        {
            LoadTest();
            if (test == null)
                throw new Exception("Test is already in the system");
            testRoot.Add(TestToXML(test));//adding,transforing it into xml
            testRoot.Save(testPath);
        }
        /// <summary>
        /// removes a test from te list...
        /// </summary>
        /// <param name="id"></param>
        public void RemoveTest(int id)//the id is the num of the test
        {
            LoadTest();
            XElement testElement;
            try
            {//goes through on the whole testroot and slects the first test that answares our requirments
                Test check = GetTest(id);
                if (check == null)
                    throw new Exception("this test doesnt exist in the system");
                testElement = (from t in testRoot.Elements()
                               where Convert.ToInt32(t.Element("id").Value) == id
                               select t).FirstOrDefault();
                testElement.Remove();
                testRoot.Save(testPath);
                return;
            }
            catch(Exception m)
            {
                throw new Exception(m.Message);
            }
        }
        /// <summary>
        /// updates a test from the list,if the test doesnt exist an exeption will be thrown
        /// </summary>
        /// <param name="test"></param>
        public void UpdateTest(Test test)
        {
            LoadTest();
            if (GetTest(test.NumOfTest) == null || test == null)
                throw new Exception("test doesnt exist in the system");
            //passes on all the testroot, finds the test we want and returns it
            XElement testElement = (from t in testRoot.Elements()
                                   where Convert.ToInt32(t.Element("id").Value) == test.NumOfTest
                                   select t).FirstOrDefault();
            testElement.Element("NumOfTest").Value = test.NumOfTest.ToString();
            testElement.Element("TesterID").Value = test.TesterID.ToString();
            testElement.Element("StudentID").Value = test.StudentID.ToString();
            testElement.Element("TestDayNHour").Value = test.TestDayNHour.ToString();
            testElement.Element("StartingAdress").Value = test.StartingAdress.ToString();
            testElement.Element("DistanceMaintainance").Value = test.DistanceMaintainance.ToString();
            testElement.Element("ReverseParking").Value = test.ReverseParking.ToString();
            testElement.Element("MirrorObseravation").Value = test.MirrorsObservation.ToString();
            testElement.Element("Signal").Value = test.Signal.ToString();
            testElement.Element("StreaingControl").Value = test.StearingControl.ToString();
            testElement.Element("TempSuccsesfulCriteria").Value = test.TempSuccsesfulCriteria.ToString();
            testElement.Element("CarTestedRn").Value = test.CarTestedRn.ToString();
            testElement.Element("PasedOrFailed").Value = test.PastOrFailed.ToString();
            testElement.Element("NoteOfTester").Value = test.NoteOfTester.ToString();

            testRoot.Save(testPath);
        }

        public IEnumerable<Test> GetAllTests(Func<Test, bool> predicate = null)
        {
            LoadTest();
            List<Test> tests = new List<Test>();
            try
            {
                //passes on every item in testRoot converts each elemnt from XML to test
                var item = from temp in testRoot.Elements()
                           select XMLToTest(temp);
                tests = item.ToList();//into a list
            }
            catch
            {
                tests = null;
            }
            //if predicate exists,return all the tests that are relavant to that predicate-else return as predicate
            if (predicate != null)//check why
                return tests.Where(predicate).AsEnumerable();
            return tests.AsEnumerable();

        }
        #endregion
        #region TESTER LONG WAY
       /// <summary>
       /// converts TEster to elenmt type
       /// </summary>
       /// <param name="tester"></param>
       /// <returns></returns>
        XElement TesterToXML(Tester tester)
        {
            if (tester == null)
                return null;
            XElement ID = new XElement("ID", tester.ID);
            XElement LastName = new XElement("LastName", tester.LastName);
            XElement Name = new XElement("Name", tester.Name);
            XElement TesterGender = new XElement("TesterGender", tester.TesterGender);
            XElement DateOfBirth = new XElement("DateOfBirth", tester.DateOfBirth);
            XElement PhoneNum = new XElement("PhoneNumber", tester.PhoneNum);
            XElement TesterAdress = new XElement("TesterAdress", tester.TesterAdress);
            XElement YearsOfExperiance = new XElement("YearsOfExperiance", tester.YearsOfExperiance);
            XElement MaxTests = new XElement("MaxTests", tester.MaxDistance);
            XElement TesterCarSpeciality = new XElement("TesterCarSpeciality", tester.TesterCarSpeciality);
            XElement City = new XElement("City", tester.City);
            XElement MaxDistance = new XElement("MaxDistance", tester.MaxDistance);
            XElement Smoker = new XElement("Smoker", tester.Smoker);
            XElement DrinksAlcohol = new XElement("DrinksAlcohol", tester.DrinksAlcohol);
            // XElement TesterWorkDays = new XElement("TesterWorkDays", tester.TesterWorkDays);
            XElement TempTesterWorkDays = new XElement("TempTesterWorkDays", tester.TempTesterWorkDays);
            XElement Cash = new XElement("Cash", tester.Cash);

            XElement testerElement = new XElement("tester", ID, LastName, Name, TesterGender, DateOfBirth,
             PhoneNum, TesterAdress, YearsOfExperiance, MaxTests, TesterCarSpeciality, City, MaxDistance, TempTesterWorkDays,
            Smoker, DrinksAlcohol/*,TesterWorkDays)*/, Cash);
            return testerElement;
        }
        /// <summary>
        /// converts XML into Tester
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Tester XMLToTester(XElement t)
        {
            if (t == null)
                return null;
            Tester tester = new Tester()
            {
                ID = int.Parse(t.Element("ID").Value),
                LastName=t.Element("LastName").Value,
                Name=t.Element("Name").Value,
                TesterGender=(Gender)Enum.Parse(typeof(Gender),t.Element("TesterGender").Value),
                DateOfBirth=DateTime.Parse(t.Element("DateOfBirth").Value),
                PhoneNum=int.Parse(t.Element("PhoneNum").Value),
                TesterAdress = new BE.Adress(t.Element("Adress").Element("Street").Value,
                int.Parse(t.Element("Adress").Element("NumOfBUilding").Value),
                t.Element("Adress").Element("City").Value),
                YearsOfExperiance=int.Parse(t.Element("YearsOfExperiance").Value),
                MaxTests=int.Parse(t.Element("MaxTests").Value),
                TesterCarSpeciality= (CarSpeciality)Enum.Parse(typeof(CarSpeciality), t.Element("TesterCarSpeciality").Value),
                City= t.Element("City").Value,
                MaxDistance= Convert.ToDouble(t.Element("MaxDistance").Value),
                Smoker=Convert.ToBoolean(t.Element("Smoker").Value),
                DrinksAlcohol = Convert.ToBoolean(t.Element("DrinksAlcohol").Value),
                Cash = Convert.ToBoolean(t.Element("Cash").Value),
                //TesterWorkDays = t.Element("TesterWorkDays").Value
                TempTesterWorkDays = t.Element("TempTesterWorkDays").Value
            };
            return tester;
        }

        public Tester GetTester(int id)
        {
            
           LoadTester();
           //should i do try and catch???''
            XElement tsr = (from t in testerRoot.Elements()
                            where (int.Parse(t.Element("ID").Value)) == id
                            select t).FirstOrDefault();
            if (tsr == null)
              return null;
            return XMLToTester(tsr);
            
        }
        /// <summary>
        /// adds a tester to the list,if this tester already exits-will throw exception
        /// </summary>
        /// <param name="tester"></param>
        public void AddTester(Tester tester)
        {
            LoadTester();
            if (tester == null)
                throw new Exception("Tester is already in the system");
            testerRoot.Add(TesterToXML(tester));//adding,transforing it into xml
            testerRoot.Save(testerPath);
        }
        /// <summary>
        /// Removes a tseter from the list
        /// </summary>
        /// <param name="id"></param>
        public void RemoveTester(int id)
        {
            LoadTester();
            XElement testerElement;
            try
            {//goes through on the whole testerroot and slects the first test that answares our requirments
                Tester check = GetTester(id);
                if (check == null)
                    throw new Exception("this tester doesnt exist in the system");
                testerElement = (from t in testerRoot.Elements()
                               where Convert.ToInt32(t.Element("id").Value) == id
                               select t).FirstOrDefault();
                testerElement.Remove();
                testerRoot.Save(testerPath);
                return;
            }
            catch (Exception m)
            {
                throw new Exception(m.Message);
            }
        }
        /// <summary>
        /// updates a tester from the list
        /// </summary>
        /// <param name="tester"></param>
        public void UpdateTester(Tester tester)
        {
            LoadTester();
            if (GetTester(tester.ID) == null || tester == null)
                throw new Exception("Tester doesnt exist in the system");
            //passes on all the testerroot ,finds the tester we want and returns it
            XElement testerElement = (from t in testerRoot.Elements()
                                      where Convert.ToInt32(t.Element("ID").Value) == tester.ID
                                      select t).FirstOrDefault();
            testerElement.Element("ID").Value = tester.ID.ToString();
            testerElement.Element("LastName").Value = tester.LastName.ToString();
            testerElement.Element("Name").Value = tester.Name.ToString();
            testerElement.Element("TesterGender").Value = tester.TesterGender.ToString();
            testerElement.Element("DateOfBirth").Value = tester.DateOfBirth.ToString();
            testerElement.Element(" PhoneNum").Value = tester.PhoneNum.ToString();
            testerElement.Element("TesterAdress").Value = tester.TesterAdress.ToString();
            testerElement.Element("YearsOfExperiance").Value = tester.YearsOfExperiance.ToString();
            testerElement.Element("MaxTests").Value = tester.MaxTests.ToString();
            testerElement.Element("TesterCarSpeciality").Value = tester.TesterCarSpeciality.ToString();
            testerElement.Element(" City").Value = tester.City.ToString();
            testerElement.Element("MaxDistance").Value = tester.MaxDistance.ToString();
            testerElement.Element("Smoker").Value = tester.Smoker.ToString();
            testerElement.Element("DrinksAlcohol").Value = tester.DrinksAlcohol.ToString();
            testerElement.Element("Cash").Value = tester.Cash.ToString();
            testerElement.Element("TempTesterWorkDays").Value = tester.TempTesterWorkDays.ToString();

            testerRoot.Save(testerPath);
        }
        public IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicate = null)
        {
            //try
            //{
            //    List<Tester> t = LoadFromXML<List<Tester>>(testerPath);
            //    if (predicate == null)
            //        return t.AsEnumerable();
            //    return t.Where(predicate);
            //}
            //catch
            //{
            //    return null;
            //}

            LoadTester();
            List<Tester> testers = new List<Tester>();
            try
            {
                //passes on every item in testerRoot converts each elemnt from XML to test
                var item = from temp in testerRoot.Elements()
                           select XMLToTester(temp);
                testers = item.ToList();//into a list
            }
            catch
            {
                testers = null;
            }
            //if predicate exists,return all the tests that are relavant to that predicate-else return as predicate
            if (predicate != null)//check why
                return testers.Where(predicate).AsEnumerable();
            return testers.AsEnumerable();
        }
        #endregion
        #region Trainee FUNCS Longt WAY
        /// <summary>
        /// converts trainee into xml
        /// </summary>
        /// <param name="trainee"></param>
        /// <returns></returns>
        XElement TraineeToXML(Trainee trainee)
        {
            if (trainee == null)
                return null;
            XElement ID = new XElement("ID", trainee.ID);
           // XElement TesterID = new XElement("TesterID", trainee.TesterID);
            //XElement LastName = new XElement("LastName", trainee.LastName);
            XElement Name = new XElement("Name", trainee.Name);
            XElement TraineeGender = new XElement("TraineeGender", trainee.TraineeGender);
            XElement PhoneNum = new XElement("PhoneNumber", trainee.PhoneNum);
            XElement TraineeAdress = new XElement("TraineeAdress", trainee.TraineeAdress);
            XElement DateOfBirth = new XElement("DateOfBirth", trainee.DateOfBirth);
            XElement CarLearnedRn = new XElement("CarLearnedRn", trainee.CarLearnedRn);
            XElement GearBoxType = new XElement("GearBoxType", trainee.GearBoxType);
            XElement NameOfschool = new XElement("NameOfschool", trainee.NameOfschool);
            XElement TeacherName = new XElement("TeacherName", trainee.TeacherName);
            XElement NumOfLessons = new XElement("NumOfLessons", trainee.NumOfLessons);
            XElement HasTester = new XElement("HasTester", trainee.HasTester);
            XElement PayedOrNot = new XElement("PayedOrNot", trainee.PayedOrNot);
            XElement City = new XElement("City",trainee.City);
            
            XElement traineeElement = new XElement("trainee", ID, /*TesterID,*/ /*LastName, */Name, TraineeGender, DateOfBirth,
             PhoneNum, TraineeAdress, CarLearnedRn, GearBoxType, GearBoxType, City, NameOfschool,
            TeacherName, NumOfLessons,HasTester, PayedOrNot, City);
            return traineeElement;
        }
        /// <summary>
        /// converts xml into trainee
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Trainee XMLToTrainee(XElement t)
        {
            if (t == null)
                return null;
            Trainee trainee = new Trainee()
            {
                ID=int.Parse(t.Element("ID").Value),
               // TesterID = int.Parse(t.Element("TesterID").Value),
                //LastName=t.Element("LastName ").Value,
                Name = t.Element("Name").Value,
                TraineeGender = (Gender)Enum.Parse(typeof(Gender), t.Element("TraineeGender").Value),
                PhoneNum = t.Element("PhoneNum ").Value,
                TraineeAdress = new BE.Adress(t.Element("Adress").Element("Street").Value,
                int.Parse(t.Element("Adress").Element("NumOfBUilding").Value),
                t.Element("Adress").Element("City").Value),
                DateOfBirth = DateTime.Parse(t.Element("DateOfBirth").Value),
                CarLearnedRn = (CarLearnedType)Enum.Parse(typeof(CarLearnedType), t.Element("CarLearnedRn ").Value),
                GearBoxType = (GearBox)Enum.Parse(typeof(GearBox), t.Element("GearBoxType").Value),
                NameOfschool = t.Element("LastName ").Value,
                TeacherName = t.Element("LastName ").Value,
                NumOfLessons = int.Parse(t.Element("NumOfLessons ").Value),
                HasTester = Convert.ToBoolean(t.Element("HasTester ").Value),
                PayedOrNot = Convert.ToBoolean(t.Element("PayedOrNot ").Value),
                City = t.Element("City").Value
            };
            return trainee;
        }
        public Trainee GetTrainee(int id)
        {
            LoadTrainee();
          
            XElement tra = (from t in traineeRoot.Elements()
                            where (int.Parse(t.Element("ID").Value)) == id
                            select t).FirstOrDefault();
            if (tra == null)
                return null;
            return XMLToTrainee(tra);
        }
        /// <summary>
        /// adds a trainee to the list,if this trainee already exits-will throw exception
        /// </summary>
        /// <param name="trainee"></param>
        public void AddTrainee(Trainee trainee)
        {
            LoadTrainee();
            if (trainee == null)
                throw new Exception("Trainee is already in the system");
            traineeRoot.Add(TraineeToXML(trainee));//adding,transforing it into xml
            traineeRoot.Save(traineePath);
        }
        /// <summary>
        /// Removes a trainee from the list
        /// </summary>
        /// <param name="id"></param>
        public void RemoveTrainee(int id)
        {
            LoadTrainee();
            XElement traineeElement;
            try
            {//goes through on the whole traineeroot and slects the first test that answares our requirments
                Trainee check = GetTrainee(id);
                if (check == null)
                    throw new Exception("this trainee doesnt exist in the system");
                traineeElement = (from t in traineeRoot.Elements()
                                 where Convert.ToInt32(t.Element("ID").Value) == id
                                 select t).FirstOrDefault();
                traineeElement.Remove();
                traineeRoot.Save(traineePath);
                return;
            }
            catch (Exception m)
            {
                throw new Exception(m.Message);
            }
        }
        /// <summary>
        /// updates a trainee from the list
        /// </summary>
        /// <param name="tester"></param>
        public void UpdateTrainee(Trainee trainee)
        {
            LoadTrainee();
            if (GetTrainee(trainee.ID) == null || trainee == null)
                throw new Exception("trainee doesnt exists in the system");
            //passes on all the traineeRoot ,finds the trainee that we want and returns it
            XElement traineeElement = (from t in traineeRoot.Elements()
                                       where Convert.ToInt32(t.Element("id").Value) == trainee.ID
                                       select t).FirstOrDefault();
            traineeElement.Element("ID").Value = trainee.ID.ToString();
           // traineeElement.Element(" TesterID").Value = trainee.TesterID.ToString();
            //traineeElement.Element("LastName").Value = trainee.LastName.ToString();
            traineeElement.Element("Name").Value = trainee.Name.ToString();
            traineeElement.Element("TraineeGender").Value = trainee.TraineeGender.ToString();
            traineeElement.Element("PhoneNum").Value = trainee.PhoneNum.ToString();
            traineeElement.Element(" TraineeAdress").Value = trainee.TraineeAdress.ToString();
            traineeElement.Element("DateOfBirth").Value = trainee.DateOfBirth.ToString();
            traineeElement.Element("CarLearnedRn").Value = trainee.CarLearnedRn.ToString();
            traineeElement.Element("GearBoxType").Value = trainee.GearBoxType.ToString();
            traineeElement.Element("NameOfschool").Value = trainee.NameOfschool.ToString();
            traineeElement.Element("TeacherName").Value = trainee.TeacherName.ToString();
            traineeElement.Element(" NumOfLessons").Value = trainee.NumOfLessons.ToString();
            traineeElement.Element("HasTester").Value = trainee.HasTester.ToString();
            traineeElement.Element(" PayedOrNot ").Value = trainee.PayedOrNot.ToString();
            traineeElement.Element("City").Value = trainee.City.ToString();

            traineeRoot.Save(traineePath);
        }
        public IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicate = null)
        {
            LoadTrainee();
            List<Trainee> trainees = new List<Trainee>();
            try
            {
                //passes on every item in traineeRoot converts each elemnt from XML to test
                var item = from temp in traineeRoot.Elements()
                           select XMLToTrainee(temp);
                trainees = item.ToList();//into a list
            }
            catch
            {
                trainees = null;
            }
            //if predicate exists,return all the trainees that are relavant to that predicate-else return as predicate
            if (predicate != null)//check why
                return trainees.Where(predicate).AsEnumerable();
            return trainees.AsEnumerable();
        }
        #endregion
        #region CREDit Card Short WAY
        public IEnumerable<CreditCardPayment> GetAllCreditCardPayment(Func<CreditCardPayment, bool> predicate = null)
        {
            try
            {
                List<CreditCardPayment> credits = LoadFromXML<List<CreditCardPayment>>(creditPath);
                //if the predicate is null(doesnt exist so return as AsEnumerable
                if (predicate == null)
                    return credits.AsEnumerable();
                return credits.Where(predicate);
            }
            catch
            {
                return null;
            }
        }
        #endregion 

    }

}      


   



