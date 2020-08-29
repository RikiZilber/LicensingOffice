using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Serialization;



namespace BE
{
    [Serializable]
    public class Tester
    {

        private string idTester;
        public string Id
        {
            get { return idTester; }
            set
            {
                Definitions.isValidId(value);
                idTester = value;
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                Definitions.isValidName(value);
                firstName = value;
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                Definitions.isValidName(value);
                lastName = value;
            }
        }

        private DateTime birthDate;       
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                Definitions.isValidBirthDate(value);
                birthDate = value;
            }
        }

        //addition
        public int  AgeTester
        {
            get { return Definitions.convertToInt(DateTime.Now - BirthDate);
            }
            set { }
        }

        public Gender GenderTester { get; set; } 

        private string cellPhone;
        public string CellPhone
        {
            get { return cellPhone; }
            set
            {
                Definitions.isValidCell(value);
                cellPhone = value;
            }
        }

        public Address AddressTester { get; set; }

        private int experienceYears;
        public int ExperienceYears
        {
            get
            {
                return experienceYears;
            }
            set
            {
                if (value < 0) throw new Exception("BL:Experience Years cannot be less than 0!");
                experienceYears = value;
            }
        }

        private int maxWeeklyTests;
        public int MaxWeeklyTests
        {
            get
            {
                return maxWeeklyTests;
            }
            set
            {
                if (value < 1) throw new Exception("BL:maximum weekly tests cannot be less than 1!");
                maxWeeklyTests = value;
            }
        }

        public CarType CarTypeTester { get; set; }

        private bool[,] availabilityTester = new bool[Configuration.NUM_OF_HOURS,Configuration.NUM_OF_DAYS];
        [XmlIgnore]
        public bool[,] AvailabilityTester
        {
            get { return availabilityTester; }
            set
            {
                for (int i = 0; i < Configuration.NUM_OF_HOURS; i++)
                {
                    for (int j = 0; j < Configuration.NUM_OF_DAYS; j++)
                    {
                        availabilityTester[i, j] = value[i, j];
                    }
                }
            }        
        }

        public string matrix_availability //to XML serializer
        {
            get
            {
                if (availabilityTester == null)
                    return null;
                string result = "";
                if (availabilityTester != null)
                {
                    int sizeA = availabilityTester.GetLength(0);
                    int sizeB = availabilityTester.GetLength(1);
                    result += "" + sizeA + "," + sizeB;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            result += "," + availabilityTester[i, j];
                }
                return result;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    int sizeA = int.Parse(values[0]);
                    int sizeB = int.Parse(values[1]);
                    availabilityTester = new bool[sizeA, sizeB];
                    int index = 2;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            availabilityTester[i, j] = bool.Parse(values[index++]);
                }
            }
        }

        private double maxDistanceTester;
        public double MaxDistanceTester
        {
            get
            {
                return maxDistanceTester;
            }
            set
            {
                if (value < 0) throw new Exception("BL:Max distance from tester cannot be less than 0!");
                maxDistanceTester = value;
            }
        }

        //addition
        public Status PersonalStatus { get; set; }

        //addition
        public WorkerType WorkerType { get; set; }

        //addition
        private string imageSource;
        public string ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }

        // go to generic to string and add the matrix display
        public override string ToString()
        {
            return this.ToStringProperty() + showMatrix();
        }

        public Tester()
        {
            imageSource = (@"Empty Image");
            BirthDate = DateTime.Parse("01/01/1976");      
        }

        public Tester(string _id)
        {
            idTester = _id;
            imageSource = (@"Empty Image");
            BirthDate = DateTime.Parse("01/01/1976");      
        }

        public Tester(Tester other)
        {
            foreach (PropertyInfo prop in other.GetType().GetRuntimeProperties())
            {
                prop.SetValue(this, prop.GetValue(other));
            }

        }

        // display the matrix of availablity of tester
        public string showMatrix()
        {
            string s = "Availability of tester: \n";
            for (int i = 0; i < 5; i++)
            {
                s += "Day " + (i+1) + " : ";
                for (int j = 0; j < 6; j++)
                {
                    if (AvailabilityTester[j, i] == true)
                    {
                        int h = j + 9;
                        s += h + ":00, ";
                    }
                }
                s += "\n";
            }
            return s;
        }
    }
}
