namespace FluentQueryBuilder
{
    public abstract class ConditionResolver: IConditionResolver
    {
        public virtual bool IsValid(string conditionName, bool reverse = false)
        {
            return reverse ? false : true;
        }
    }
}
