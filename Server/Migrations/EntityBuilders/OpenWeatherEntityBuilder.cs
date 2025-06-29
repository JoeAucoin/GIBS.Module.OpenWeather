using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.OpenWeather.Migrations.EntityBuilders
{
    public class OpenWeatherEntityBuilder : AuditableBaseEntityBuilder<OpenWeatherEntityBuilder>
    {
        private const string _entityTableName = "GIBSOpenWeather";
        private readonly PrimaryKey<OpenWeatherEntityBuilder> _primaryKey = new("PK_GIBSOpenWeather", x => x.OpenWeatherId);
        private readonly ForeignKey<OpenWeatherEntityBuilder> _moduleForeignKey = new("FK_GIBSOpenWeather_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public OpenWeatherEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override OpenWeatherEntityBuilder BuildTable(ColumnsBuilder table)
        {
            OpenWeatherId = AddAutoIncrementColumn(table,"OpenWeatherId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> OpenWeatherId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
       
    }
}
