using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Configuration //Global variables
    {
        public static int MinNumOfLessons = 20;
        public static double MaxTesterAge=90;
        public static double MinStudentAge=18;
        //public static double GapTimeBtwnTests;//the time between each test taken
        public static int InitialNumOfTest = 0; // The initial runner number of Test
       // public int CashPayment = 100;
    }


    public struct Adress
    {
       public string Street;
       public int NumOfBUilding;
       public string City;
       

        public Adress(string v1, int v2, string v3) : this()
        {
            Street= v1;
            NumOfBUilding = v2;
            City = v3;
        }
    }
    public struct CreditCardPayment
    {
        public int CreditCardNumber;
        public int OwnerCreditCardID;
        public DateTime CardExp;
        public int ThreeBackNum;
    }
}
