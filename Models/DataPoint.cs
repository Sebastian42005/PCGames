using System.Runtime.Serialization;

namespace PCGamesFinal.Models
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(int id, string label, double y)
        {
            this.id = id;
            this.Label = label;
            this.y = y;
        }

        [DataMember(Name = "id")]
        public int id = 0;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> y = null;
    }
}
