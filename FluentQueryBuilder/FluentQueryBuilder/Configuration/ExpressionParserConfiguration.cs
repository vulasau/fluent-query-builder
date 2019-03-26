using System;

namespace FluentQueryBuilder.Configuration
{
    public static class ExpressionParserConfiguration
    {
        public static IExpressionTypeTransformer ExpressionTypeTransformer { get; private set; }
        public static IStringificationRulesResolver StringificationRulesResolver { get; private set; }
        public static INullComparisonResolver NullComparisonResolver { get; private set; }

        static ExpressionParserConfiguration()
        {
            ExpressionTypeTransformer = new ExpressionTypeTransformer();
            StringificationRulesResolver = new StringificationRulesResolver();
            NullComparisonResolver = new NullComparisonResolver();
        }

        public static void Use(IExpressionTypeTransformer expressionTypeTransformer)
        {
            if (expressionTypeTransformer == null)
                throw new ArgumentNullException("expressionTypeTransformer", "'ExpressionTypeTransformer' parameter should be set to non nullable value.");

            ExpressionTypeTransformer = expressionTypeTransformer;
        }

        public static void Use(IStringificationRulesResolver stringificationRulesResolver)
        {
            if(stringificationRulesResolver == null)
                throw new ArgumentNullException("stringificationRulesResolver", "'StringificationRulesResolver' parameter should be set to non nullable value.");

            StringificationRulesResolver = stringificationRulesResolver;
        }

        public static void Use(INullComparisonResolver nullComparisonResolver)
        {
            if (nullComparisonResolver == null)
                throw new ArgumentNullException("nullComparisonResolver", "'NullComparisonResolver' parameter should be set to non nullable value.");

            NullComparisonResolver = nullComparisonResolver;
        }
    }
}
