

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();

services.AddDependencyInjectionApplication(builder.Configuration);


var app = builder.Build();


app.UseHttpsRedirection();

app.UseRouting();

app.UseDependencyInjectionApplication(app.Environment);

app.MapControllers();


await app.RunAsync();

