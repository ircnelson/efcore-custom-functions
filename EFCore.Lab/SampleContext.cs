using System.Linq.Expressions;
using System.Reflection;
using EFCore.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

public class SampleContext : DbContext
{
    public SampleContext(DbContextOptions<SampleContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var methodInfo = typeof(MyDbFunctions).GetMethod(nameof(MyDbFunctions.GreaterThanOrEqual), BindingFlags.Static | BindingFlags.Public, [typeof(byte[]), typeof(byte[])]);

        modelBuilder
            .HasDbFunction(methodInfo)
            .HasTranslation(args =>
            {
                var leftArg = args[0];
                var expr = new SqlBinaryExpression(ExpressionType.GreaterThanOrEqual, leftArg, args[1], leftArg.Type, leftArg.TypeMapping);

                return expr;
            });
    }

    public DbSet<SampleEntity> SampleEntities => Set<SampleEntity>();
}

public static class MyDbFunctions
{
    public static bool GreaterThanOrEqual(byte[] left, byte[] right) => throw new NotImplementedException();
}