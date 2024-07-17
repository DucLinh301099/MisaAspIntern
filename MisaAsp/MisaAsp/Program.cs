
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MisaAsp.Middleware;
using MisaAsp.Models.Ulti;
using MisaAsp.Repositories;
using MisaAsp.Services;
using Npgsql;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// Cấu hình Domain tới FE
var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .WithOrigins(corsOrigins) // Thay thế bằng nguồn gốc của frontend
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Cho phép gửi cookie
    });
});

// Đăng ký các dịch vụ cần thiết
#region Đăng kí service

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountantService, AccountantService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IBankAccountService, BankAccountService>();
#endregion

#region Đăng kí Repo
builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountantRepository, AccountantRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();

builder.Services.AddScoped<ResOutput>();
#endregion

//Thiết lập cơ chế xác thực và các tham số cần thiết để xác thực JWT.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new NpgsqlConnection(connectionString);
});

var app = builder.Build();

// Cấu hình pipeline xử lý yêu cầu HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Kích hoạt middleware để phục vụ Swagger được sinh ra như là một endpoint JSON.
    app.UseSwagger();
    // Kích hoạt middleware để phục vụ swagger-ui (HTML, JS, CSS, v.v.),
    // chỉ định endpoint JSON của Swagger.
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MisaAsp v1"));
}

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseMiddleware<TokenMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
