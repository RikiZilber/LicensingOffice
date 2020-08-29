using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;





namespace BE
{
    [Serializable]
    public class Trainee //: INotifyDataErrorInfo, IDataErrorInfo
    {

        private string idTrainee;
        public string Id
        {
            get { return idTrainee; }
            set
            {
                Definitions.isValidId(value);
                idTrainee = value;
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
        public int AgeTrainee
        {
            get
            { return Definitions.convertToInt(DateTime.Now - BirthDate); }
            set { }
        }

        public Gender GenderTrainee { get; set; }

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

        private Address addressTrainee;
        public Address AddressTrainee
        {
            get
            {
                return addressTrainee;
            }
            set
            {
                addressTrainee.City = value.City;
                addressTrainee.HouseNumber = value.HouseNumber;
                addressTrainee.Street = value.Street;
            }
        }

        public CarType CarTypeTrainee { get; set; }

        public GearBox gearBoxtrainee { get; set; }

        private string schoolName;
        public string SchoolName
        {
            get { return schoolName; }
            set
            {
                Definitions.isValidName(value);
                schoolName = value;
            }
        }

        private string teacherName;
        public string TeacherName
        {
            get { return teacherName; }
            set
            {
                Definitions.isValidName(value);
                teacherName = value;
            }
        }

        private int numLessons;
        public int NumLessons
        {
            get
            {
                return numLessons;
            }
            set
            {
                if (value < 0) throw new ArgumentException("BL:Number of lessons cannot be less than 0!");
                numLessons = value;
            }
        }

        //addition
        public Status PersonalStatus { get; set; }

        //addition
        public bool isUltraOrtodox { get; set; }

        //addition
        public bool IsHandicapped { get; set; }
   
        //addition
        private string imageSource;
        [Browsable(false)]
        public string ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }

        public override string ToString()
        {
            return this.ToStringProperty();
        }

        public Trainee()
        {
            //AddressTrainee = new Address();
            imageSource = (@"Empty Image");
            BirthDate = DateTime.Parse("01/01/1990");  //?
        }

        public Trainee(string _id)
        {
            //AddressTrainee = new Address();
            idTrainee = _id;
            imageSource = (@"Empty Image");
            BirthDate = DateTime.Parse("01/01/1990");  //?
        }

        public Trainee(Trainee other)
        {
            foreach (PropertyInfo prop in other.GetType().GetRuntimeProperties())
            {
                prop.SetValue(this, prop.GetValue(other));
            }
        }
    }
}

