using myfinance_web_netcore.Domain;
using myfinance_web_netcore.Infrastructure;
using myfinance_web_netcore.Mappers;
using myfinance_web_netcore.Services;

var builder = WebApplication.CreateBuilder(args); //@ a classe program, tem como função criar o serviço web (um host)

// Add services to the container.
builder.Services.AddControllersWithViews(); //@ informa que vamos utilizar controladores com views (padrão MVC)
builder.Services.AddDbContext<MyFinanceDbContext>();  //@ Informamos a aplicação qual a classe realiza a conexão com o DB
builder.Services.AddAutoMapper(typeof(PlanoContaMap));
builder.Services.AddAutoMapper(typeof(TransacaoMap));

//Services
builder.Services.AddTransient<IPlanoContaService, PlanoContaService>(); //* <Primeiro a interface depois a classe>
builder.Services.AddTransient<ITransacaoService, TransacaoService>();

//UseCases

//Repositories

//Aplications


var app = builder.Build();  //$ Builder é um objeto criacional que te ajuda um objeto complexo seguindo um passo a passo (Builder é um design partenes)

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); //@ informa que sua aplicação pode acessar arquivos do tipo CSS, JS, Imagens ... desde que estejam na pasta "wwwroot"

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); //! define o padrão de roteamento, sendo a default: "home / index / id" ( esse ultimo é opcional, se não passar o programa roda normal) 

app.Run(); //@ ela define tudo que tem que ser feito e no final retorna um app.Run
