using System;

namespace WithPrefixAndSuffix
{
	public class PrefixFormPredefiniedFieldOptionSuffix
	{
		public virtual int Id { get; set; }
		public virtual string AddLogin { get; set; }
		public virtual DateTime? AddDate { get; set; }
		public virtual string ModifLogin { get; set; }
		public virtual DateTime? ModifDate { get; set; }
		public virtual bool? Active { get; set; }
		public virtual DateTime? Date { get; set; }
		public virtual string String { get; set; }
		public virtual bool? Boolean { get; set; }
		public virtual int? Integer { get; set; }
		public virtual decimal? Decimal { get; set; }
		public virtual PrefixFormPredefiniedFieldSuffix PredefiniedField { get; set; }
	}
}
