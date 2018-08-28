using System;
using FluentQueryBuilder.Application.Converters;
using FluentQueryBuilder.Application.DataAccess;
using FluentQueryBuilder.Application.Features;
using FluentQueryBuilder.Configuration;

namespace FluentQueryBuilder.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var queryExecutor = new DataConnector();

            var converterResolver = new ExtendedConverterResolver();
            var conditionResolver = new ExtendedConditionResolver();

            ObjectMapperConfiguration.Use(converterResolver);
            ObjectMapperConfiguration.Use(conditionResolver);

            var contactsRepository = new ContactsRepository(queryExecutor);

            var contacts = contactsRepository.Where(c => c.Wiegth > 70).ToArray();
            var contact = contactsRepository.FirstOrDefault(c => c.ID == 1187);
            var milenials = contactsRepository.Where(c => c.BirthDate > new DateTime(2000, 1, 1)).Take(20).OrderBy(c => c.Name).ToArray();
        }
    }
}
