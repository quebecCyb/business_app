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
    
    options.AddPolicy("AllowSpecificOrigins",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://162.19.233.237:4041")
                .AllowAnyMethod()
                .AllowAnyHeader();
            
            policyBuilder.WithOrigins("https://exec.fordka.info")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

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


// app.UseCors("AllowAllOrigins");

app.Run();