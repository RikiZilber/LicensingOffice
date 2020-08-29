using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        #region Tester Functions
        /// <summary>
        /// add tester to data base
        /// </summary>
        /// <param name="tester"></param>
        void addTester(Tester tester);

        /// <summary>
        /// delete tester from DB
        /// </summary>
        /// <param name="tester"></param>
        /// <returns></returns>
        bool deleteTester(Tester tester);

        /// <summary>
        /// update tester details
        /// </summary>
        /// <param name="newTester"></param>
        void updateTester(Tester tester);

        IEnumerable<Tester> getTestersList(Func<Tester,bool> predicat=null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_adress"></param>
        /// <param name="_x">max distance</param>
        /// <returns>all the testers wich their distance from current address smaller than _x</returns>
        IEnumerable<Tester> getCloseTesters(Address _adress, double _x);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>collection of available testers for newTest</returns>
        IEnumerable<Tester> getAvailableTesters(DateTime _date);

        /// <summary>
        /// addition
        /// </summary>
        /// <param name="_tester"></param>
        /// <returns>percent of passed test trainees for tester</returns>
        int precentOfPassedTestTraineesForTester(Tester _tester);

        /// <summary>
        /// addition
        /// </summary>
        /// <returns>the tester with the maximum percents of passed test trainees</returns>
        Tester maxPrecentOfPassedTestTrainees();                     

        #endregion

        #region Trainee Functions

        /// <summary>
        /// add trainee to data base
        /// </summary>
        /// <param name="trainee"></param>
        void addTrainee(Trainee trainee);

        /// <summary>
        /// delete trainee from DB
        /// </summary>
        /// <param name="trainee"></param>
        /// <returns></returns>
        bool deleteTrainee(Trainee trainee);

        /// <summary>
        /// update trainee details
        /// </summary>
        /// <param name="newTrainee"></param>
        void updateTrainee(Trainee trainee);

        /// <summary>
        /// 
        /// </summary>
        /// <returns> collection of all the trainees</returns>
        IEnumerable<Trainee> getTraineesList(Func<Trainee, bool> predicat = null);

        /// <summary>
        /// addition
        /// </summary>
        /// <param name="_trainee"></param>
        /// <returns>number of tests for trainee</returns>
        int getNumTestsGeneral(Trainee _trainee);

        /// <summary>
        /// addition
        /// </summary>
        /// <param name="_trainee"></param>
        /// <param name="type"></param>
        /// <returns>number of tests for specific car type for trainee</returns>
        int getNumTestsForCarType(Trainee _trainee, CarType type);

        /// <summary>
        /// addition
        /// </summary>
        /// <returns>avarage of trainees who passed a test</returns>
        double percentAverageOfPassedTestTrainees();

        /// <summary>
        /// addition
        /// </summary>
        /// <param name="_trainee"></param>
        /// <returns>true if trainee has a license</returns>
        bool isAllowLicenseGeneral(Trainee _trainee);

        /// <summary>
        /// addition
        /// </summary>
        /// <param name="_trainee"></param>
        /// <param name="type"></param>
        /// <returns>true if trainee has license for specific car type</returns>
        bool isAllowLicenseForCarType(Trainee _trainee, CarType type);

        /// <summary>
        /// addition
        /// </summary>
        /// <param name="year"></param>
        /// <returns>number of trainees who passed a test in specific year</returns>
        int numOfPassedTestForYear(int year) ;
        //int percentOfPassedFirstTestTrainees();    

        #endregion

        #region Test Functions

        /// <summary>
        /// add test to data base
        /// </summary>
        /// <param name="trainee"></param>
        /// <param name="wantedTestDate"></param>
        Test addTest(Trainee trainee, DateTime wantedTestDate);

        /// <summary>
        /// 
        /// </summary>
        /// <returns> collection of all the tests</returns>
        IEnumerable<Test> getTestsList(Func<Test, bool> predicat = null);

        /// <summary>
        /// update the test fields- score, note and criteria
        /// </summary>
        /// <param name="newTest"></param>
        /// 
        void updateTest(Test test);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dayOrMonth"></param>
        /// <returns>collection of all the tests for a specific day or month</returns>
        IEnumerable<Test> getPlannedTests(DateTime date, DateChoice dayOrMonth);

        #endregion

        #region Grouping Functions

        /// <summary>
        /// grouping function
        /// </summary>
        /// <param name="toSort"></param>
        /// <returns>all testers grouped by expertise</returns>
        IEnumerable<IGrouping<CarType, Tester>> GetAllTestersGroupByExpertise(bool toSort = false);

        //*************************************************************הוספתי עוד משתנה של מחרוזת של השם של הבי"ס

        /// <summary>
        /// grouping function
        /// </summary>
        /// <param name="toSort"></param>
        /// <returns>all trainees group by school name</returns>
        IEnumerable<IGrouping<string, Trainee>> GetAllTraineesGroupBySchoolName(bool toSort = false, string schoolName = "");

        /// <summary>
        /// grouping function
        /// </summary>
        /// <param name="toSort"></param>
        /// <returns>all trainees group by tester </returns>
        IEnumerable<IGrouping<string, Trainee>> GetAllTraineesGroupByTester(bool toSort = false);

        /// <summary>
        /// grouping function
        /// </summary>
        /// <param name="toSort"></param>
        /// <returns>all Trainees group by test number</returns>
        IEnumerable<IGrouping<int, Trainee>> GetAllTraineessGroupByTestsNum(bool toSort = false);

        #endregion
    }
}
