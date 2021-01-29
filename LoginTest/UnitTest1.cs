using Xunit;
using Login.Data;
using System.Linq;
using Login.Utilities;

namespace LoginTest
{
    public class UnitTest1
    {
        
        [Fact]
        public void ShouldGetNumberOfPendingTickets()
        {
            using (var context = new ApplicationDbContext(DbOptionsFactory.DbContextOptions))
            {
                var dbResponse = context.Ticket.Where(x => x.TicketPriority == Priority.Pending);
                Assert.NotNull(dbResponse);
            }
            
        }
        [Fact]
        public void ShouldGetNumberOfAverageTickets()
        {
            using (var context = new ApplicationDbContext(DbOptionsFactory.DbContextOptions))
            {
                var dbResponse = context.Ticket.Where(x => x.TicketPriority == Priority.Average);
                Assert.NotNull(dbResponse);
            }
        }
        [Fact]
        public void ShouldGetNumberOfLowTickets()
        {
            using (var context = new ApplicationDbContext(DbOptionsFactory.DbContextOptions))
            {
                var dbResponse = context.Ticket.Where(x => x.TicketPriority == Priority.Low);
                Assert.NotNull(dbResponse);
            }
        }
        [Fact]
        public void ShouldGetNumberOfHighTickets()
        {
            using (var context = new ApplicationDbContext(DbOptionsFactory.DbContextOptions))
            {
                var dbResponse = context.Ticket.Where(x => x.TicketPriority == Priority.High);
                Assert.NotNull(dbResponse);
            }
        }
        [Fact]
        public void ShouldGetNumberOfCriticalTickets()
        {
            using (var context = new ApplicationDbContext(DbOptionsFactory.DbContextOptions))
            {
                var dbResponse = context.Ticket.Where(x => x.TicketPriority == Priority.Critical);
                Assert.NotNull(dbResponse);
            }
        }
    }
}
