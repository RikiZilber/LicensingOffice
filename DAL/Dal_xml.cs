using BE;
using DS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace DAL
{
    class Dal_xml:IDal
    {
        XElement testRoot;
        XElement configRoot;

        string testPath = @"testXml.xml";
        string traineePath = @"traineeXml.xml";
        string testerPath = @"testerXml.xml";
        string configPath = @"config.xml";


        private static Dal_xml instance = null;

        public static Dal_xml getDal_XML()
        {
            if (instance == null)
                instance = new Dal_xml();
            return instance;           
        }

        private Dal_xml()
        {
            if (!File.Exists(testPath))
                CreateFilesTest();
            else
                LoadDataTest();

            if (!File.Exists(configPath))
                CreateFilesCode();
            else
                LoadDataCode();

            if (!File.Exists(traineePath))
            {
                FileStream fileTrainee = new FileStream(traineePath, FileMode.Create);
                fileTrainee.Close();
            }
            else
                DataSource.traineesList = (loadListFromXML<Trainee>(traineePath));

            if (!File.Exists(testerPath))
            {
                FileStream fileTester = new FileStream(testerPath, FileMode.Create);
                fileTester.Close();
            }
            else
                DataSource.testersList = (loadListFromXML<Tester>(testerPath));

            saveListToXML<Trainee>(DataSource.traineesList, traineePath);
            saveListToXML<Tester>(DataSource.testersList, testerPath);
        }

        public static void saveListToXML<T>(List<T> list, string Path)
        {
            FileStream file = new FileStream(Path, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }

        public static List<T> loadListFromXML<T>(string path)
        {
            if (File.Exists(path))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(path, FileMode.Open);
                list = (List<T>)x.Deserialize(file);
                file.Close();
                return list;
            }
            else return new List<T>();
        }

        private void CreateFilesTest()
        {
            testRoot = new XElement("tests");
            testRoot.Save(testPath);
        }

        private void CreateFilesCode()
        {
            XElement code = new XElement("TestCode", "10000000");
            configRoot = new XElement("Config", code);
            configRoot.Save(configPath);
        }

        private void LoadDataTest()
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

        private void LoadDataCode()
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

        XElement ConvertTest(Test test)
        {
            XElement testElement = new XElement("test");

            foreach (PropertyInfo item in typeof(Test).GetProperties())
            {
                if (item.GetValue(test, null) != null)
                {
                    if (item.Name != "ExitAdress" && item.Name != "Criteria")
                    {
                        testElement.Add(new XElement(item.Name, item.GetValue(test, null).ToString()));
                    }
                    else if (item.Name == "ExitAdress")
                    {
                        XElement city = new XElement("City", test.ExitAdress.City);
                        XElement street = new XElement("Street", test.ExitAdress.Street);
                        XElement houseNumber = new XElement("HouseNumber", test.ExitAdress.HouseNumber);
                        testElement.Add(new XElement("ExitAdress", street, houseNumber, city));
                    }
                    else if (item.Name == "Criteria")
                    {
                        string sd = test.Criteria[Parameters.distance_keeping] != null ? test.Criteria[Parameters.distance_keeping].ToString() : "null";
                        string sm = test.Criteria[Parameters.mirrors_looking] != null ? test.Criteria[Parameters.mirrors_looking].ToString() : "null";
                        string sr = test.Criteria[Parameters.reverse_parking] != null ? test.Criteria[Parameters.reverse_parking].ToString() : "null";
                        string ss = test.Criteria[Parameters.signaling] != null ? test.Criteria[Parameters.signaling].ToString() : "null";
                        string st = test.Criteria[Parameters.traffic_signs] != null ? test.Criteria[Parameters.traffic_signs].ToString() : "null";

                        XElement distance = new XElement("distance_keeping", sd);
                        XElement mirrors = new XElement("mirrors_looking", sm);
                        XElement reverse = new XElement("reverse_parking", sr);
                        XElement signal = new XElement("signaling", ss);
                        XElement traffic = new XElement("traffic_signs", st);

                        testElement.Add(new XElement("Criteria", distance, mirrors, reverse, signal, traffic));
                    }
                }
                else
                {
                    testElement.Add(new XElement(item.Name, "null"));
                }
            }
            return testElement;
        }

        Test ConvertTest(XElement element)
        {
            Test test = new Test();

            foreach (PropertyInfo item in typeof(Test).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue;
                if (item.Name != "ExitAdress" && item.Name != "Criteria" && item.Name != "ScoreTest")
                {
                    convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);
                    if (item.CanWrite)
                        item.SetValue(test, convertValue);
                }
                else if (item.Name == "ExitAdress")
                {                  
                    string s = (element.Element(item.Name)).Element("Street").Value;
                    int h = int.Parse((element.Element(item.Name)).Element("HouseNumber").Value);
                    string c = (element.Element(item.Name)).Element("City").Value;
                    Address tmp = new Address() { Street = s, HouseNumber = h, City = c };
                    if (item.CanWrite)  item.SetValue(test, tmp);
                }
                else if (item.Name == "ScoreTest")
                {
                    string score = element.Element(item.Name).Value;
                    bool? bs = score != "null" ? bool.Parse(score) : (bool?)null;
                    test.ScoreTest = bs;
                }
                else if (item.Name == "Criteria")
                {
                    string sd = (element.Element(item.Name)).Element("distance_keeping").Value;
                    bool? d = sd != "null" ? bool.Parse(sd) : (bool?)null;

                    string sm = (element.Element(item.Name)).Element("mirrors_looking").Value;
                    bool? m = sm != "null" ? bool.Parse(sm) : (bool?)null;

                    string sr = (element.Element(item.Name)).Element("reverse_parking").Value;
                    bool? r = sr != "null" ? bool.Parse(sr) : (bool?)null;

                    string ss = (element.Element(item.Name)).Element("signaling").Value;
                    bool? s = ss != "null" ? bool.Parse(ss) : (bool?)null;

                    string st = (element.Element(item.Name)).Element("traffic_signs").Value;
                    bool? t = st != "null" ? bool.Parse(st) : (bool?)null;

                    // Dictionary<Parameters, bool?> criteria = new Dictionary<Parameters, bool?>();
                    test.Criteria[Parameters.distance_keeping] = d;
                    test.Criteria[Parameters.mirrors_looking] = m;
                    test.Criteria[Parameters.reverse_parking] = r;
                    test.Criteria[Parameters.signaling] = s;
                    test.Criteria[Parameters.traffic_signs] = t;
                }
            }
            return test;
        }
        #region Test Functions

        public void addTest(Test test)
        {
            LoadDataCode();
            long code = long.Parse(configRoot.Element("TestCode").Value);
            
            test.TestCode = ++code;
            if (test.TestCode > 99999999)
                throw new Exception("DAL: you cannot add the current test, you passed the limit of 8 digits code ");
            configRoot.Element("TestCode").Value = code.ToString();
            configRoot.Save(configPath);

            Test t = getTest(test.TestCode);
            if (t != null)
                throw new Exception("test with the same code already exists...");

            testRoot.Add(ConvertTest(test));
            testRoot.Save(testPath);
        }

        public void updateTest(Test test)
        {
            XElement toUpdate = (from item in testRoot.Elements()
                                 where long.Parse(item.Element("TestCode").Value) == test.TestCode
                                 select item).FirstOrDefault();

            if (toUpdate == null)
                throw new Exception("Test with the same code not found...");

            XElement updatedTest = ConvertTest(test);

            toUpdate.ReplaceNodes(updatedTest.Elements());

            testRoot.Save(testPath);
        }

        public Test getTest(long code)
        {
            XElement test = null;
            try
            {
                test = (from item in testRoot.Elements()
                          where long.Parse(item.Element("TestCode").Value) == code
                          select item).FirstOrDefault();

            }
            catch (Exception)
            {
                return null;
            }

            if (test == null)
                return null;

            return ConvertTest(test);
        }

        public IEnumerable<Test> getAllTests(Func<Test, bool> predicat = null)
        {
            if (predicat == null)
            {
                return from item in testRoot.Elements()
                       select ConvertTest(item);
            }

            return from item in testRoot.Elements()
                   let c = ConvertTest(item)
                   where predicat(c)
                   select c;
        }

        #endregion

        #region Trainee functions
        public void addTrainee(Trainee trainee)
        {
            DataSource.traineesList = (loadListFromXML<Trainee>(traineePath));
            Trainee trainee1 = DataSource.traineesList.FirstOrDefault(t => t.Id == trainee.Id);
            if (trainee1 != null)
                throw new Exception("DAL: Trainee with the same id already exists...");
            DataSource.traineesList.Add(trainee);
            saveListToXML(DS.DataSource.traineesList, traineePath);
        }

        public bool deleteTrainee(Trainee trainee)
        {
            DataSource.traineesList = (loadListFromXML<Trainee>(traineePath));
            Trainee t = getTrainee(trainee.Id);

            XElement test = (from item in testRoot.Elements()
                             where item.Element("TraineeId").Value == trainee.Id
                             select item).FirstOrDefault();

            if (test != null)
                throw new Exception("DAL: Test has been determinated for this trainee !!!");
            
            bool delete= DataSource.traineesList.Remove(t);
            saveListToXML(DS.DataSource.traineesList, traineePath);
            return delete;
        }

        public void updateTrainee(Trainee trainee)
        {
            DataSource.traineesList = (loadListFromXML<Trainee>(traineePath));
            int index = DataSource.traineesList.FindIndex(t => t.Id == trainee.Id);
            if (index == -1)
                throw new Exception("DAL: Trainee with the same id not found...");

            DataSource.traineesList[index] = trainee;
            saveListToXML(DS.DataSource.traineesList, traineePath);

        }
        public Trainee getTrainee(string id)
        {
            DataSource.traineesList = (loadListFromXML<Trainee>(traineePath));
            int index = DataSource.traineesList.FindIndex(t => t.Id == id);
            if (index == -1)
                throw new Exception("DAL: Trainee with the same id not found...");
            return DataSource.traineesList.FirstOrDefault(t => t.Id == id);
        }
        public IEnumerable<Trainee> getTraineesList(Func<Trainee, bool> predicat = null)
        {
            DataSource.traineesList = (loadListFromXML<Trainee>(traineePath));
            var v = from item in DataSource.traineesList
                    select new Trainee(item);

            if (predicat == null)
                return v.AsEnumerable().OrderByDescending(s => s.LastName);

            return v.Where(predicat).OrderByDescending(s => s.LastName);
        }


        #endregion

        #region Tester functions

       
        public Tester getTester(string _id)
        {
            DataSource.testersList = (loadListFromXML<Tester>(testerPath));
            int index = DataSource.testersList.FindIndex(t => t.Id == _id);
            if (index == -1)
                throw new Exception("DAL: Tester with the same id not found...");
            return DataSource.testersList.FirstOrDefault(t => t.Id == _id);
        }
        public void addTester(Tester tester)
        {
            DataSource.testersList = (loadListFromXML<Tester>(testerPath));
            Tester tester1 = DataSource.testersList.FirstOrDefault(t => t.Id == tester.Id); ;
            if (tester1 != null)
                throw new Exception("DAL: Tester with the same id already exists...");
            DataSource.testersList.Add(tester);
            saveListToXML(DS.DataSource.testersList, testerPath);
        }

        public bool deleteTester(Tester tester)
        {
            DataSource.testersList = (loadListFromXML<Tester>(testerPath));
            Tester t = getTester(tester.Id);
            //if (t == null)
            //    throw new Exception("DAL: Tester with the same id not found...");

            XElement test = (from item in testRoot.Elements()
                             where item.Element("TesterId").Value == tester.Id
                             select item).FirstOrDefault();

            if (test != null)
                throw new Exception("DAL: Test has been determinated for this tester!!!");

            //if (DataSource.testsList.Exists(ts => ts.TesterId == tester.Id))
            //    throw new Exception("DAL: Tests has been determinared for this tester !!!");

            bool delete= DataSource.testersList.Remove(t);
            saveListToXML(DS.DataSource.testersList, testerPath);
            return delete;
        }

        public void updateTester(Tester tester)
        {
            DataSource.testersList = (loadListFromXML<Tester>(testerPath));
            int index = DataSource.testersList.FindIndex(t => t.Id == tester.Id);
            if (index == -1)
                throw new Exception("DAL: Tester with the same id not found...");

            DataSource.testersList[index] = tester;
            saveListToXML(DS.DataSource.testersList, testerPath);
        }

        public IEnumerable<Tester> getTestersList(Func<Tester, bool> predicat = null)
        {
            DataSource.testersList = (loadListFromXML<Tester>(testerPath));
            var v = from item in DataSource.testersList
                    select new Tester(item);

            if (predicat == null)
                return v.AsEnumerable().OrderByDescending(s => s.LastName);

            return v.Where(predicat).OrderByDescending(s => s.LastName);
        }
        #endregion
    }

}
