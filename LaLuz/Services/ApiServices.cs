using System;
using System.Net;
using System.Text;
using AutoMapper;
using LaLuz.DataAccess;
using LaLuz.Models;
using Newtonsoft.Json;

namespace LaLuz.Services;

public class ApiServices : IApiServices
{
    string _endpoint="";
    private readonly IMapper _mapper;

    public ApiServices(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<ApiResponse> GetDataCnel(string Id, string Type)
    {
        try
            {
                _endpoint =$"https://api.cnelep.gob.ec/servicios-linea/v1/notificaciones/consultar/{Id}/{Type}";
                var httpClient = new HttpClient();
                var httpMethod = HttpMethod.Get;

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(_endpoint),
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

public async Task <ApiResponse> GetDataCentroSur(CentroSurModel centroSurModel)
{
    try
    {
                        
                _endpoint = "https://nest.centrosur.gob.ec";//Este usa metodo POST

                var request = JsonConvert.SerializeObject(centroSurModel);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(_endpoint);
                var url = "/api/v2/portal/get-cortes";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                   return new ApiResponse()
                    {
                        resp="No fue posible conectarse",
                        mensaje="No fue posible conectarse {ex.Message}",
                        notificaciones=null,
                        mensajeError=null
                    } ;
                }

                var result = await response.Content.ReadAsStringAsync();
                var resultObject = JsonConvert.DeserializeObject<CentroSur>(result);

                // Suponiendo que tienes un objeto del segundo modelo
                // Método que retorna los datos del segundo JSON
                var notificaciones = _mapper.Map<List<Notificacion>>(resultObject.data.Cortes);

                    return new ApiResponse()
                    {
                        resp="OK",
                        mensaje="Ok",
                        notificaciones=notificaciones,
                        mensajeError=null
                    } ;
        
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
