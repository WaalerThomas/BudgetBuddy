using BudgetBuddy.Account;
using BudgetBuddy.Account.AutoMapper;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddAccount();
    
    builder.Services
        .AddAutoMapper(typeof(AccountProfile));
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");
    app.MapControllers();
    app.UseHttpsRedirection();
    app.Run();
}
