var builder = WebApplication.CreateBuilder(args);

// Razor Pages ve HttpClient ekle
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<MongoDBService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();