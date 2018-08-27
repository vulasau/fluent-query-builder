namespace FluentQueryBuilder
{
    public interface IConditionResolver
    {
        /// <summary>
        /// Validates if condition is valid based on condition name.
        /// </summary>
        /// <param name="conditionName">Condition name</param>
        /// <returns>'True' if condition is valid, 'False' in other cases.</returns>
        bool IsValid(string conditionName);
    }
}
