namespace WithBaseClass
{
	public class FormPredefiniedField : MyBaseClassName
	{
		public virtual string Title { get; set; }
		public virtual bool Global { get; set; }
		public virtual FormFieldDefinition BaseDefinition { get; set; }
		public virtual FormValueType ValueType { get; set; }
	}
}
