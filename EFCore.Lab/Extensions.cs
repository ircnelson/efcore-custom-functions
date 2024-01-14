using Microsoft.EntityFrameworkCore;

namespace EFCore.Lab;

public static class Extensions
{
    public static void PrepareDatabase(SampleContext myContext)
    {
        myContext.Database.EnsureCreated();

        if (!myContext.Database.CanConnect()) return;
        
        myContext.Database.Migrate();

        if (myContext.SampleEntities.Any()) return;
        
        for (var i = 0; i < 10; i++)
        {
            myContext.SampleEntities.Add(new SampleEntity
            {
                Id = Guid.NewGuid(),
                Number = i
            });
        }

        myContext.SaveChanges();
    }
    
    public static byte[] ToByteArray(this string hex)
    {
        var x = hex.Replace("0x", "");
        
        var numberChars = x.Length;
        var bytes = new byte[numberChars / 2];

        for (var i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(x.Substring(i, 2), 16);
        }

        return bytes;
    }
}