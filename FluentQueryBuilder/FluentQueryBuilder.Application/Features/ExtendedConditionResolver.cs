namespace FluentQueryBuilder.Application.Features
{
    public class ExtendedConditionResolver: IConditionResolver
    {
        public bool IsValid(string conditionName)
        {
            return FeatureToggle.IsValid(conditionName);
        }
    }
}
