using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
     public interface IBL 

     {    
        #region TEST
        void AddTest(Test test, DateTime wantedDate);
        void UpdateTest(Test test);
        void RemoveTest(int id);
        IEnumerable<Test> GetAllTests(Func<Test, bool> predicate = null);
        Test GetTest(int tst);
        #endregion
        #region TESTER
        void AddTester(Tester tester);
        void RemoveTester(int id);
        void UpdateTester(Tester tester);
        IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicate = null);
        Tester GetTester(int id);
        #endregion
        #region TRAINEE
        void AddTrainee(Trainee trainee);
        void RemoveTrainee(int id);
        void UpdateTrainee(Trainee trainee);
        IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicate = null);
        Trainee GetTrainee(int id);
        #endregion
        #region OTHER FUNC
        int CheckNumOfTests(Trainee trainee);
        bool EntitledToALicense(Trainee trainee);
        List <Test> PlannedTest(DateTime d);
        #endregion
        #region HELPING FUNCS
        bool CheckSuccessfulCriteria(bool[] SuccsesfulCriteria);
        IEnumerable<Tester> TesterNoSmokeNoAlcool();
        void QuotaLessons(Trainee trainee);
        IEnumerable<Trainee> TraineeNoTesterFound();
        #endregion
        #region PAYMENT FUNCS
        IEnumerable<Tester> AcceptCash();
        void TraineePayedOrNOt(Trainee trainee);
        void RemovePayingTrainee(Trainee trainee);
        IEnumerable<CreditCardPayment> RequiresCreditCard(IEnumerable<Tester> AcceptCash, CreditCardPayment credit);
        CreditCardPayment CreditInfo();
        IEnumerable<CreditCardPayment> GetAllCreditCardPayment(Func<CreditCardPayment, bool> predicate = null);
        IEnumerable<Tester> MatchTester(Trainee trainee);
         IEnumerable<Trainee> PayedOrNot();
       
        #endregion
        #region GROUPING
        IEnumerable<IGrouping<CarSpeciality, Tester>> GroupingTesterSpeciality(bool sort = false);
        IEnumerable<IGrouping<string, Trainee>> GroupingTraineeSchoolName(bool sort = false);
        IEnumerable<IGrouping<string, Trainee>> GroupingTraineeByTeacher(bool sort = false);
        IEnumerable<IGrouping<int, Trainee>> GroupingNumOfTestsDone(bool sort = false);
        #endregion

    }
}
