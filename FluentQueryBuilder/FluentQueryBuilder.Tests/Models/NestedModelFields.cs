using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Tests.Models
{
    public abstract class NestedModelFields : NestedModelBase
    {
        public const string MODEL_NAME = "NestedModelFields";
        public const string FIELD_NAME = "Field";
        public const string CONDITIONAL_FIELD_NAME = "ConditionalField";
        public const string CONDITIONAL_REVERSED_FIELD_NAME = "ConditionalReversedField";
        public const string READONLY_FIELD_NAME = "ReadonlyField";
        public const string DEPENDENT_FIELD_NAME = "DependentField";
        public const string DEPENDENT_REVERSED_FIELD_NAME = "DependentReversedField";

        public const string FIELD_VALUE = "field";
        public const bool CONDITIONAL_FIELD_VALUE = true;
        public const int CONDITIONAL_REVERSED_FIELD_VALUE = 100;
        public const double READONLY_FIELD_VALUE = 300;
        public const int DEPENDENT_FIELD_VALUE = 1;
        public const int DEPENDENT_REVERSED_FIELD_VALUE = 3;

        [FluentProperty]
        public string Field { get; protected set; }

        [FluentProperty]
        [Condition("Hola")]
        public bool ConditionalField { get; protected set; }

        [FluentProperty]
        [Condition("Hola", true)]
        public int ConditionalReversedField { get; protected set; }

        [FluentProperty(isReadonly: true)]
        public double ReadonlyField { get; protected set; }

        [FluentProperty]
        [Dependency("Dependency")]
        public int DependentField { get; protected set; }

        [FluentProperty]
        [Dependency("Dependency", true)]
        public int DependentReversedField { get; protected set; }

        public abstract bool Dependency { get; set; }
    }
}
