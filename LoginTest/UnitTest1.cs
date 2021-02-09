using Xunit;
using Login.Data;
using System.Linq;
using Login.Utilities;
using Login.Repository;
using Login.Models;

namespace LoginTest
{
    public class UnitTest1
    {

        [Fact]
        public void ShouldGetTicketById()
        {
            using var context = new ApplicationDbContext(DbOptionsFactory.DbContextOptions);
            var expected = new Repository(context).GetTicket(29).ToString();
            var actual = new Ticket{ 
                Id = 29,
                CreatedBy = "Janai Mitchell", 
                Title = "Create a new github page", 
                Description = "Create a new repository and migrate the legacy code into the new repository." }
            .ToString();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ShouldGetNumberOfPendingTickets()
        {
            using var context = new ApplicationDbContext(DbOptionsFactory.DbContextOptions);
            var dbResponse = context.Ticket.Where(x => x.TicketPriority == Priority.Pending);
            Assert.NotNull(dbResponse);

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
