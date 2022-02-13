using System;

namespace DatabaseModels
{
	public class FormPredefiniedField
	{
		public virtual int Id { get; set; }
		public virtual string AddLogin { get; set; }
		public virtual DateTime? AddDate { get; set; }
		public virtual string ModifLogin { get; set; }
		public virtual DateTime? ModifDate { get; set; }
		public virtual bool? Active { get; set; }
		public virtual string Title { get; set; }
		public virtual bool? Global { get; set; }
		public virtual FormFieldDefinition BaseDefinition { get; set; }
		public virtual FormValueType ValueType { get; set; }
	}
}
