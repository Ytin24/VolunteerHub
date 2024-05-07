using Microsoft.Extensions.Configuration;
using System;

public class ConnectionStringProvider {
    public string GerMssqlConnectionString() {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        return configuration.GetConnectionString("MSSQL");
    }
}
