using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using NextDevAsp.Api;
using NextDevAsp.Api.DataContext;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TeamDbContext>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDbConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => 
{
   opt.SwaggerDoc("v1", new OpenApiInfo{
        Title = "DANKASSAWA NEXTDEV API",
        Version = "v1",
        Description = "CRUD API FOR NEXTDEV TEAMS",
        Contact = new OpenApiContact
        {
              Name = "DANKASSAWA TEAM",
              Email = "l.akalete20@gmail.com" 
        }
   });
});


//builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("CorsPolicy");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "DANKASSAWA NextDEv API";
        //options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.RoutePrefix = "swagger";
    });
};

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.MapGet("/", async (TeamDbContext _dbContext) =>
{
    var dataFromDb = await _dbContext.TeamMembers.ToListAsync();
    return Results.Ok(dataFromDb);
});

app.MapGet("/{id:int}", async (int id, TeamDbContext _dbContext) =>
{
    if (id <= 0)
    {
        return Results.Problem(title: "Index out of range");
    }
    var data = await _dbContext.TeamMembers.FirstOrDefaultAsync(t => t.Id == id);
    return Results.Ok(data);
});

app.MapPost("/", async (TeamDbContext _dbContext, TeamMember teamMember) =>
{
    _dbContext.TeamMembers.Add(teamMember);
    await _dbContext.SaveChangesAsync();
    return Results.Ok(teamMember);
});

app.MapDelete("/{id:int}", async (int id, TeamDbContext _dbContext) =>
{
    if (await _dbContext.TeamMembers.FindAsync(id) is TeamMember teamMember)
    {
        _dbContext.TeamMembers.Remove(teamMember);
        await _dbContext.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});


app.Run();

