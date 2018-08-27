namespace FluentQueryBuilder
{
    public class ConditionResolver: IConditionResolver
    {
        public virtual bool IsValid(string conditionName)
        {
            return true;
        }
    }
}
