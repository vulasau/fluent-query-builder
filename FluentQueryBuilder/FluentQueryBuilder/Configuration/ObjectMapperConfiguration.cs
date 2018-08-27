using System;

namespace FluentQueryBuilder.Configuration
{
    public static class ObjectMapperConfiguration
    {
        public static IConditionResolver ConditionResolver { get; private set; }
        public static IConverterResolver ConverterResolver { get; private set; }

        static ObjectMapperConfiguration()
        {
            ConditionResolver = new ConditionResolver();
            ConverterResolver = new ConverterResolver();
        }

        public static void Use(IConditionResolver conditionResolver)
        {
            if (conditionResolver == null)
                throw new ArgumentNullException("conditionResolver", "'ConditionResolver' parameter should be set to non nullable value.");

            ConditionResolver = conditionResolver;
        }

        public static void Use(IConverterResolver converterResolver)
        {
            if (converterResolver == null)
                throw new ArgumentNullException("converterResolver", "'ConverterResolver' parameter should be set to non nullable value.");

            ConverterResolver = converterResolver;
        }
    }
}
