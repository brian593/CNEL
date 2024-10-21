using System;
using LaLuz.Models;

namespace LaLuz.Services;

public interface IApiServices
{
    Task<ApiResponse> GetDataCnel(string Id, string Type);
    Task<ApiResponse> GetDataCentroSur(CentroSurModel centroSurModel);

}
