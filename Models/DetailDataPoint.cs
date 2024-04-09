using System.Runtime.Serialization;

namespace PCGamesFinal.Models
{
	[DataContract]
	public class DetailDataPoint
	{
		public DetailDataPoint(double x, double y)
		{
			this.x = x;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "x")]
		public Nullable<double> x = null;

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
		public DateTime date { get; set; }
	}
}
