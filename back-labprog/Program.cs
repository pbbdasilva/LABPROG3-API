using back_labprog.Business;

var builder = WebApplication.CreateBuilder(args);

var policy = "CustomPolicy";
// Add services to the container.
builder.Services.AddScoped<IDiseaseBO, DiseaseBO>();
builder.Services.AddScoped<IUserBO, UserBO>();
builder.Services.AddScoped<IMapBO, MapBO>();
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy(name: policy, policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); }));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();