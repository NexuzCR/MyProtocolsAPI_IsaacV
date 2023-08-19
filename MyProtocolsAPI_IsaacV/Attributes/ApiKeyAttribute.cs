using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyProtocolsAPI_IsaacV.Attributes
{
    // esta clase ayuda a adigitar la forma en que se puede consumir un recurso de controlador (end point)
    //basicamente vamos a crear una decoracion personalizada que inyecta cierta funcionalidad ya sea a todo
    //un controlador o un point particular.

    [AttributeUsage(validOn:AttributeTargets.All)]
    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter

    {
        //especificamos cual es el clave valor dentro de appsettings que queremos usar como apikey 
private readonly string _apiKey = "PracticaApiKey";


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        { 
        // aca validamos que en el bady (en tipo json) del request vaya a la info de la ApiKey 
        //si no va a la info presentamos un mensaje de error indicando que falta ApiKey y que no se puede consumir el recurso 
        
            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var Apisalida)) 
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Llamada no contiene informacion de seguridad..."
                };
                return;

                //si no hay info de seguridad sale de la funcion y muestra este mensaje
            }

            //si viene info de seguridad falta validar que sera la correcta 
            //para esto lo primero es extraer el valor de PracticaApiKey dentro de AppSettings.json
            //para poder comparar contra lo que viene en el request
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = appSettings.GetValue<string>(_apiKey);
            //queda comparar que las apikey sean iguales
            if (!ApiKeyValue.Equals(Apisalida))
            {

                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey invalida..."
               
                };
                return;
            }
            await next();   
        }
    }       
}
