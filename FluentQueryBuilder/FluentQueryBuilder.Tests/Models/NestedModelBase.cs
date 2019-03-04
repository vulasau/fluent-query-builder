namespace FluentQueryBuilder.Tests.Models
{
    public abstract class NestedModelBase
    {
        public abstract string StringProperty { get; set; }
        public abstract bool ConditionalProperty { get; set; }
        public abstract int ConditionalReversedProperty { get; set; }
        public abstract double ReadonlyProperty { get; set; }

        public abstract int DependentProperty { get; set; }
        public abstract int DependentReversedProperty { get; set; }
    }
}