namespace WithChangedAccessModifier
{
	internal class FormFieldDefinitionValueType : MyBaseClassName
	{
		public virtual FormValueType ValueType { get; set; }
		public virtual FormFieldDefinition FieldDefinition { get; set; }
	}
}
