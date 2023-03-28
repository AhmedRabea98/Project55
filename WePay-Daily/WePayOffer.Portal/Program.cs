using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SubmitOrderRef;
using WePayOffer.BL.Helper;
using WePayOffer.BL.Interface;
using WePayOffer.BL.Mapper;
using WePayOffer.BL.Repository;
using WePayOffer.DAL.Database;
using WePayOffer.DAL.Extend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region Connection String

var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(connectionString));

#endregion


#region Auto Mapper

builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

#endregion


#region Generic Repository Configuration
builder.Services.AddScoped(typeof(IGenericRep<>), typeof(GenericRep<>));
builder.Services.AddScoped<ProductOffer, ProductOffer>();
#endregion


#region Unit of work Configuration

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

#endregion


#region Services
builder.Services.AddScoped<FileUploader, FileUploader>();
#endregion



#region Identity Configuration


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });


builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);



// Password and user name configuration

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Default Password settings.

    options.User.RequireUniqueEmail = true;

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<ApplicationContext>();

#endregion

var app = builder.Build();


#region Auto Db Migration

//Automatic Migration
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var dbcontext = services.GetRequiredService<ApplicationContext>();
    dbcontext.Database.Migrate();

}

#endregion




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Account}/{action=Login}/{id?}");
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
