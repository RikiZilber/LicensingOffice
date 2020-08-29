using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BE
{

    public enum Gender { Male, Female }
    public enum CarType { TwoWheeled, Private, Van, MediumTruck, HeavyTruck }
    public enum GearBox { Manual, Automatic }
    public enum WorkerType { WorksContracter,CivilServant}
    public enum Parameters { distance_keeping, reverse_parking, mirrors_looking, signaling, traffic_signs } //partial addition
    public enum DateChoice { day, month}
    public enum Status { single, married,devorsed,widower}

    [Serializable]
    public struct Address
    {
        string street;
        int houseNumber;
        string city;
        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                Definitions.isValidName(value); // if the value is inValid the function throw exeption.
                street = value;
            }
        }

        public int HouseNumber
        {
            get
            {
                return houseNumber;
            }
            set
            {
                if (value < 0) throw new Exception("BL: House Number cannot be less than 0!"); // ****************** open comment
                houseNumber = value;

            }
        }

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                Definitions.isValidName(value); // if the value is inValid the function throw exeption.
                city = value;
            }
        }
        

        public bool isSame(Address newAddress)
        {
            return (city == newAddress.city && houseNumber == newAddress.houseNumber && street == newAddress.street);
        }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
    

    public class Definitions
    {
        /// <summary>
        /// checks weather the name is ligeal or throw exeption
        /// </summary>
        /// <param name="_value"></param>
        public static void isValidName(string _value)
        {
            if (_value != null)
            {
                if (_value.Length > 25) throw new Exception("BL:Too long name!!");
                char current;
                for (int i = 0; i < _value.Length; i++)
                {
                    current = _value[i];

                    if (((current < 65) && (current != 32)) || (current > 122) || ((current > 90) && (current < 96)))
                        throw new Exception("BL:name must contain only letters and spaces!");
                }
            }
        }

        /// <summary>
        /// checks weather the Id is ligeal or throw exeption
        /// </summary>
        /// <param name="_id"></param>
        public static void isValidId(string _id)     
        {
            if (_id != null)
            {
                if (_id.Length != 9) throw new Exception("BL:Id number must have 9 digits!");               
                char current;
                for (int i = 0; i < 9; i++)
                {
                     current = _id[i];
                     if ((current < 48) || (current > 57)) throw new Exception("BL:Id must contain only numbers!");
                }
            }
        }

        /// <summary>
        /// checks weather the cell number is ligeal or throw exeption
        /// </summary>
        /// <param name="_number"></param>
        public static void isValidCell(string _number)
        {
            if (_number != null)
            {
                if (_number.Length != 10) throw new Exception("BL:Cell Phone number must have 10 digits!");
                char current;
                if ((_number[0] != '0') || (_number[1] != '5')) throw new Exception("BL:Cell Phone number must begin with '05'!");
                for (int i = 0; i < 9; i++)
                {
                    current = _number[i];
                    if ((current < 48) || (current > 57)) throw new Exception("BL:Cell Phone must contain only numbers!");
                }
            }
        }

        /// <summary>
        /// checks if the date is not in future
        /// </summary>
        /// <param name="_number"></param>
        public static void isValidBirthDate(DateTime _date)
        {
            if (_date >= DateTime.Today)                           
                throw new Exception("Birth date cannot be later then now!");
        }

        /// <summary>
        /// checks if the date is not in past
        /// </summary>
        /// <param name="_number"></param>
        public static void isValidTestDate(DateTime _date)
        {
            if (_date <= DateTime.Today)
                throw new Exception("Test date cannot be earlier then now!");
        }

        /// <summary>
        /// help function
        /// </summary>
        /// <param name="_time"></param>
        /// <returns>int value of years in time span</returns>
        public static int convertToInt(TimeSpan _time)
        {
            return (int)(_time.TotalDays / 365);
        }

    }
}
