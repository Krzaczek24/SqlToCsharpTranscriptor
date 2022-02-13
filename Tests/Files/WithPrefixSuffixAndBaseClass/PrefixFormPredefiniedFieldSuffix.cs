namespace WithPrefixSuffixAndBaseClass
{
	public class PrefixFormPredefiniedFieldSuffix : MyBaseClassName
	{
		public virtual string Title { get; set; }
		public virtual bool? Global { get; set; }
		public virtual PrefixFormFieldDefinitionSuffix BaseDefinition { get; set; }
		public virtual PrefixFormValueTypeSuffix ValueType { get; set; }
	}
}
