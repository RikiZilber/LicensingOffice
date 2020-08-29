using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDal
    {
        void addTester(Tester tester);
        bool deleteTester(Tester tester);
        void updateTester(Tester tester);
        Tester getTester(string id);


        void addTrainee(Trainee trainee);
        bool deleteTrainee(Trainee trainee);
        void updateTrainee(Trainee trainee);
        Trainee getTrainee(string id);


        void addTest(Test test);
        void updateTest(Test test);
        Test getTest(long code);

        //returns copy of the testers collection wich answer a specific predicate or null
        IEnumerable<Tester> getTestersList(Func<Tester, bool> predicat = null);
        //returns copy of the trainees collection wich answer a specific predicate or null
        IEnumerable<Trainee> getTraineesList(Func<Trainee, bool> predicat = null);
        //returns copy of the test collection wich answer a specific predicate or null
        IEnumerable<Test> getAllTests(Func<Test, bool> predicat = null);
                

    }
}

