using FinalApi.Models;
using Microsoft.OpenApi.Models;
using Quartz;
using FinalApi.Jobs;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using FinalApi.FilterHeader;
using Serilog;
using FinalApi.Services.Repository;
using FinalApi.Services.Impl;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<projectDemoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("value")));
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
    new HeaderApiVersionReader("x-version"));
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop Orders", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Shop Orders API v2", Version = "v2" });

});
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("CreateCustomerJob");
    var triggerKey = new TriggerKey("CustomerJobTrigger");
    q.AddJob<CreateCustomerVipJob>(j => 
    j.WithIdentity(jobKey));
    q.AddTrigger(t => t
    .WithIdentity(triggerKey)
    .ForJob(jobKey)
    .StartNow()
    .WithSimpleSchedule(s => s.WithIntervalInSeconds(5)
    .RepeatForever())
    );
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICustomerService,CustomerService>(); 
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<SecretKeyFilter>();
var _logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration).
Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog(_logger);

var app = builder.Build();
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
        });
    }
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();

