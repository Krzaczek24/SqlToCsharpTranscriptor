namespace WithPrefixSuffixAndBaseClass
{
	public class PrefixFormPredefiniedFieldOptionSuffix : MyBaseClassName
	{
		public virtual System.DateTime? Date { get; set; }
		public virtual string String { get; set; }
		public virtual bool? Boolean { get; set; }
		public virtual int? Integer { get; set; }
		public virtual PrefixFormPredefiniedFieldSuffix PredefiniedField { get; set; }
	}
}
