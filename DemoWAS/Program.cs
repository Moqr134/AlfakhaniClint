using DemoWAS;
using DemoWAS.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSingleton<CartService>();
builder.Services.AddSingleton<UserInfoService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IBillsService, BillsService>();
builder.Services.AddScoped<INotifSrvice, NotifSrvice>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://api.alfakhani.com/")
        //BaseAddress = new Uri("https://localhost:7292/")
    });
await builder.Build().RunAsync();
