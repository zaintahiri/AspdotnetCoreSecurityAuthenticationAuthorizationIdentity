var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    //this is default page which is called if you have applied the authentication and
    // authorization then /Account/Login will be called 
    // you can change the following path with any, but /Account/Login is default no need to mention
    // you can place any options.LoginPath="/FolderName/RazorPage"
    // if you are already login then /Account/Login will not be called.
    options.LoginPath = "/Account/Login";

    // this page is for access denied if someone trying to access page and have no previleged to access 
    // then error page should be shown
    options.AccessDeniedPath = "/Account/AccessDenied";

});
// how to add policy for any page who can access the page, only authorized person
builder.Services.AddAuthorization(options =>
{
    // here we have addedd police
    // policy is the page you accessing must have claim Department and value must be HR
    // page have authorize attribute [authorize(policy="MustBelongToHRDepartment")], this is how you can achieve
    options.AddPolicy("MustBelongToHRDepartment",
        policy => policy.RequireClaim("Department", "HR"));

    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
