using Microsoft.Extensions.Hosting;

try
{

    var builder = WebApplication.CreateBuilder(args);

    var services = builder.Services;

    services.AddControllers();

    services.AddDependencyInjectionApplication(builder.Configuration);


    var app = builder.Build();

    Console.WriteLine($"\nEnvironment : {app.Environment.EnvironmentName}\n");

    if (app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    app.UseRouting();

    await app.UseDependencyInjectionApplication(app.Environment);

    app.MapControllers();


    await app.RunAsync();

}
catch (ArgumentOutOfRangeException exOut)
{
    var dontPrint = exOut;
}
catch (Exception ex)
{
    Console.WriteLine($"Error : {ex.Message}");
}
