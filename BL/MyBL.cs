using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.IO;
using System.Net;
using System.Xml;
using System.Threading;
using System.Globalization;

namespace BL
{
    public class MyBL : IBL
    {        
        private static MyBL instance = null;
        IDal d;

        public static MyBL getMyBL()
        {
            if (instance == null)
                instance = new MyBL();
            return instance;
        }

        private MyBL()
        {
            d = FactoryDal.getDal();
            if(d.getTestersList().ToList().Count == 0) InitialFields();

        }

        // initials basic details for WPF
        void InitialFields()
        {

            try
            {
                Tester avi = new Tester
                {
                    Id = "111111111",
                    LastName = "chohen",
                    FirstName = "avi",
                    BirthDate = DateTime.ParseExact("13.2.1970", "dd.mm.yyyy", CultureInfo.InvariantCulture),
                    GenderTester = Gender.Male,
                    CellPhone = "0511111111",
                    AddressTester = new Address() { Street = "hashomer", City = "Bnei Brak", HouseNumber = 16 },
                    ExperienceYears = 18,
                    MaxWeeklyTests = 12,
                    CarTypeTester = CarType.Private,
                    MaxDistanceTester = 10,
                    PersonalStatus = Status.married,
                    WorkerType = WorkerType.WorksContracter,
                    ImageSource = @"images\user_03.jpg"

                };

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        avi.AvailabilityTester[i, j] = true;
                    }
                }

                Tester efrat = new Tester("222222222")
                {
                    LastName = "levi",
                    FirstName = "efrat",
                    BirthDate = DateTime.ParseExact("13.2.1975", "dd.mm.yyyy", CultureInfo.InvariantCulture),
                    GenderTester = Gender.Female,
                    CellPhone = "0522222222",
                    AddressTester = new Address() { Street = "hashomer", City = "Bnei Brak", HouseNumber = 20 },
                    ExperienceYears = 12,
                    MaxWeeklyTests = 17,
                    CarTypeTester = CarType.HeavyTruck,
                    MaxDistanceTester = 15,
                    PersonalStatus = Status.devorsed,
                    WorkerType = WorkerType.WorksContracter,
                    ImageSource = @"images\user_02.jpg"
                };

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        efrat.AvailabilityTester[i, j] = true;
                    }
                }

                efrat.AvailabilityTester[2, 3] = false;

                Tester yossi = new Tester("333333333")
                {
                    LastName = "catzt",
                    FirstName = "yossi",
                    BirthDate = DateTime.ParseExact("13.2.1960", "dd.mm.yyyy", CultureInfo.InvariantCulture),
                    GenderTester = Gender.Male,
                    CellPhone = "0533333333",
                    AddressTester = new Address() { Street = "hapoel", City = "Ramat Gan", HouseNumber = 13 },
                    ExperienceYears = 15,
                    MaxWeeklyTests = 20,
                    CarTypeTester = CarType.TwoWheeled,
                    MaxDistanceTester = 25,
                    PersonalStatus = Status.widower,
                    WorkerType = WorkerType.CivilServant,
                    ImageSource = @"images\user_1.jpg"
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
                    BirthDate = DateTime.ParseExact("28.6.1975", "dd.mm.yyyy", CultureInfo.InvariantCulture),
                    GenderTester = Gender.Male,
                    CellPhone = "0511112211",
                    AddressTester = new Address() { Street = "hashomer", City = "Bnei Brak", HouseNumber = 30 },
                    ExperienceYears = 18,
                    MaxWeeklyTests = 8,
                    CarTypeTester = CarType.MediumTruck,
                    MaxDistanceTester = 12,
                    PersonalStatus = Status.married,
                    WorkerType = WorkerType.WorksContracter,
                    ImageSource = @"images\user_4.jpg"
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

                addTester(chaim);
                addTester(avi);
                addTester(efrat);
                addTester(yossi);

                Trainee shalom = new Trainee("444444444")
                {
                    LastName = "zilber",
                    FirstName = "shalom",
                    BirthDate = DateTime.ParseExact("28.1.1995", "dd.mm.yyyy", CultureInfo.InvariantCulture),
                    GenderTrainee = Gender.Male,
                    CellPhone = "0544444444",
                    AddressTrainee = new Address() { Street = "hagiborim", City = "RamatGan", HouseNumber = 5 },
                    CarTypeTrainee = CarType.Private,
                    gearBoxtrainee = GearBox.Manual,
                    IsHandicapped = true,
                    SchoolName = "lomdim",
                    TeacherName = "gabbi",
                    NumLessons = 35,
                    PersonalStatus = Status.married,
                    isUltraOrtodox = true,
                    ImageSource = @"images\user_6.jpg"
                };

                Trainee roni = new Trainee("555555555")
                {
                    LastName = "rachman",
                    FirstName = "roni",
                    BirthDate = DateTime.ParseExact("30.1.1998", "dd.mm.yyyy", CultureInfo.InvariantCulture),
                    GenderTrainee = Gender.Male,
                    CellPhone = "0555555555",
                    AddressTrainee = new Address() { Street = "hertzel", City = "TelAviv", HouseNumber = 20 },
                    CarTypeTrainee = CarType.HeavyTruck,
                    gearBoxtrainee = GearBox.Automatic,
                    SchoolName = "lomdim",
                    TeacherName = "kobi",
                    NumLessons = 40,
                    PersonalStatus = Status.single,
                    ImageSource = @"images\user_8.jpg",
                    isUltraOrtodox = false
                };

                Trainee shira = new Trainee("666666666")
                {
                    LastName = "shushan",
                    FirstName = "shira",
                    BirthDate = DateTime.ParseExact("28.1.2000", "dd.mm.yyyy", CultureInfo.InvariantCulture),
                    GenderTrainee = Gender.Female,
                    CellPhone = "0566666666",
                    AddressTrainee = new Address() { Street = "BenYehuda", City = "Yahud", HouseNumber = 50 },
                    CarTypeTrainee = CarType.TwoWheeled,
                    gearBoxtrainee = GearBox.Automatic,
                    SchoolName = "melamed",
                    TeacherName = "dani",
                    NumLessons = 25,
                    PersonalStatus = Status.single,
                    isUltraOrtodox = false,
                    ImageSource = @"images\user_03.jpg"

                };

                addTrainee(shalom);
                addTrainee(roni);
                addTrainee(shira);

                DateTime wantedDate1 = new DateTime(2019, 3, 13, 13, 0, 0);
                DateTime wantedDate2 = new DateTime(2020, 1, 19, 10, 0, 0);

                Test t1 = addTest(roni, wantedDate1); 
                Test t2 = addTest(shalom, wantedDate2); 
            }

            catch (Exception exp)
            {
                throw exp;   
            }
        }

        #region Help Functions

        // help function
        private void isValidTester(Tester tester)
        {
            if (((tester.AgeTester) < Configuration.MIN_TESTER_AGE) ||
                       ((tester.AgeTester) > Configuration.MAX_TESTER_AGE))
                throw new Exception("BL: inValid tester " + tester.FirstName + " age! tester age must be between " +
                                Configuration.MIN_TESTER_AGE + " and " + Configuration.MAX_TESTER_AGE);
        }

        /// <summary>
        /// score is legeal if al least half of the criteria equal to the test score
        /// </summary>
        /// <param name="test"></param>
        /// <returns>true if the test score is legeal according to criteria's score</returns>
        private bool isValidScore(Test test)
        {
            int counter = 0;
            bool? val = test.ScoreTest;
            for (int i = 0; i < Configuration.NUM_OF_CRITERIA; i++)
            {
                if (test.Criteria[(Parameters)i] == val)
                    counter++;
            }
            if (counter > Configuration.NUM_OF_CRITERIA / 2)
                return true;
            return false;
        }

        // help function
        private void isValidTrainee(Trainee trainee)
        {
            if (((trainee.AgeTrainee) < Configuration.MIN_TRAINEE_AGE))
                throw new Exception("BL: inValid trainee age! tester age must be bigger than " + Configuration.MIN_TRAINEE_AGE);
        }

        // help function
        private void isValidTest(Test test)
        {
            IEnumerable<Test> traineesTests = getMatchTests(t => t.TraineeId == test.TraineeId);
            if (traineesTests.Count() != 0)
            {
                Test lastTest = traineesTests.First();           // last newTest contain the last newTest of current trainee
                if (Math.Abs((test.TestDateTime - lastTest.TestDateTime).TotalDays) < 7)
                    throw new Exception("BL: can not set a newTest within 7 days from last newTest! ");

                if (lastTest.ScoreTest.HasValue && lastTest.ScoreTest == true)
                {
                    if (lastTest.carTypeTest == test.carTypeTest)
                        throw new Exception("BL: this trainee has already passed the newTest for " +
                                        lastTest.carTypeTest.ToString() + "at " + lastTest.TestDateTime.ToString());
                }
            }

            if (d.getTrainee(test.TraineeId).NumLessons < Configuration.MIN_CLASSES)
                throw new Exception("BL: can not set a newTest for trainee who didnt do at least "
                                + Configuration.MIN_CLASSES + "lessons! ");
        }

        /// <summary>
        /// help function
        /// </summary>
        /// <param name=""></param>
        /// <returns>numbers of tests were setted up for current week for a tester</returns>
        private int getNumOfTesterTestsForWeek(DateTime date, Tester tester)
        {
            IEnumerable<Test> testsForTester = d.getAllTests(t => t.TesterId == tester.Id);
            int counter = 0;
            foreach (var test in testsForTester)
            {
                if (isDatesInSameWeek(date, test.TestDateTime))
                    counter++;
            }
            return counter;
        }

        private IEnumerable<Tester> getCloseTesters(Address _adress)
        {
            //Thread t1 = new Thread(isCloseEnough);
            return from item in d.getTestersList()
                   where isCloseEnough(_adress, item.AddressTester, item.MaxDistanceTester)
                   select item;
        }

        /// <summary>
        /// help function
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns>true if two dates are in same week</returns>
        private bool isDatesInSameWeek(DateTime date1, DateTime date2)
        {
            var d1 = date1.AddDays(-1 * (int)date1.DayOfWeek);
            var d2 = date2.AddDays(-1 * (int)date2.DayOfWeek);
            return (d1 == d2);
        }

        /// <summary>
        /// help function
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="_x"></param>
        /// <returns>true if the distance between 2 addresses is less then current number</returns>
        private static bool isCloseEnough(Address first, Address second, double _x)
        {
            Random random = new Random();
            Thread.Sleep(3000);

            string origin = first.HouseNumber + " " + first.Street + " " +  "Street " + first.City;
            string destination = second.HouseNumber + " " + second.Street + " " + "Street " + second.City;
            string url = @"https://www.mapquestapi.com/directions/v2/route" +
            @"?key=" + System.Environment.GetEnvironmentVariable("MAP_KEY") +
            @"&from=" + origin +
            @"&to=" + destination +
            @"&outFormat=xml" +
            @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
            @"&enhancedNarrative=false&avoidTimedConditions=false";

            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);

            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                distInMiles = distInMiles * 1.609344;
                return distInMiles <= _x;
            }

            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            //we have an answer that an error occurred, one of the addresses is not found
            {
                return false;
            }

            else //busy network or other error...
            {
                double d = random.NextDouble() * (10 - 0.2) + 0.2;
                return (d <= _x);
            }
        }

        /// <summary>
        /// help function
        /// </summary>
        /// <param name="tester"></param>
        /// <param name="date"></param>
        /// <returns>true if the tester does not have a newTest at current date time</returns>
        private bool isSetTest(Tester tester, DateTime date)
        {
            var tests = getMatchTests(t => t.TesterId == tester.Id);

            foreach (var item in tests)
            {
                if (item.TestDateTime == date)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// the function calls to dal function
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns>collection to all the newTest for a current condition</returns>
        private IEnumerable<Test> getMatchTests(Func<Test, bool> predicat = null)
        {
            return d.getAllTests(predicat);
        }

        #endregion

        #region Tester Functions


        public void addTester(Tester tester)
        {
            isValidTester(tester);
            d.addTester(tester);
        }

        public bool deleteTester(Tester tester)
        {
            return d.deleteTester(tester);
        }

        public IEnumerable<Tester> getTestersList(Func<Tester, bool> predicat = null)
        {
            return d.getTestersList(predicat);
        }

        public void updateTester(Tester newTester)
        {
            isValidTester(newTester);

            Tester oldTester = d.getTester(newTester.Id);
            if (newTester.BirthDate != oldTester.BirthDate)
                throw new Exception("BL: You  can not change your Birth date!");

            // updating the tester's address
            if (!newTester.AddressTester.isSame(oldTester.AddressTester))
            {
                IEnumerable<Test> testsForTester = d.getAllTests(t => t.TesterId == oldTester.Id);
                if (testsForTester.Count() != 0)
                {
                    foreach (var test in testsForTester)
                    {
                        if (test.ScoreTest == null)     // the newTest has not happened yet
                        {
                            if (!isCloseEnough(newTester.AddressTester, test.ExitAdress, Configuration.MAX_DIST_FROM_TESTER))
                                throw new Exception("BL: Your new address is too far from your setted newTest's" +
                                                    " address! thus you can not change your address!");
                        }
                    }
                }
            }

            // updating the tester's Car type
            if (newTester.CarTypeTester != oldTester.CarTypeTester)
            {
                IEnumerable<Test> testsForTester = d.getAllTests(t => t.TesterId == newTester.Id);
                if (testsForTester.Count() != 0)
                {
                    foreach (var test in testsForTester)
                    {
                        if ((test.ScoreTest == null) && (test.carTypeTest == oldTester.CarTypeTester))    // the newTest has not happened yet
                            throw new Exception("BL: You already have a newTest for your old car type! you can not change your car type till it happens!");
                    }
                }
            }

            d.updateTester(newTester);
        }

        public IEnumerable<Tester> getCloseTesters(Address _adress, double _x)
        {
            return from item in d.getTestersList()
                   where isCloseEnough(_adress, item.AddressTester, _x)
                   select item;
        }      

        public IEnumerable<Tester> getAvailableTesters(DateTime date)
        {
            int day = (int)date.DayOfWeek;
            if (day > 4) throw new Exception();
            List<Tester> testers = d.getTestersList().ToList();
                var v = (from item in testers
                        where (item.AvailabilityTester[date.Hour - 9, day] == true) && (!isSetTest(item, date))
                        select item).ToList();

            return v;        
        }

        public int precentOfPassedTestTraineesForTester(Tester _tester)
        {
            IEnumerable<Test> testsOfTester = d.getAllTests(t => t.TesterId == _tester.Id);
            if (testsOfTester.Count() == 0) throw new Exception("BL: the list of tests for this tester is empty!");
            var v = from item in testsOfTester
                    where ((item.ScoreTest != null) && (item.ScoreTest == true))
                    select item;
            return (100 * v.Count() / testsOfTester.Count());
        }

        public Tester maxPrecentOfPassedTestTrainees()
        {
            IEnumerable<Tester> testersList = d.getTestersList();

            int max = 0;
            Tester maxTester = testersList.First();
            foreach (var tester in testersList)
            {
                if (d.getAllTests(t => t.TesterId == tester.Id).ToList().Count == 0) break;
                int current = precentOfPassedTestTraineesForTester(tester);
                if (current > max)
                {
                    max = current;
                    maxTester = tester;
                }
            }
            return maxTester;
        }

        #endregion

        #region Trainee Functions


        public void addTrainee(Trainee trainee)
        {
            isValidTrainee(trainee);
            d.addTrainee(trainee);
        }

        public bool deleteTrainee(Trainee trainee)
        {
            return d.deleteTrainee(trainee);
        }

        public IEnumerable<Trainee> getTraineesList(Func<Trainee, bool> predicat = null)
        {
            return d.getTraineesList(predicat);
        }

        public void updateTrainee(Trainee newTrainee)
        {
            isValidTrainee(newTrainee);
            Trainee oldTrainee = d.getTrainee(newTrainee.Id);
            if (newTrainee.BirthDate != oldTrainee.BirthDate)
                throw new Exception("BL: Trainee  can not change his Birth date!");

            // updating the traineee's address
            if (!newTrainee.AddressTrainee.isSame(oldTrainee.AddressTrainee))
            {
                IEnumerable<Test> testsForTrainee = d.getAllTests(t => t.TraineeId == newTrainee.Id);
                if (testsForTrainee.Count() != 0)
                {
                    foreach (var test in testsForTrainee)
                    {
                        if (test.ScoreTest == null)     // the newTest has not happened yet
                        {
                            Tester currentTester = d.getTester(test.TesterId);
                            if (!isCloseEnough(newTrainee.AddressTrainee, currentTester.AddressTester, Configuration.MAX_DIST_FROM_TESTER))
                                throw new Exception("BL: Your new address is too far from your setted newTest's" +
                                                    " tester's address! thus you can not change your address!");
                            test.ExitAdress = newTrainee.AddressTrainee;    // the test exit address setted up by trainees address
                        }
                    }
                }
            }

            // updating the traineee's Car type
            if (newTrainee.CarTypeTrainee != oldTrainee.CarTypeTrainee)
            {
                IEnumerable<Test> testsForTrainee = d.getAllTests(t => t.TraineeId == newTrainee.Id);
                if (testsForTrainee.Count() != 0)
                {
                    foreach (var test in testsForTrainee)
                    {
                        if ((test.ScoreTest == null) && (test.carTypeTest == oldTrainee.CarTypeTrainee))    // the newTest has not happened yet
                            throw new Exception("BL: You already have a newTest for your old car type! you can not change your car type till you pass it!");
                    }
                }
            }

            d.updateTrainee(newTrainee);
           
        }


        public int getNumTestsGeneral(Trainee _trainee)
        {
            return (d.getAllTests(t => t.TraineeId == _trainee.Id)).Count();
        }

        public int getNumTestsForCarType(Trainee _trainee, CarType type)
        {
            IEnumerable<Test> testForTrainee = d.getAllTests(t => t.TraineeId == _trainee.Id);
            var v = from test in testForTrainee
                    where test.carTypeTest == type
                    select test;
            return v.Count();
        }

        public double percentAverageOfPassedTestTrainees()
        {
            IEnumerable<int> v = (from tester in d.getTestersList()
                                  where (d.getAllTests(t => t.TesterId == tester.Id).ToList().Count != 0)
                                  select precentOfPassedTestTraineesForTester(tester)).ToList();

            return v.Average();
        }

        public bool isAllowLicenseGeneral(Trainee _trainee)
        {
            IEnumerable<Test> testForTrainee = d.getAllTests(t => t.TraineeId == _trainee.Id);
            foreach (var test in testForTrainee)
            {
                if ((test.ScoreTest != null) && (test.ScoreTest == true))
                    return true;
            }
            return false;
        }

        public bool isAllowLicenseForCarType(Trainee _trainee, CarType type)
        {
            IEnumerable<Test> testForTrainee = d.getAllTests(t => t.TraineeId == _trainee.Id);
            foreach (var test in testForTrainee)
            {
                if ((test.ScoreTest != null) && (test.ScoreTest == true) && (test.carTypeTest == type))
                    return true;
            }
            return false;
        }

        public int numOfPassedTestForYear(int year)
        {
            var v = from test in d.getAllTests(t => t.TestDateTime.Year == year)
                    where ((test.ScoreTest != null) && (test.ScoreTest == true))
                    select test;
            return v.Count();
        }

        #endregion

        #region Test Functions

        public Test addTest(Trainee trainee, DateTime wantedTestDate)
        {
            if (wantedTestDate <= DateTime.Today)
                throw new Exception("Test date cannot be earlier then now!");

            if (trainee.NumLessons < Configuration.MIN_CLASSES) throw new Exception("Trainee can not set a test! not enough lessons have been taken!");

            List<Test> testsForTrainee = d.getAllTests(t => t.TraineeId == trainee.Id).ToList();
            if (testsForTrainee.Count() != 0)
            {
                if (testsForTrainee.Exists(s => (s.carTypeTest == trainee.CarTypeTrainee) && ((s.ScoreTest == null) || (s.ScoreTest == true))))
                    throw new Exception("BL: Test for this trainee and this car type already exist!");

                int period = (int)(wantedTestDate - testsForTrainee[0].TestDateTime).TotalDays;
                if (Math.Abs(period) < Configuration.MIN__DAYS_AFTER_TEST)
                    throw new Exception("BL: Not enough time passed since last test!");
            }

            var groupedList = GetAllTestersGroupByExpertise();
            List<Tester> matchExpertiseTesters = new List<Tester>();

            foreach (var item in groupedList)
            {
                if (item.Key == trainee.CarTypeTrainee)
                    foreach (var tester in item)
                    {
                        matchExpertiseTesters.Add(tester);  //add to matchExpertiseTesters testers whose expertise as needed
                    }
            }

            if (matchExpertiseTesters.Count == 0) throw new Exception("BL: there is no tester for needed car type expertise!");

            if ((trainee.isUltraOrtodox) && (!matchExpertiseTesters.Exists(k => k.GenderTester == trainee.GenderTrainee)))
                throw new Exception("BL: there is no tester matchs your Gender!");

            List<Tester> macthCloseTesters = getCloseTesters(trainee.AddressTrainee).ToList();
            if (macthCloseTesters.Count == 0) throw new Exception("BL: there is no tester in this radius!");

            string idTester = "";

            bool flag = false;
            foreach (var tester in matchExpertiseTesters)
            {
                if (macthCloseTesters.Exists(t => t.Id == tester.Id))
                {
                    if (!trainee.isUltraOrtodox || (trainee.isUltraOrtodox && trainee.GenderTrainee == tester.GenderTester))
                    {
                        flag = true;                //we found match tester!
                        break;
                    }
                }
            }

            if (!flag) throw new Exception("BL: there is no tester stand in requirements!");

            List<Tester> macthAvailableTesters = getAvailableTesters(wantedTestDate).ToList();

            if (macthAvailableTesters.Count == 0) throw new Exception("BL: there is no tester availabled at this date and time!  Please try another date!");


            flag = false;
            foreach (var tester in matchExpertiseTesters)
            {
                if ((macthCloseTesters.Exists(t => t.Id == tester.Id)) && (macthAvailableTesters.Exists(t => t.Id == tester.Id)))
                    if (getNumOfTesterTestsForWeek(wantedTestDate, tester) < tester.MaxWeeklyTests)
                    {
                        if (!trainee.isUltraOrtodox || (trainee.isUltraOrtodox && trainee.GenderTrainee == tester.GenderTester))
                        {
                            flag = true;                //we found match tester!
                            idTester = tester.Id;
                            break;
                        }
                    }
            }

            if (!flag) throw new Exception("BL: There is no tester availabled at thid date and time! Please try another date!");

            Test test = new Test(idTester, trainee.Id)
            {
                TestDateTime = wantedTestDate,
                carTypeTest = trainee.CarTypeTrainee,
                ExitAdress = trainee.AddressTrainee,
                IsAccessibleForHandicapped = trainee.IsHandicapped
            };

            isValidTest(test);
            d.addTest(test);
            return test;
        }

        public void updateTest(Test newTest)
        {
            // if the tester did not fill all the test fields
            if ((!newTest.ScoreTest.HasValue) || (newTest.TesterNote == null) || (!newTest.Criteria[Parameters.distance_keeping].HasValue)
                   || (!newTest.Criteria[Parameters.mirrors_looking].HasValue) || (!newTest.Criteria[Parameters.reverse_parking].HasValue)
                   || (!newTest.Criteria[Parameters.signaling].HasValue) || (!newTest.Criteria[Parameters.traffic_signs].HasValue))

                throw new Exception("BL: can not update a newTest without all the tester fields!");

            if (!isValidScore(newTest)) throw new Exception("BL: test's score does not match the criteria!");

            Test oldTest = d.getTest(newTest.TestCode);

            // if the tester changed one of test details
            if ((oldTest.TesterId != newTest.TesterId) || (oldTest.TraineeId != newTest.TraineeId) ||
                        (oldTest.TestDateTime != newTest.TestDateTime) || (!oldTest.ExitAdress.isSame(newTest.ExitAdress)) ||
                        (oldTest.carTypeTest != newTest.carTypeTest))
            {
                throw new Exception("BL: can not change basic datails of a test");
            }


            d.updateTest(newTest);
        }

        public IEnumerable<Test> getPlannedTests(DateTime date, DateChoice dayOrMonth)
        {
            if (dayOrMonth == DateChoice.day)
                return getMatchTests(t => t.TestDateTime.Date == date.Date);
            return getMatchTests(t => (t.TestDateTime.Month == date.Month && t.TestDateTime.Year == date.Year));
        }

        public IEnumerable<Test> getTestsList(Func<Test, bool> predicat = null)
        {
            return d.getAllTests(predicat);
        }

        #endregion

        #region Grouping Functions

        public IEnumerable<IGrouping<CarType, Tester>> GetAllTestersGroupByExpertise(bool toSort = false)
        {
            if (toSort) return from item in d.getTestersList()
                               group item by item.CarTypeTester into f1
                               orderby f1.Key
                               select f1;

            else return from item in d.getTestersList()
                        group item by item.CarTypeTester into f1
                        select f1;
        }
        public IEnumerable<IGrouping<string, Trainee>> GetAllTraineesGroupBySchoolName(bool toSort = false,string schoolName="")
        {
            if (toSort) return from item in d.getTraineesList(t=>t.SchoolName==schoolName)
                               group item by item.SchoolName into f1
                               orderby f1.Key
                               select f1;

            else return from item in d.getTraineesList()
                        group item by item.SchoolName into f1
                        select f1;
        }
        public IEnumerable<IGrouping<string, Trainee>> GetAllTraineesGroupByTester(bool toSort = false)
        {
            if (toSort) return from test in d.getAllTests()
                               let trainee = d.getTrainee(test.TraineeId)
                               group trainee by test.TesterId into f1
                               orderby f1.Key
                               select f1;

            else return from test in d.getAllTests()
                        let trainee = d.getTrainee(test.TraineeId)
                        group trainee by test.TesterId into f1
                        select f1;
        }

        public IEnumerable<IGrouping<int, Trainee>> GetAllTraineessGroupByTestsNum(bool toSort = false)
        {
            if (toSort) return from test in d.getAllTests()
                               let trainee = d.getTrainee(test.TraineeId)
                               let numTests = getNumTestsGeneral(trainee)
                               group trainee by numTests into f1
                               orderby f1.Key
                               select f1;

            else return from test in d.getAllTests()
                        let trainee = d.getTrainee(test.TraineeId)
                        let numTests = getNumTestsGeneral(trainee)
                        group trainee by numTests into f1
                        select f1;
        }

        #endregion

    }
}
