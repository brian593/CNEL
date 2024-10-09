using System;
using System.Net;
using LaLuz.Models;
using Newtonsoft.Json;

namespace LaLuz.Services;

public class ApiServices : IApiServices
{
    public async Task<ApiResponse> GetDataCnel(string Id, string Type)
    {
        try
            {
                string  endpoint = $"https://api.cnelep.gob.ec/servicios-linea/v1/notificaciones/consultar/{Id}/{Type}";
                var httpClient = new HttpClient();
                var httpMethod = HttpMethod.Get;

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(endpoint),
                    Method = httpMethod
                };
                //request.Headers.Add("Authorization", Token);

                var httpResponse = await httpClient.SendAsync(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    var resultObject= JsonConvert.DeserializeObject<ApiResponse>(result);
                    resultObject.jsonData = result;
                return resultObject;
                    
                }
                else
                {
                    return new ApiResponse()
                    {
                        resp="No fue posible obtener alguna respuesta con esos datos",
                        mensaje="No fue posible obtener alguna respuesta con esos datos",
                        notificaciones=null,
                        mensajeError=null
                    } ;
                }

            }
            catch (Exception ex)
            {
                 return new ApiResponse()
                    {
                        resp="No fue posible conectarse",
                        mensaje="No fue posible conectarse {ex.Message}",
                        notificaciones=null,
                        mensajeError=null
                    } ;

            }    
            
    }
}
