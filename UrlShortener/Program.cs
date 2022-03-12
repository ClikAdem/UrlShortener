using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.RepositoryServices;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUrlService, UrlService>();

string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UrlDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//app.UseExceptionHandler(handler =>
//{
//    handler.Run(async context =>
//    {
//        var statusCode = HttpStatusCode.InternalServerError;

//        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

//        if (exceptionHandlerPathFeature == null)
//            return;

//        var detail = exceptionHandlerPathFeature.Message;

//        switch (exceptionHandlerPathFeature)
//        {
//            case BusinessException businessException:
//                detail = businessException.Message;
//                statusCode = HttpStatusCode.BadRequest;
//                break;
//        }

//        UrlShortener.Middleware.ApplicationBuilderExtensions.GenerateErrorResponse(context, detail, (int)statusCode);
//    });
//});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
