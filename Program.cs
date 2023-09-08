using CarBuilderAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
}

app.UseHttpsRedirection();

List<PaintColor> paintColors = new List<PaintColor>()
{
    new PaintColor()
    {
        Id = 1,
        Price = 500.00M,
        Color = "Silver"
    },
    new PaintColor()
    {
        Id = 2,
        Price = 600.00M,
        Color = "Midnight Blue"
    },
    new PaintColor()
    {
        Id = 3,
        Price = 800.00M,
        Color = "Firebrick Red"
    },
    new PaintColor()
    {
        Id = 4,
        Price = 700.00M,
        Color = "Spring Green"
    }
};

List<Interior> interiors = new List<Interior>()
{
    new Interior()
    {
        Id = 1,
        Price = 300.00M,
        Material = "Beige Fabric"
    },
    new Interior()
    {
        Id = 2,
        Price = 450.00M,
        Material = "Charcoal Fabric"
    },
    new Interior()
    {
        Id = 3,
        Price = 400.00M,
        Material = "White Leather"
    },
    new Interior()
    {
        Id = 4,
        Price = 500.00M,
        Material = "Black Leather"
    }
};

List<Technology> technologies = new List<Technology>()
{
    new Technology()
    {
        Id = 1,
        Price = 1000.00M,
        Package = "Basic Package",
    },
    new Technology()
    {
        Id = 2,
        Price = 1500.00M,
        Package = "Navigation Package",
    },
    new Technology()
    {
        Id = 3,
        Price = 2000.00M,
        Package = "Visibility Package",
    },
    new Technology()
    {
        Id = 4,
        Price = 2500.00M,
        Package = "Ultra Package",
    }
};

List<Wheels> wheels = new List<Wheels>()
{
    new Wheels()
    {
        Id = 1,
        Price = 400.00M,
        Style = "17-inch Pair Radial"
    },
    new Wheels()
    {
        Id = 2,
        Price = 550.00M,
        Style = "17-inch Pair Radial Black"
    },
    new Wheels()
    {
        Id = 3,
        Price = 650.00M,
        Style = "18-inch Pair Spoke Silver"
    },
    new Wheels()
    {
        Id = 4,
        Price = 750.00M,
        Style = "18-inch Pair Spoke Black"
    }
};

List<Order> orders = new List<Order>()
{
    new Order()
    {
        Id = 1,
        WheelId = 4,
        InteriorId = 3,
        TechnologyId = 3,
        PaintColorId = 2,
        IsComplete = true
    },
    new Order()
    {
        Id = 2,
        WheelId = 3,
        InteriorId = 2,
        TechnologyId = 3,
        PaintColorId = 4,
        IsComplete = false
    }
};

// Wheels
app.MapGet("/api/wheels", () =>
{
    return wheels;
});

// Technology
app.MapGet("/api/technologies", () =>
{
    return technologies;
});

// Interior
app.MapGet("/api/interiors", () =>
{
    return interiors;
});

// PaintColor
app.MapGet("/api/paintcolors", () =>
{
    return paintColors;
});

// Order
// Get all orders with corresponding data
app.MapGet("/api/orders", () =>
{
    List<Order> incompleteOrders = orders.Where(o => o.IsComplete == false).ToList();

    foreach (Order order in incompleteOrders)
    {
        order.Wheels = wheels.FirstOrDefault(w => w.Id == order.WheelId);
        order.Technology = technologies.FirstOrDefault(t => t.Id == order.TechnologyId);
        order.Interior = interiors.FirstOrDefault(i => i.Id == order.InteriorId);
        order.PaintColor = paintColors.FirstOrDefault(pc => pc.Id == order.PaintColorId);
    }

    return incompleteOrders;
});

// Add Order, create date and Id
app.MapPost("/api/orders", (Order order) =>
{
    order.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1;
    order.Timestamp = DateTime.Now;
    orders.Add(order);
    return order;
});

// Fulfill an order
app.MapPost("/api/orders/{id}/fulfill", (int id) =>
{
    Order order = orders.FirstOrDefault(o => o.Id == id);
    order.IsComplete = true;
    return order;
});

app.Run();