using SearchApp.Services;
using SearchApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPrepareDataSourceService, PrepareDataSourceService>();
builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddCors(p => p.AddPolicy("clientapp", builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

var prepareDataSource = app.Services.GetRequiredService<IPrepareDataSourceService>();
(await prepareDataSource.Prepare("DataSource.json")).FlattenDataSource();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.UseCors("clientapp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
