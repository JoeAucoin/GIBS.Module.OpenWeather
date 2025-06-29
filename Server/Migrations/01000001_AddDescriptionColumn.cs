//using GIBS.Module.OpenWeather.Migrations.EntityBuilders;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Oqtane.Migrations;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GIBS.Module.OpenWeather.Server.Migrations
//{
//    internal class _01000001_AddDescriptionColumn
//    {
//    }
//}


using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;

using GIBS.Module.OpenWeather.Migrations.EntityBuilders;     //Shivam.Employee.Repository;
using GIBS.Module.OpenWeather.Repository; //Shivam.Employee.Repository;

namespace GIBS.Module.OpenWeather.Server.Migrations
{

    [DbContext(typeof(OpenWeatherContext))]
    [Migration("OpenWeather.01.00.00.01")]
    public class _01000001_AddAddressColumn : MultiDatabaseMigration
    {
        public _01000001_AddAddressColumn(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var OpenWeatherEntityBuilder = new OpenWeatherEntityBuilder(migrationBuilder, ActiveDatabase);
            OpenWeatherEntityBuilder.AddStringColumn("Description", 2000, true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var OpenWeatherEntityBuilder = new OpenWeatherEntityBuilder(migrationBuilder, ActiveDatabase);
            OpenWeatherEntityBuilder.DropColumn("Description");
        }
    }
}