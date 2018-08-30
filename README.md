# fluent-query-builder
An extensions for fluent mapping of object properties to dynamic entity and building string queries based on this properties parsing expression trees.

Provide IQueryExecutor interface implementation to object of IFluentQueryable<T> and work with any database source as with .net collections.

For conditional fields mapping implement IConditionResolver interface and set it up for ObjectMapperConfiguration.
For custom data type conversion implement IConverterResolver interface and set it up for ObjectMapperConfiguration.
Or just use custom converters implementing IPropertyConverter interface in FluentPropertyAttribute.

If any expression types should be transformed to string in a way different from standard behavior, implement IExpressionTypeTransformer interface and set it up for ExpressionParserConfiguration.

If and object types should be transformed to strings in queries, implement IStringificationRulesResolver interfaces and set it up for ExpressionParserConfiguration.

Default IQueryProvider<T> implementation not fully covers all needs of query building expressions.
For add, update, delete operations default query provider should be extended. In this case query provider can be substituted via implementation of IQueryProviderFactory.

Default implementation of IFluentQueriable<T> can also be extended if needed.
