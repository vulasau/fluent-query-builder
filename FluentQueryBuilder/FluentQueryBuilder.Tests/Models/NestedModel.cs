using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Tests.Models
{
    [FluentEntity(MODEL_NAME)]
    public class NestedModel : NestedModelFields
    {
        public override string StringProperty
        {
            get { return Field; }
            set { Field = value; }
        }

        public override bool ConditionalProperty
        {
            get { return ConditionalField; }
            set { ConditionalField = value; }
        }

        public override int ConditionalReversedProperty
        {
            get { return ConditionalReversedField; }
            set { ConditionalReversedField = value; }
        }

        public override double ReadonlyProperty
        {
            get { return ReadonlyField; }
            set { ReadonlyField = value; }
        }

        public override int DependentProperty
        {
            get { return DependentField; }
            set { DependentField = value; }
        }

        public override int DependentReversedProperty
        {
            get { return DependentReversedField; }
            set { DependentReversedField = value; }
        }

        public override bool Dependency { get; set; }

        public NestedModel()
        {
            Dependency = true;
        }

        public NestedModel(bool empty) : this()
        {
            if (!empty)
            {
                StringProperty = FIELD_VALUE;
                ConditionalProperty = CONDITIONAL_FIELD_VALUE;
                ConditionalReversedProperty = CONDITIONAL_REVERSED_FIELD_VALUE;
                ReadonlyProperty = READONLY_FIELD_VALUE;
                DependentField = DEPENDENT_FIELD_VALUE;
                DependentReversedField = DEPENDENT_REVERSED_FIELD_VALUE;
            }
        }
    }
}
