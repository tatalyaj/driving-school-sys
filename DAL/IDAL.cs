using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;


namespace DAL
{
   public interface IDAL
    {
        #region TEST
        void AddTest(Test test);
        void UpdateTest(Test test);
        void RemoveTest(int id);
        Test GetTest(int id);
        IEnumerable<Test> GetAllTests(Func<Test, bool> predicate = null);
        #endregion
        #region TESTER
        void AddTester(Tester tester);
        Tester GetTester(int id);
        void RemoveTester(int id);
        void UpdateTester(Tester tester);
        IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicate = null);
        #endregion
        #region TERAINEE
        void AddTrainee(Trainee trainee);
        Trainee GetTrainee(int id);
        void RemoveTrainee(int id);
        void UpdateTrainee(Trainee trainee);
        IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicate = null);
        #endregion    
        //payment method-list of credit cards
        IEnumerable<CreditCardPayment> GetAllCreditCardPayment(Func<CreditCardPayment, bool> predicate = null);
    }
}
