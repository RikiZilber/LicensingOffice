// Rivka Zilbershlag ID 305526824
// Odelye Movadat ID 17365982

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;


namespace UI
{
    class Program
    {
        static IBL bl = FactoryBL.getBL();

        public static object _stam { get; private set; }

        static void Main(string[] args)
        {
            try
            {
                #region Testers

                Tester avi = new Tester
                {
                    Id = "111111111",
                    LastName = "chohen",
                    FirstName = "avi",
                    BirthDate = DateTime.Parse("13.2.1970"),
                    GenderTester = Gender.Male,
                    CellPhone = "0511111111",
                    AddressTester = new Address() { Street = "hashomer", City = "Bnei Brak", HouseNumber = 16 },
                    ExperienceYears = 18,
                    MaxWeeklyTests = 12,
                    CarTypeTester = CarType.Private,
                    MaxDistanceTester = 10,
                    PersonalStatus = Status.married,
                    WorkerType = WorkerType.WorksContracter
                };

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        avi.AvailabilityTester[i, j] = true;
                    }
                }
                avi.AvailabilityTester[3, 2] = false;
                avi.AvailabilityTester[2, 3] = false;
                avi.AvailabilityTester[1, 1] = false;

                Tester efrat = new Tester("222222222")
                {
                    LastName = "levi",
                    FirstName = "efrat",
                    BirthDate = DateTime.Parse("13.2.1975"),
                    GenderTester = Gender.Female,
                    CellPhone = "0522222222",
                    AddressTester = new Address() { Street = "hashomer", City = "Bnei Brak", HouseNumber = 20 },
                    ExperienceYears = 12,
                    MaxWeeklyTests = 17,
                    CarTypeTester = CarType.HeavyTruck,
                    MaxDistanceTester = 15,
                    PersonalStatus = Status.devorsed,
                    //AvailabilityTester[5, 1] = true,
                    //AvailabilityTester = new bool[7, 5] { { true,false,false,true,false}, { false, false, false, true, false }, { true, false, true, true, false }, { true, false, false, true, true }, { false, false, false, false, false }, { true, false, false, true, false }, { true, false, true, true, false } }
                };

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        efrat.AvailabilityTester[i, j] = true;
                    }
                }

                efrat.AvailabilityTester[2, 4] = false;
                efrat.AvailabilityTester[1, 0] = false;
                efrat.AvailabilityTester[4, 1] = false;
                efrat.AvailabilityTester[2, 3] = false;

                Tester yossi = new Tester("333333333")
                {
                    LastName = "catzt",
                    FirstName = "yossi",
                    BirthDate = DateTime.Parse("13.2.1960"),
                    GenderTester = Gender.Male,
                    CellPhone = "0533333333",
                    AddressTester = new Address() { Street = "hapoel", City = "Ramat Gan", HouseNumber = 13 },
                    ExperienceYears = 15,
                    MaxWeeklyTests = 20,
                    CarTypeTester = CarType.TwoWheeled,
                    MaxDistanceTester = 25,
                    PersonalStatus = Status.widower,
                    WorkerType = WorkerType.CivilServant,
                    //AvailabilityTester[5, 1] = true,
                    //AvailabilityTester = new bool[7, 5] { { true,false,false,true,false}, { false, false, false, true, false }, { true, false, true, true, false }, { true, false, false, true, true }, { false, false, false, false, false }, { true, false, false, true, false }, { true, false, true, true, false } }
                };

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        yossi.AvailabilityTester[i, j] = true;
                    }
                }

                yossi.AvailabilityTester[3, 3] = false;

                Tester chaim = new Tester("134659786")
                {
                    LastName = "gill",
                    FirstName = "chaim",
                    BirthDate = DateTime.Parse("28.6.1975"),
                    GenderTester = Gender.Male,
                    CellPhone = "0511112211",
                    AddressTester = new Address() { Street = "hashomer", City = "Bnei Brak", HouseNumber = 30 },
                    ExperienceYears = 22,
                    MaxWeeklyTests = 8,
                    CarTypeTester = CarType.MediumTruck,
                    MaxDistanceTester = 12,
                    PersonalStatus = Status.married,
                    WorkerType = WorkerType.WorksContracter,
                    // AvailabilityTester[5, 1] = true,
                    //AvailabilityTester = new bool[7, 5] { { true,false,false,true,false}, { false, false, false, true, false }, { true, false, true, true, false }, { true, false, false, true, true }, { false, false, false, false, false }, { true, false, false, true, false }, { true, false, true, true, false } }
                };

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        chaim.AvailabilityTester[i, j] = true;
                    }
                }

                chaim.AvailabilityTester[5, 3] = false;
                chaim.AvailabilityTester[3, 4] = false;
                chaim.AvailabilityTester[2, 1] = false;

                bl.addTester(chaim);
                bl.addTester(avi);
                bl.addTester(efrat);
                bl.addTester(yossi);

                #endregion


                foreach (var tester in bl.getTestersList())
                {
                    Console.WriteLine("Tester:\n" + tester + "\n");
                }

                #region Trainees
                Trainee shalom = new Trainee("444444444")
                {
                    LastName = "zilber",
                    FirstName = "shalom",
                    BirthDate = DateTime.Parse("28.1.1995"),
                    GenderTrainee = Gender.Male,
                    CellPhone = "0544444444",
                    AddressTrainee = new Address() { Street = "hagiborim", City = "RamatGan", HouseNumber = 5 },
                    CarTypeTrainee = CarType.Private,
                    gearBoxtrainee = GearBox.Manual,
                    SchoolName = "lomdim",
                    TeacherName = "gabbi",
                    NumLessons = 35,
                    PersonalStatus = Status.married,
                    isUltraOrtodox = true
                };

                Trainee roni = new Trainee("555555555")
                {
                    LastName = "rachman",
                    FirstName = "roni",
                    BirthDate = DateTime.Parse("30.1.1998"),
                    GenderTrainee = Gender.Male,
                    CellPhone = "0555555555",
                    AddressTrainee = new Address() { Street = "hertzel", City = "TelAviv", HouseNumber = 20 },
                    CarTypeTrainee = CarType.HeavyTruck,
                    gearBoxtrainee = GearBox.Automatic,
                    SchoolName = "lamdan",
                    TeacherName = "kobi",
                    NumLessons = 40,
                    PersonalStatus = Status.single,
                    isUltraOrtodox = false
                };

                Trainee shira = new Trainee("666666666")
                {
                    LastName = "shushan",
                    FirstName = "shira",
                    BirthDate = DateTime.Parse("28.1.2000"),
                    GenderTrainee = Gender.Female,
                    CellPhone = "0566666666",
                    AddressTrainee = new Address() { Street = "BenYehuda", City = "Yahud", HouseNumber = 50 },
                    CarTypeTrainee = CarType.TwoWheeled,
                    gearBoxtrainee = GearBox.Automatic,
                    SchoolName = "melamed",
                    TeacherName = "dani",
                    NumLessons = 25,
                    PersonalStatus = Status.single,
                    isUltraOrtodox = false
                };

                bl.addTrainee(shalom);
                bl.addTrainee(roni);
                bl.addTrainee(shira);

                #endregion

                foreach (var trainee in bl.getTraineesList())
                {
                    Console.WriteLine("Trainee:\n" + trainee + "\n");
                }

                //shalom.CellPhone = "0569854";
                //bl.updateTrainee(shalom);  throw exp too few digits

                chaim.LastName = "fux";

                bl.updateTester(chaim);

                Console.WriteLine(chaim);

                bl.deleteTester(chaim);
              
                foreach (var tester in bl.getTestersList())
                {
                    Console.WriteLine("Tester:\n" + tester + "\n");
                }

                #region Tests

                Test t1 = bl.addTest(roni, DateTime.Parse("1 / 1 / 2019"));
                Test t2 = bl.addTest(shalom, DateTime.Parse("9 / 6 / 2020"));
                Test t3 = bl.addTest(shira, DateTime.Parse("26 / 9 / 2019"));

                foreach (var test in bl.getTestsList())
                {
                    Console.WriteLine("Test: \n" + test + "\n");
                }

                t1.Criteria[Parameters.distance_keeping] = true;
                t1.Criteria[Parameters.mirrors_looking] = false;
                t1.Criteria[Parameters.reverse_parking] = true;
                t1.Criteria[Parameters.signaling] = true;
                t1.Criteria[Parameters.traffic_signs] = false;
                t1.ScoreTest = true;
                t1.TesterNote = "very good!";

                bl.updateTest(t1);

                t2.Criteria[Parameters.distance_keeping] = true;
                t2.Criteria[Parameters.mirrors_looking] = false;
                t2.Criteria[Parameters.reverse_parking] = true;
                t2.Criteria[Parameters.signaling] = true;
                t2.Criteria[Parameters.traffic_signs] = false;
                t2.ScoreTest = true;
                t2.TesterNote = "very good!";

                bl.updateTest(t2);

                t3.Criteria[Parameters.distance_keeping] = false;
                t3.Criteria[Parameters.mirrors_looking] = false;
                t3.Criteria[Parameters.reverse_parking] = true;
                t3.Criteria[Parameters.signaling] = true;
                t3.Criteria[Parameters.traffic_signs] = false;
                t3.ScoreTest = false;
                t3.TesterNote = "too bad!";

                bl.updateTest(t2);

                foreach (var test in bl.getTestsList())
                {
                    Console.WriteLine("Test: \n" + test + "\n");
                }

                #endregion

                #region functions

                Console.WriteLine("percent for avi: " + bl.precentOfPassedTestTraineesForTester(avi));
                Console.WriteLine("max tester: " + bl.maxPrecentOfPassedTestTrainees().FirstName);
                Console.WriteLine("num tests :" + bl.getNumTestsGeneral(shalom));
                Console.WriteLine("num tests for cartype :" + bl.getNumTestsForCarType(shalom, shira.CarTypeTrainee));
                Console.WriteLine("is :" + bl.isAllowLicenseGeneral(shalom));     // addition
                Console.WriteLine("is aloow car type" + bl.isAllowLicenseForCarType(shalom, shalom.CarTypeTrainee));
                Console.WriteLine("1 for 2019? :" + bl.numOfPassedTestForYear(2019)+"\n");

                var v = bl.GetAllTestersGroupByExpertise();
                foreach (var item in v)
                {
                    Console.WriteLine(item.Key);
                    foreach (var w in item)
                        Console.WriteLine(w);
                }

                var s = bl.GetAllTraineessGroupByTestsNum();
                foreach (var item in s)
                {
                    Console.WriteLine(item.Key);
                    foreach (var w in item)
                        Console.WriteLine(w);
                }
                var p = bl.GetAllTraineesGroupByTester();
                foreach (var item in p)
                {
                    Console.WriteLine(item.Key);
                    foreach (var w in item)
                        Console.WriteLine(w);
                }
                #endregion
            }

            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }

            Console.ReadKey();
        }
    }
}
