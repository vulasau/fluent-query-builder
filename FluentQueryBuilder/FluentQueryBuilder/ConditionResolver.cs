namespace FluentQueryBuilder
{
    public abstract class ConditionResolver: IConditionResolver
    {
        public virtual bool IsValid(string conditionName)
        {
            return true;
        }
    }
}
