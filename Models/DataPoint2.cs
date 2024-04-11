using System.Runtime.Serialization;

namespace PCGamesFinal.Models
{
    [DataContract]
    public class DataPoint2
    {
        public DataPoint2(string x, double y)
        {
            this.x = x;
            this.y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public string x = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> y = null;

        public DateTime date { get; set; }
    }
}
