using Microsoft.EntityFrameworkCore;
using PatrickGod.SuperHero;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HeroContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Welcome to the first minimal API");

app.MapGet("/heroes", async (HeroContext db) => {
    return await GetAllHeroesAsync(db);
});

app.MapGet("/heroes/{id:int}", async (HeroContext db, int id) => {
    var hero = await db.Heroes.FindAsync(id);
    return hero == null ? Results.NotFound("Sorry, hero not found") : Results.Ok(hero);
});

app.MapPost("/heroes", async (HeroContext db, Hero hero) => {
    db.Heroes.Add(hero);
    await db.SaveChangesAsync();
    return Results.Ok(await GetAllHeroesAsync(db));
});

app.MapPut("/heroes/{id:int}", async (HeroContext db, int id, Hero hero) => {
    var dbHero = await db.Heroes.FindAsync(id);
    if (dbHero == null)
        return Results.NotFound("No hero found");

    dbHero.FirstName = hero.FirstName;
    dbHero.LastName = hero.LastName;
    dbHero.HeroName = hero.HeroName;

    await db.SaveChangesAsync();
    return Results.Ok(await GetAllHeroesAsync(db));
});

app.MapDelete("/heroes/{id:int}", async (HeroContext db, int id) => {
    var hero = await db.Heroes.FindAsync(id);
    if (hero == null)
        return Results.NotFound("No hero found");

    db.Heroes.Remove(hero);
    await db.SaveChangesAsync();
    
    return Results.Ok(await GetAllHeroesAsync(db));
});

app.Run();

async Task<List<Hero>> GetAllHeroesAsync(HeroContext db) 
    => await db.Heroes.ToListAsync();