using Microsoft.AspNetCore.Authentication.Cookies;
using Server.ConsoleLogger;
using Server.Global.Storage;
using Server.Logic.Auth;
using Server.Logic.Auth.Contracts.UserStorage;
using Server.Logic.Auth.Models;
using Server.Logic.Books;
using Server.Logic.Books.Contracts.BookStorage;
using ILogger = Server.Global.Logger.ILogger;

ConsoleLogger logger = new ConsoleLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ILogger, ConsoleLogger>();
builder.Services.AddSingleton<IUserStorage, GlobalStorage>();
builder.Services.AddSingleton<IBookStorage, GlobalStorage>();

builder.Services.AddTransient<AuthLogicProvider, AuthLogicProvider>();
builder.Services.AddTransient<BooksLogicProvider, BooksLogicProvider>();

builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;}).AddCookie();
builder.Services.AddAuthorization();


builder.Services.AddControllers();

var app = builder.Build();

var userStorage = app.Services.GetService<IUserStorage>();

userStorage.CreateUser(new User("root", "rootPassword", BasicRoles.AdminRole));

app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();