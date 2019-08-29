using Bogus;
using Saritasa.UnrealEstate.DataAccess.DbContexts;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.DataAccess.DbPlaceholders.EstateDb
{
    /// <summary>
    /// Static class that stores the logic of filling the database with fake data.
    /// </summary>
    public static class FakeDataEstateDbPlaceholder
    {
        private static readonly EstateDbContext context = new EstateDbContext();

        static FakeDataEstateDbPlaceholder()
        {
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Fill listings table with fake data async.
        /// </summary>
        public static async Task FillListingsAsync()
        {
            var listingsGenerator = new Faker<Listing>()
                .RuleFor(l => l.City, (f, l) => f.Address.City())
                .RuleFor(l => l.State, (f, l) => f.Address.State())
                .RuleFor(l => l.Zip, (f, l) => f.Address.ZipCode())
                .RuleFor(l => l.AddressLine1, (f, l) => f.Address.FullAddress())
                .RuleFor(l => l.AddressLine2, (f, l) => f.Address.SecondaryAddress())
                .RuleFor(l => l.Status, (f, l) => f.PickRandom<ListingStatus>())
                .RuleFor(l => l.Beds, (f, l) => f.Random.Number(1, 10))
                .RuleFor(l => l.Size, (f, l) => f.Random.Double(1, 3000))
                .RuleFor(l => l.BuiltYear, (f, l) => f.Date.Past(1000).Year)
                .RuleFor(l => l.StartingPrice, (f, l) => f.Random.Decimal(1, 1000000))
                .RuleFor(l => l.DueDate, (f, l) => f.Date.Future())
                .RuleFor(l => l.Description, (f, l) => f.Lorem.Text());

            List<Listing> listings = listingsGenerator.Generate(10);

            foreach (var listing in listings)
            {
                await context.Listings.AddAsync(listing);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Fill listing photos table with fake data async.
        /// </summary>
        public static async Task FillListingPhotosAsync()
        {
            var listingPhotosGenerator = new Faker<ListingPhoto>()
                .RuleFor(lp => lp.Listing, (f, lp) => f.PickRandom<Listing>(context.Listings))
                .RuleFor(lp => lp.PhotoUrl, (f, lp) => f.Internet.Url());

            List<ListingPhoto> listingPhotos = listingPhotosGenerator.Generate(10);

            foreach (var listingPhoto in listingPhotos)
            {
                await context.ListingPhotos.AddAsync(listingPhoto);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Fill listing notes table with fake data async.
        /// </summary>
        public static async Task FillListingNotesAsync()
        {
            var listingNotesGenerator = new Faker<ListingNote>()
                .RuleFor(ln => ln.Listing, (f, ln) => f.PickRandom<Listing>(context.Listings))
                .RuleFor(ln => ln.User, (f, ln) => f.PickRandom<User>(context.Users))
                .RuleFor(ln => ln.Text, (f, ln) => f.Lorem.Text());

            List<ListingNote> listingNotes = listingNotesGenerator.Generate(10);

            foreach (var listingNote in listingNotes)
            {
                await context.ListingNotes.AddAsync(listingNote);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Fill bids table with fake data async.
        /// </summary>
        public static async Task FillBidsAsync()
        {
            var bidsGenerator = new Faker<Bid>()
                .RuleFor(b => b.Listing, (f, b) => f.PickRandom<Listing>(context.Listings))
                .RuleFor(b => b.User, (f, b) => f.PickRandom<User>(context.Users))
                .RuleFor(b => b.Price, (f, b) => f.Random.Decimal(10000, 1000000))
                .RuleFor(b => b.Comment, (f, b) => f.Lorem.Text());

            List<Bid> bids = bidsGenerator.Generate(10);

            foreach (var bid in bids)
            {
                await context.Bids.AddAsync(bid);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Fill comments table with fake data async.
        /// </summary>
        public static async Task FillCommentsAsync()
        {
            var commentsGenerator = new Faker<Comment>()
                .RuleFor(c => c.Listing, (f, c) => f.PickRandom<Listing>(context.Listings))
                .RuleFor(c => c.User, (f, c) => f.PickRandom<User>(context.Users))
                .RuleFor(c => c.Text, (f, c) => f.Lorem.Text());

            List<Comment> comments = commentsGenerator.Generate(10);

            foreach (var comment in comments)
            {
                await context.Comments.AddAsync(comment);
            }

            await context.SaveChangesAsync();
        }
    }
}
