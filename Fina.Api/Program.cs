using Fina.Api;
using Fina.Api.Api.EndPoints;
using Fina.Api.Common.Api;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContext();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();
app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndPoints();
app.Run();
