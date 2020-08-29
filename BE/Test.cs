using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BE

{
    [Serializable]
    public class Test
    {
        public long TestCode { get; set; }

        private string testerId;
        public string TesterId
        {
            get {return testerId; }
            set
            {
                Definitions.isValidId(value);
                testerId = value;
             }
        }

        private string traineeId;
        public string TraineeId
        {
            get { return traineeId; }
            set
            {
                Definitions.isValidId(value);
                traineeId = value;
            }
        }

        public CarType carTypeTest { get; set; }

        private DateTime testDateTime;         
        public DateTime TestDateTime { get; set; }    

        public Address ExitAdress { get; set; }

        //addition
        public bool IsAccessibleForHandicapped { get; set; }
        
        private Dictionary<Parameters, bool?> criteria = new Dictionary<Parameters, bool?>();
        public Dictionary<Parameters , bool?> Criteria
        {
            get
            {
                return criteria;
            }
            set
            {
                criteria = value;
            }
        }

        public bool? ScoreTest { get; set; }

        public string TesterNote { get; set; } 

        public Test(string _testerId, string _traineeId)
        {
            TesterId = _testerId;
            TraineeId = _traineeId;
            criteria.Add(Parameters.distance_keeping, null);
            criteria.Add(Parameters.mirrors_looking, null);
            criteria.Add(Parameters.reverse_parking, null);
            criteria.Add(Parameters.signaling, null);
            criteria.Add(Parameters.traffic_signs, null);
        }

        public Test() 
        {
            criteria.Add(Parameters.distance_keeping, null);
            criteria.Add(Parameters.mirrors_looking, null);
            criteria.Add(Parameters.reverse_parking, null);
            criteria.Add(Parameters.signaling, null);
            criteria.Add(Parameters.traffic_signs, null);
        }

        // go to generic tostring and adds the dict display
        public override string ToString()
        {
            return this.ToStringProperty() + showDictionary();
        }

        public Test(Test other)
        {
            foreach (PropertyInfo prop in other.GetType().GetRuntimeProperties())
            {
                prop.SetValue(this, prop.GetValue(other));
            }
        }

        public string showDictionary()
        {
            if (TesterNote == null) return "";
            string s = "Criteria for test: \n";

            foreach (var item in Criteria)
            {
                s += item.Key + ": " + item.Value + "\n";
            }        
            return s;
        }
    }
}
