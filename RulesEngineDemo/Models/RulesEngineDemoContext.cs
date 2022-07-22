using Microsoft.EntityFrameworkCore;

namespace RulesEngineDemo.Models
{
    public class RulesEngineDemoContext : RulesEngineContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
          => options.UseSqlServer(@"Server=localhost;Database=RulesEngineDemo;Trusted_Connection=true");
          // => options.UseSqlServer(@"Server=localhost;Database=RulesEngineDemo;User ID=sa;Password=Rampanttheater20!;");
    }

}
