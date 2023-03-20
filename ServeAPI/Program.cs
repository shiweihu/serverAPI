using ServeAPI.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AspNetCore.Firebase.Authentication.Extensions;
using ServeAPI.Paypal;
using Microsoft.AspNetCore.Builder;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";





var builder = WebApplication.CreateBuilder(args);

//var configuration = builder.Configuration;

//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    var key = configuration["Authentication:Google:ClientId"];
//    var secret = configuration["Authentication:Google:ClientSecret"];
//    googleOptions.ClientId = key!;
//    googleOptions.ClientSecret = secret!;
//    googleOptions.SaveTokens = true;

//});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options =>
//        {
//            options.IncludeErrorDetails = true;
//            options.Authority = "https://securetoken.google.com/webapi-69a16";
//            options.TokenValidationParameters = new TokenValidationParameters{
//                ValidateIssuer = true,
//                ValidIssuer = "https://securetoken.google.com/webapi-69a16",
//                ValidateAudience = true,
//                ValidAudience = "webapi-69a16",
//                ValidateLifetime = true
//            };
//        });

var FirebaseAuthentication_Issuer = "https://securetoken.google.com/webapi-69a16";
var FirebaseAuthentication_Audience = "webapi-69a16";
builder.Services.AddFirebaseAuthentication(FirebaseAuthentication_Issuer,
                                   FirebaseAuthentication_Audience);

builder.Services.AddCors(options =>
{
    
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").WithOrigins("http://10.0.2.2:4200").AllowAnyHeader()
                                .AllowAnyMethod().AllowCredentials();
                      });
});

builder.Services.AddHttpClient<PaypalService>();
builder.Services.AddSingleton<PaypalService>();




// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else {
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
    RequestPath = "/StaticFiles"
});




app.UseCors(MyAllowSpecificOrigins);


app.UseAuthentication();
app.UseAuthorization();

app.Run();





