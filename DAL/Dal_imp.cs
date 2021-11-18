using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    static class Help
    {

        public static IEnumerable<T> DeepCopy<T>(this IEnumerable<T> t)
        {
            T[] array = new T[t.Count()];
            t.ToList().CopyTo(array);
            return array.ToList();
        }
    }
    public  class Dal_imp : IDAL
    {
        #region TEST FUNCTIONS 

        public Test GetTest(int tst)
        {
            if (DataSource.TestsList == null)
                return null;
            return DataSource.TestsList.FirstOrDefault(t => t.NumOfTest == tst);
        }
        public void AddTest(Test test)
        {
            Test testCheck = GetTest(test.NumOfTest);
            if (testCheck != null)
                throw new Exception("TESt With that number already exists in the system");
            DS.DataSource.TestsList.Add(test);
            int numOfTests = DS.DataSource.TestsList.Count;
            test.NumOfTest = numOfTests++;

          //  InitialNumOfTest++;

        }
        public void UpdateTest(Test test)
        {
            //we earased
            int index = DS.DataSource.TestsList.FindIndex(t => t.NumOfTest == test.NumOfTest);

            DS.DataSource.TestsList[index] = test;
        }
       public void RemoveTest(int id)
        {
            Test testCheck = GetTest(id);
            if (testCheck == null)
                throw new Exception("This Test  doesn't exists in the system\n");
            DataSource.TestsList.Remove(testCheck);
        }
        /// <summary>
        /// gets all the tests according to a specific condition,if there isnt predicate=null
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Test> GetAllTests(Func<Test, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.TestsList.AsEnumerable().DeepCopy();
            return DS.DataSource.TestsList.Where(predicate).DeepCopy();
        }
        #endregion

        #region TESTER FUNCTIONS

        public Tester GetTester(int id)
        {
            if (DataSource.TestersList == null)
                return null;
            return DataSource.TestersList.FirstOrDefault(t => t.ID == id);
        }

        public void AddTester(Tester tester)
        {
          
            Tester testerCheck = GetTester(tester.ID);
            if(testerCheck!=null)
                throw new Exception("Tester with this ID already exists in the system\n");
            DS.DataSource.TestersList.Add(tester);
           //CHANGED THINGS HERE!!!!!
        }

        public void RemoveTester(int id)
        {
            Tester testerCheck = GetTester(id);
            if (testerCheck == null)
                throw new Exception("Tester with this ID doesn't exists in the system\n");
            DataSource.TestersList.Remove(testerCheck);
        }

        public void UpdateTester(Tester tester)
        {
            int index = DS.DataSource.TestersList.FindIndex(t => t.ID == tester.ID);
           
                DS.DataSource.TestersList[index] = tester;
        }
        /// <summary>
        /// gets all the testers according to a specific condition,if there isnt predicate=null
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.TestersList.AsEnumerable().DeepCopy();
            return DS.DataSource.TestersList.Where(predicate).DeepCopy();
        }
        #endregion

        #region TRAINEE FUNCTIONS

        public Trainee GetTrainee(int id)
        {
            if (DataSource.TraineesList == null)
                return null;
            return DataSource.TraineesList.FirstOrDefault(t => t.ID == id);
        }
        public void AddTrainee(Trainee trainee)
        {
            Trainee traineeCheck = GetTrainee(trainee.ID);
            if(traineeCheck!=null)
                throw new Exception("Trainee with this ID already exists in the system");
            DS.DataSource.TraineesList.Add(trainee);
           
            // int index = DS.DataSource.TestersList.FindIndex(t => t.ID == trainee.ID); // findindex return -1 if the tester is not exists 
           // if (index == -1)
              //  DS.DataSource.TraineesList.Add(trainee);
           // else
              //  throw new Exception("Trainee with this Id is already exists\n");
            
          //  DataSource.TraineesList.Add(trainee);
        }

        public void RemoveTrainee(int id)
        {
            Trainee traineecheck = GetTrainee(id);
            if (traineecheck == null)
                throw new Exception("Trainee with this ID doesn't exists in the system\n");
            DataSource.TraineesList.Remove(traineecheck);
        }

        public void UpdateTrainee(Trainee trainee)
        {
            int index = DS.DataSource.TraineesList.FindIndex(t => t.ID == trainee.ID);
            DS.DataSource.TraineesList[index] = trainee;
            
            //int index = DS.DataSource.TraineesList.FindIndex(t => t.ID == trainee.ID);
            //if (index == -1)
            //    throw new Exception("This trainee does not exists\n");
            //else
            //    DS.DataSource.TraineesList[index] = trainee;
        }

        /// <summary>
        /// gets all the trainees according to a specific condition,if there isnt predicate=null
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.TraineesList.AsEnumerable().DeepCopy();
            return DS.DataSource.TraineesList.Where(predicate).DeepCopy();
        }
        #endregion
        public IEnumerable<CreditCardPayment> GetAllCreditCardPayment(Func<CreditCardPayment, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.CreditCards.AsEnumerable().DeepCopy();
            return DS.DataSource.CreditCards.Where(predicate).DeepCopy();
        }

    }
}
