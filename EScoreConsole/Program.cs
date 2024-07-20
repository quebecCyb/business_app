using EScoreConsole.Services;
using EScoreConsole.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://162.19.233.237:4040");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IChatGpt, ChatGpt>();
builder.Services.AddSingleton<IAiClient, ChatGpt>();
builder.Services.AddScoped<IExecScore, ExecScore>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Make sure authorization and CORS policies, if any, are correctly configured
app.UseAuthorization();
app.MapRazorPages();
app.UseStaticFiles(); // Enable serving static files

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  // Ensures controllers are mapped
});

app.UseCors(builder =>
    builder.WithOrigins("http://162.19.233.237:4050")
        .AllowAnyHeader()
        .AllowAnyMethod());


app.Run();