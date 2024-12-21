using MercySocial.Application;
using MercySocial.Infrastructure;
using MercySocial.Presentation;
using MercySocial.Presentation.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation(builder.Configuration);

var app = builder.Build();

app.Configure();

app.Run();
