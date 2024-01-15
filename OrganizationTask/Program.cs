using OrganizationTask.Postgres;
using OrganizationTask.Postgres.DI;
using OrganizationTask.WepApiLib.DI;
using OrganizationTask.WepApiLib.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ����� ���������� � �������
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	// ��������� ����������� ������� ������
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

	options.AddWebApiLibComments();
});

// WebApiLib
builder.Services.AddWebApiLib();

// DB
builder.Services.AddPersistenceNpgDb(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// �������� ����������
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Services.InitContextAndData();

app.Run();
