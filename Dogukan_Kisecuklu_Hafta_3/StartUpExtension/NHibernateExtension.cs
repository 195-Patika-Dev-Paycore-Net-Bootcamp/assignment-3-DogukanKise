using Dogukan_Kisecuklu_Hafta_3.Context.Abstract;
using Dogukan_Kisecuklu_Hafta_3.Context.Concrete;
using Dogukan_Kisecuklu_Hafta_3.Model.Concrete;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;


namespace Dogukan_Kisecuklu_Hafta_3.StartUpExtension
{
    public static class NHibernateExtension
    {
        public static IServiceCollection AddNHibernatePosgreSql(this IServiceCollection services, string connectionString)
        {
            // This is an extension for the PostgreSQL connection. All the codes are default except injection 

            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(NHibernateExtension).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<PostgreSQLDialect>();
                c.ConnectionString = connectionString;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.SchemaAction = SchemaAutoAction.Validate;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });
            configuration.AddMapping(domainMapping);

            var sessionFactory = configuration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());


            // INJECTION
            services.AddScoped<IMapperSession<Vehicle>, VehicleSession>();
            services.AddScoped<IMapperSession<Container>, ContainerSession>();

            return services;
        }
    }
}
