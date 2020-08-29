using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class MyDal:IDal
    {
        private static MyDal instance = null;

        public static MyDal getMyDal()
        {
            if (instance == null)
                instance = new MyDal();
            return instance;
        }

        private MyDal()
        {
            //DataSource.testersList = new List<Tester>();
            //DataSource.traineesList = new List<Trainee>();
            //DataSource.testsList = new List<Test>();
        }

        /// <summary>
        /// help function-return the tester with the recieved id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Tester getTester(string _id)
        {
            int index = DataSource.testersList.FindIndex(t => t.Id == _id);
            if (index == -1)
                throw new Exception("DAL: Tester with the same id not found...");
            return DataSource.testersList.FirstOrDefault(t => t.Id == _id);
        }

        /// <summary>
        /// help function-return the treinee with the recieved id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Trainee getTrainee(string _id)
        {
            int index = DataSource.traineesList.FindIndex(t => t.Id == _id);
            if (index == -1)
                throw new Exception("DAL: Trainee with the same id not found...");
            return DataSource.traineesList.FirstOrDefault(t => t.Id == _id);
        }

        /// <summary>
        /// help function-return the test with the recieved code
        /// </summary>
        /// <param name="_code"></param>
        /// <returns></returns>
        public Test getTest(long _code)
        {
            int index = DataSource.testsList.FindIndex(t => t.TestCode == _code);
            if (index == -1)
                throw new Exception("DAL: Test with the same code not found...");
            return DataSource.testsList.FirstOrDefault(t => t.TestCode == _code);

        }

        /// <summary>
        /// add a new tester to the list
        /// </summary>
        /// <param name="tester"></param>
        public void addTester(Tester tester)
        {
            Tester tester1 = DataSource.testersList.FirstOrDefault(t => t.Id == tester.Id); ;
            if (tester1 != null)
                throw new Exception("DAL: Tester with the same id already exists...");
            DataSource.testersList.Add(tester);
        }

        /// <summary>
        /// delete a tester from the list
        /// </summary>
        /// <param name="tester"></param>
        /// <returns>true if seccessfully removed</returns>
        public bool deleteTester(Tester tester)
        {
            Tester t = getTester(tester.Id);
            //if (t == null)
            //    throw new Exception("DAL: Tester with the same id not found...");

            if (DataSource.testsList.Exists(ts => ts.TesterId == tester.Id))
                throw new Exception("DAL: Tests has been determinared for this tester !!!");

            return DataSource.testersList.Remove(t);
        }

        /// <summary>
        /// update a tester in the list- if exist
        /// </summary>
        /// <param name="tester"></param>
        public void updateTester(Tester tester)
        {
            int index = DataSource.testersList.FindIndex(t => t.Id == tester.Id);
            if (index == -1)
                throw new Exception("DAL: Tester with the same id not found...");

            DataSource.testersList[index] = tester;
        }

        /// <summary>
        /// add a new trainee to the list
        /// </summary>
        /// <param name="trainee"></param>
        public void addTrainee(Trainee trainee)
        {
            Trainee trainee1 = DataSource.traineesList.FirstOrDefault(t => t.Id == trainee.Id);
            if (trainee1 != null)
                throw new Exception("DAL: Trainee with the same id already exists...");
            DataSource.traineesList.Add(trainee);
        }

        /// <summary>
        /// delete a trainee from the list
        /// </summary>
        /// <param name="trainee"></param>
        /// <returns>true if seccessfully removed</returns>
        public bool deleteTrainee(Trainee trainee)
        {
            Trainee t = getTrainee(trainee.Id);
            //if (t == null)
            //    throw new Exception("Trainee with the same id not found...");

            if (DataSource.testsList.Exists(ts => ts.TraineeId == trainee.Id))
                throw new Exception("DAL: Test has been determinared for this trainee !!!");

            return DataSource.traineesList.Remove(t);
        }

        /// <summary>
        /// update a trainee in the list- if exist
        /// </summary>
        /// <param name="trainee"></param>
        public void updateTrainee(Trainee trainee)
        {
            int index = DataSource.traineesList.FindIndex(t => t.Id == trainee.Id);
            if (index == -1)
                throw new Exception("DAL: Trainee with the same id not found...");

            DataSource.traineesList[index] = trainee;
        }

        /// <summary>
        /// add a new test to the list
        /// </summary>
        /// <param name="test"></param>
        public void addTest(Test test)
        {
            test.TestCode = (++BE.Configuration.testCode);
            Test t = DataSource.testsList.FirstOrDefault(t1 => t1.TestCode == test.TestCode);
            if (t != null)
                throw new Exception("DAL: Test with the same code already exists...");
            if (!DataSource.testersList.Exists(ts => ts.Id == test.TesterId))
                throw new Exception("DAL: Tester with the same id not found...");
            if (!DataSource.traineesList.Exists(ts => ts.Id == test.TraineeId))
                throw new Exception("DAL: Trainee with the same id not found...");
            DataSource.testsList.Add(test);
        }

        /// <summary>
        /// update a test in the list- if exist
        /// </summary>
        /// <param name="test"></param>
        public void updateTest(Test test)
        {
            int index = DataSource.testsList.FindIndex(t => t.TestCode == test.TestCode);
            if (index == -1)
                throw new Exception("DAL: Test with the same code not found...");
            if(DataSource.testsList[index].TesterId!=test.TesterId)
                throw new Exception("DAL: Tester id not match to the current test");
            if (DataSource.testsList[index].TraineeId != test.TraineeId)
                throw new Exception("DAL: trainee id not match to the current test");
            DataSource.testsList[index] = test;
        }

        /// <summary>
        /// generic function for getting testers list for current condition
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns>collection of testers orderd descending by last name</returns>
        public IEnumerable<Tester> getTestersList(Func<Tester,bool> predicat=null)
        {
            var v = from item in DataSource.testersList
                    select new Tester(item);

            if (predicat == null)
                return v.AsEnumerable().OrderByDescending(s => s.LastName);

            return v.Where(predicat).OrderByDescending(s => s.LastName);
        }

        /// <summary>
        /// generic function for getting trainees list for current condition
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns>collection of trainees orderd descending by last name</returns>
        public IEnumerable<Trainee> getTraineesList(Func<Trainee, bool> predicat = null)
        {
            var v = from item in DataSource.traineesList
                    select new Trainee(item);

            if (predicat == null)
                return v.AsEnumerable().OrderByDescending(s => s.LastName);

            return v.Where(predicat).OrderByDescending(s => s.LastName);
        }

        /// <summary>
        /// generic function for getting tests list for current condition
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns>collection of tests orderd descending by date</returns>
        public IEnumerable<Test> getAllTests(Func<Test, bool> predicat = null)
        {
            var v = from item in DataSource.testsList
                    select new Test(item);

            if (predicat == null)
                return v.AsEnumerable().OrderByDescending(s => s.TestDateTime);

            return v.Where(predicat).OrderByDescending(s => s.TestDateTime);
        }


    }
}
