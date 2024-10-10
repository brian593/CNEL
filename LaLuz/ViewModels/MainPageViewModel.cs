using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LaLuz.DataAccess;
using LaLuz.Models;
using LaLuz.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LaLuz.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    #region Variables
    private readonly IApiServices _apiServices;
    private readonly DWJDBContext _dbContext;


    // Diccionario para mapear Criterio a sus valores y nombres

    // Diccionario para mapear los valores API a nombres de visualización
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
    public System.Windows.Input.ICommand SubmitAsyncCommand { get; }
    public System.Windows.Input.ICommand ShareItemCommand { get; }

    #endregion

    #region CONSTRUCTOR
    public MainPageViewModel(IApiServices apiServices, DWJDBContext context)
    {
        _apiServices = apiServices;
        _dbContext = context;

        DisplayTypes = new ObservableCollection<string>(Types.Values);
        selectedType = Types["CUENTA_CONTRATO"]; // Valor por defecto

        SubmitAsyncCommand = new AsyncRelayCommand(SubmitAsync);
        ShareItemCommand = new AsyncRelayCommand(ShareItem);

        MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

        // Task.Run(async () => await LoadDataFromDatabaseAsync());

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
        // Obtener el valor de la API basado en el displayName seleccionado
        var apiValue = GetSelectedApiValue();

        CnelData = await _apiServices.GetDataCnel(IdInput, apiValue);
        LlenarDatos(CnelData);


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
        // Aquí llamas a tu API y le envías apiValue y numericInput
    }


    private async Task ShareItem()
    {
        await DisplayAlert("Toco el boton", "nmms", "se");
        //if (item != null)
        //{
        //    await Share.Default.RequestAsync(new ShareTextRequest
        //    {
        //        Text = $"El dia : {item.fechaCorte} se realizaran cortes desde: {item.horaDesde} hasta {item.horaHasta}",
        //        Title = "Share Item"
        //    });
        //}
    }

    private void LlenarDatos(ApiResponse CnelData)
    {
        // Asegúrate de que la propiedad 'notificaciones' y 'detallesPlanificaciones' no sean nulas.
        if (CnelData?.notificaciones != null && CnelData.notificaciones.Count > 0 && CnelData.notificaciones[0].detalleplanificacion != null)
        {
            var detallePlanificacions = CnelData.notificaciones[0].detalleplanificacion;
            DetallesPlanificaciones = new ObservableCollection<DetallePlanificacion>(detallePlanificacions);


        }
        else
        {
            // Si la lista es nula, crea una ObservableCollection vacía
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
        // Buscar el valor del API basado en el nombre seleccionado
        foreach (var type in Types)
        {
            if (type.Value == SelectedType)
            {
                return type.Key;
            }
        }
        return "CUENTA_CONTRATO"; // Valor por defecto si no se encuentra
    }
}
