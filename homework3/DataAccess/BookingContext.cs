using homework3.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace homework3.DataAccess
{
	public class BookingContext : DbContext
	{
		public BookingContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Booking> Bookings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Booking>()
				.HasOne(x => x.User)
				.WithMany(u => u.Bookings)
				.HasForeignKey(b => b.UserId)
				.IsRequired(true);

			modelBuilder.Entity<Booking>()
				.Property(b => b.FromUtc)
				.IsRequired(true);

			modelBuilder.Entity<Booking>()
				.Property(b => b.ToUtc)
				.IsRequired(true);

			/*
			SELECT * FROM public."Bookings"
			WHERE "FromUtc" > '2022-08-02 20:51:37+00'
			vs
			SELECT * FROM public."Bookings"
			WHERE "Comment" like '%Пере%'
			 */
			modelBuilder.Entity<Booking>()
				.HasIndex(b => b.FromUtc);
			modelBuilder.Entity<Booking>()
				.HasIndex(b => b.ToUtc);
		}
	}
}
