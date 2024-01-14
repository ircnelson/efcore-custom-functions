using EFCore.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

const string connectionString = "Server=localhost,1435;Database=EFCoreLab;User Id=SA;Password=Passw0rd;TrustServerCertificate=True;";
services.AddDbContext<SampleContext>(options => options.UseSqlServer(connectionString));

var serviceProvider = services.BuildServiceProvider();

var dbContext = serviceProvider.GetRequiredService<SampleContext>();

Extensions.PrepareDatabase(dbContext);

var version = "0x00000000000007D6".ToByteArray();

var queryable = dbContext.SampleEntities.Where(x => MyDbFunctions.GreaterThanOrEqual(x.Version, version));

Console.WriteLine(queryable.ToQueryString());

var records = queryable.ToList();

Console.WriteLine(records.Count);

