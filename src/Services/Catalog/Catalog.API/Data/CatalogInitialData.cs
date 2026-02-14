namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
        {
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S24 Ultra",
                Description = "Flagship Samsung smartphone with advanced camera system and S-Pen support.",
                ImageUrl = "samsung_galaxy_s24_ultra.png",
                Price = 1499.99M,
                Categories = new List<string> { "Smart Phone", "Samsung" }
            },

            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "iPhone 15 Pro Max",
                Description = "Apple’s premium smartphone featuring A17 chip and titanium design.",
                ImageUrl = "iphone_15_pro_max.png",
                Price = 1599.99M,
                Categories = new List<string> { "Smart Phone", "Apple" }
            },

            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 8 Pro",
                Description = "Google flagship phone with AI-powered photography features.",
                ImageUrl = "google_pixel_8_pro.png",
                Price = 1199.99M,
                Categories = new List<string> { "Smart Phone", "Google" }
            },

            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Sony WH-1000XM5",
                Description = "Industry-leading noise canceling wireless headphones.",
                ImageUrl = "sony_wh1000xm5.png",
                Price = 399.99M,
                Categories = new List<string> { "Audio", "Sony" }
            },

            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Apple MacBook Pro 14 M3",
                Description = "Powerful laptop with Apple M3 chip for professionals.",
                ImageUrl = "macbook_pro_14_m3.png",
                Price = 2499.99M,
                Categories = new List<string> { "Laptop", "Apple" }
            },

            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Dell XPS 15",
                Description = "High-performance Windows laptop with stunning InfinityEdge display.",
                ImageUrl = "dell_xps_15.png",
                Price = 2199.99M,
                Categories = new List<string> { "Laptop", "Dell" }
            },

            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "iPad Pro 12.9",
                Description = "Apple tablet with M2 chip and Liquid Retina XDR display.",
                ImageUrl = "ipad_pro_12_9.png",
                Price = 1399.99M,
                Categories = new List<string> { "Tablet", "Apple" }
            },

            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy Tab S9",
                Description = "Premium Android tablet with AMOLED display and S-Pen.",
                ImageUrl = "galaxy_tab_s9.png",
                Price = 1099.99M,
                Categories = new List<string> { "Tablet", "Samsung" }
            },

            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Logitech MX Master 3S",
                Description = "Advanced wireless mouse for productivity and creative workflows.",
                ImageUrl = "logitech_mx_master_3s.png",
                Price = 129.99M,
                Categories = new List<string> { "Accessories", "Logitech" }
            },

            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "ASUS ROG Strix G16",
                Description = "Gaming laptop powered by Intel Core i9 and RTX graphics.",
                ImageUrl = "asus_rog_strix_g16.png",
                Price = 1999.99M,
                Categories = new List<string> { "Laptop", "Gaming" }
            }
        };
}
