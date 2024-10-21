using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LaLuz.DataAccess;
using LaLuz.Models;
using LaLuz.Services;
using LaLuz.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LaLuz.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    #region Variables
    private readonly IApiServices _apiServices;
    private readonly DWJDBContext _dbContext;

    private Dictionary<string, string> Types { get; } = new Dictionary<string, string>
    {
        { "CUENTA_CONTRATO", "Cuenta contrato" },
        { "CUEN", "Código único (cuen)" },
        { "IDENTIFICACION", "Cédula de identidad" }
    };

    public ObservableCollection<string> DisplayTypes { get; }
    #endregion

    #region Propiedades
    [ObservableProperty]
    private ApiResponse cnelData;

    [ObservableProperty]
    private bool isSave;

    [ObservableProperty]
    private bool isCentroSur;

    [ObservableProperty]
    private string idInput;

    [ObservableProperty]
    private string selectedType;

    [ObservableProperty]
    private ObservableCollection<DetallePlanificacion> detallesPlanificaciones;

    [ObservableProperty]
    private string cuentaContrato;
    [ObservableProperty]
    private string direccion;
    [ObservableProperty]
    private string fechaRegistro;
    [ObservableProperty]
    private string colorONOFF;


    public ICommand SubmitAsyncCommand { get; }

    #endregion

    #region CONSTRUCTOR
    public MainPageViewModel(IApiServices apiServices, DWJDBContext context)
    {
        _apiServices = apiServices;
        _dbContext = context;

        DisplayTypes = new ObservableCollection<string>(Types.Values);
        selectedType = Types["CUENTA_CONTRATO"]; // Valor por defecto

        SubmitAsyncCommand = new AsyncRelayCommand(SubmitAsync);
        MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

        ColorONOFF = "#666666";

    }
    #endregion
    public async Task Obtener()
    {
        var dataWhitJson = await _dbContext.DataWhitJsons.FirstOrDefaultAsync();
        if (dataWhitJson != null)
        {
            CnelData = JsonConvert.DeserializeObject<ApiResponse>(dataWhitJson.JsonData);
            SelectedType = Types[dataWhitJson.TypeCNEL];
            IdInput = dataWhitJson.IdCNEL;
            LlenarDatos(CnelData);

        }
    }




    public async Task SubmitAsync()
    {
        try
        {
            var apiValue = GetSelectedApiValue();
            var valido=true;
            if(apiValue== "IDENTIFICACION")
            {
            valido=VerifyID.VerificaIdentificacion(IdInput);
            }
            if(valido)  
            {
                if (!IsCentroSur)
                {            
                    CnelData = await _apiServices.GetDataCnel(IdInput, apiValue);
                }
                else
                {
                    if (apiValue=="CUENTA_CONTRATO")
                    {
                        apiValue="CCO";
                    }
                    else if (apiValue=="IDENTIFICACION")
                    {
                        apiValue="CED";
                    }
                    else if(apiValue=="CUEN")
                    {
                        apiValue="CUE";
                    }
                    
                    var DataModel=new CentroSurModel{
                        iTipoConsulta=apiValue,
                        iValorConsulta=idInput
                    };
                    CnelData = await _apiServices.GetDataCentroSur(DataModel);
                }

            LlenarDatos(CnelData);
            ColorONOFF = "#a8d8ff";

            if (IsSave)
            {
                await Task.Run(async () =>
                {
                    var isClear = await _dbContext.ClearDataWhitJsonsTableAsync();

                    var dataWhitJson = new DataWhitJson
                    {
                        IdCNEL = idInput,
                        TypeCNEL = apiValue,
                        JsonData = CnelData.jsonData,
                        SaveDate = DateTime.Now,
                    };
                    _dbContext.DataWhitJsons.Add(dataWhitJson);
                    await _dbContext.SaveChangesAsync();

                });
            }
            }
            else
            {
               await DisplayAlert("Error","La identificacion no es validad","Cancelar");
            }
        }
        catch (System.Exception ex)
        {
            await DisplayAlert("Error",$"Experimentamos un Error: {ex.Message}","Ok");
        }
    }

    private void LlenarDatos(ApiResponse CnelData)
    {
        if (CnelData?.notificaciones != null && CnelData.notificaciones.Count > 0 && CnelData.notificaciones[0].detalleplanificacion != null)
        {
            var detallePlanificacions = CnelData.notificaciones[0].detalleplanificacion;
            DetallesPlanificaciones = new ObservableCollection<DetallePlanificacion>(detallePlanificacions);
        }
        else
        {
            DetallesPlanificaciones = new ObservableCollection<DetallePlanificacion>();
        }
        CuentaContrato = CnelData.notificaciones[0].cuentaContrato;
        FechaRegistro = CnelData.notificaciones[0].fechaRegistro;
        Direccion = CnelData.notificaciones[0].direccion;

        IsVisible = true;
        Debug.WriteLine(CnelData.notificaciones.Count());
    }

    private string GetSelectedApiValue()
    {
        foreach (var type in Types)
        {
            if (type.Value == SelectedType)
            {
                return type.Key;
            }
        }
        return "CUENTA_CONTRATO"; 
    }
}
