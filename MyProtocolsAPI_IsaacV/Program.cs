using Microsoft.Data.SqlClient;
using MyProtocolsAPI_IsaacV.Models;
using Microsoft.EntityFrameworkCore;

namespace MyProtocolsAPI_IsaacV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //vamos a leer la etiqueta CNNSTR de appsewttings.json para configurar la conexion
            //a la base de datos 
            var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

            //elimina del CNNSTR el dato del password ya que seria muy sencillo obtener la info
            CnnStrBuilder.Password = "123456";

            //cnnStrBuilder es un objeto que permite la contruccion de cadenas de conexion a base de datos.
            //se puede modificar cada parte de la misma, para el final debemos extraer un string con la info final
            string cnnStr = CnnStrBuilder.ToString();

            builder.Services.AddDbContext<MyProtocolsDBContext>(options => options.UseSqlServer(cnnStr));
               


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}