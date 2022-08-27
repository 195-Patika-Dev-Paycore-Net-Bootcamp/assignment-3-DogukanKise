using Dogukan_Kisecuklu_Hafta_3.Model.Concrete;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dogukan_Kisecuklu_Hafta_3.Mapping
{
    public class ContainerMap : ClassMapping<Container>
    {
        // Container's mapping process Type,ColumnName, Nullable etc.
        public ContainerMap()
        {
            Id(x => x.id, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("id");
                x.Generator(Generators.Increment);
            });

            Property(b => b.container_name, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.Column("container_name");
                x.NotNullable(true);
            });
            Property(b => b.latitude, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Column("latitude");
                x.NotNullable(true);
            });
            Property(b => b.longitude, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Column("longitude");
                x.NotNullable(true);
            });
            Property(b => b.vehicle_id, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("vehicle_id");
                x.NotNullable(true);
            });
        }
    }
}

