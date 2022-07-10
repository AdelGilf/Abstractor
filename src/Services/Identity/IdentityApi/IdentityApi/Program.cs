using Core.Validators;
using FluentValidation;
using IdentityApi.Configuration;
using Core;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwagger();

builder.Services.AddDataBase(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

builder.Services.AddCore(builder.Configuration);

builder.Services.AddAutoMapper(typeof(IdentityProfile));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
