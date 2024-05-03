using Movie.Services;

var builder = WebApplication.CreateBuilder(args);
var authToken = builder.Configuration["Movie:AuthToken"];
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IMovieSearchService, MovieSearchService>();

builder.Services.AddHttpClient("TMDB",httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
