var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Mailjet Service
builder.Services.AddSingleton<IMailService>(new MailjetService(
    builder.Configuration["Mailjet:ApiKey"],
    builder.Configuration["Mailjet:ApiSecret"],
    builder.Configuration["Mailjet:FromEmail"],
    builder.Configuration["Mailjet:FromName"]
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
