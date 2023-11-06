using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repair.Api.Configuration;
using Repair.Api.Filters;
using Repair.Core.Helper.Jwt;
using Repair.EntityFrameworkCore;
using Repair.Repository.BusRepository;
using Repair.Service.SysService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddScoped<GlobalExceptionFilter>();
builder.Services.AddControllers(opt =>
{
    //opt.Filters.AddService<GlobalExceptionFilter>();
});
var jwtSection = builder.Configuration.GetSection("TokenManagement");
builder.Services.Configure<TokenManagement>(jwtSection);

#region webapi��Ŀ����
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("strConm"));
});
// ����
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
//Jwt��Ȩ����
TokenManagement tokenManagement = jwtSection.Get<TokenManagement>();
builder.Services.AddAuthorization(options =>
{
    
});
builder.Services.AddAuthentication(options =>
    {
        //core�Դ��ٷ�JWT��֤
        // ����Bearer��֤
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,    //�Ƿ���֤SecurityKey
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tokenManagement.Secret)),    //�����keyҪ���м���
            //��֤������Issuer
            ValidateIssuer = true,
            ValidIssuer = tokenManagement.Issuer,   //Token�䷢����
            //��֤������
            ValidateAudience = true,
            ValidAudience = tokenManagement.Audience,   //�䷢��˭   
            ValidateLifetime = true,    //��֤token����ʱ��
            ClockSkew = TimeSpan.FromSeconds(30),    //ʱ�ӻ�����λ��
            RequireExpirationTime = true,   //token��Ҫ���ù���ʱ��
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                // ������ڣ����<�Ƿ����>��ӵ�������ͷ��Ϣ��
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };
    });
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//���swagger
builder.Services.AddSwaggerGen();
builder.Services.ConfigSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IRepairOrderRepository, RepairOrderRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IRepairOrderService, RepairOrderService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<ICommentService, CommentService>();


builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.Run();

