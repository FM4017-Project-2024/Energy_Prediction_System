using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;
using System.Diagnostics;

namespace Energy_Prediction_System;

public partial class LiveSensorData : ContentPage
{
    private readonly SensorSim rndVal = new();
    private readonly DatabaseWebAPIServices _databaseWebAPIServices;

    private bool _simulate = false;
    private bool _simulationStarted = false;
    private bool _isRunning = false;

    private struct Livesensors 
    {
        public double TT01 { get; set; }
        public double TT02 { get; set; }
        public double TT03 { get; set; }
        public double TT04 { get; set; }
        public double TT05 { get; set; }
        public DateTime? TT_DT { get; set; }
        public double RHT01 { get; set; }
        public double RHT02 { get; set; }
        public double RHT03 { get; set; }
        public double RHT04 { get; set; }
        public DateTime? RHT_DT { get; set; }
        public double KWH01 { get; set; }
        public DateTime? KWH_DT { get; set; }
    }
    Livesensors sensor = new Livesensors();

    public LiveSensorData()
    {
        InitializeComponent();
        _databaseWebAPIServices = new DatabaseWebAPIServices();
        getSensorData();
    }
    private async void getSensorData()
    {
        while (_isRunning)
        {
            
            if (_simulate)
            {
                await Task.Delay(3000);
                StartSimulatingSensorData();
            }
            else
            {
                
                GetLatestBuildingTemp();
                GetLatestBuildingRelHumidity();
                GetLatestEnergyMeterData();
                UpdateGUI();
                await Task.Delay(20000);
            }
        }
    }

    private void UpdateGUI()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {

            TT01_Label.Text = $"TT01: {sensor.TT01:F2}" + " °C";
            TT02_Label.Text = $"TT02: {sensor.TT02:F2}" + " °C";
            TT03_Label.Text = $"TT03: {sensor.TT03:F2}" + " °C";
            TT04_Label.Text = $"TT04: {sensor.TT04:F2}" + " °C";
            TT05_Label.Text = $"TT05: {sensor.TT05:F2}" + " °C";
            TTDT_Label.Text = sensor.TT_DT.ToString();
            RHT01_Label.Text = $"RHT01: {sensor.RHT01:F2}" + " %";
            RHT02_Label.Text = $"RHT02: {sensor.RHT02:F2}" + " %";
            RHT03_Label.Text = $"RHT03: {sensor.RHT03:F2}" + " %";
            RHT04_Label.Text = $"RHT04: {sensor.RHT04:F2}" + " %";
            RHTDT_Label.Text = sensor.RHT_DT.ToString();
            KWH_Label.Text = $"KWH01: {sensor.KWH01:F2}" + " kWh";
            KWHDT_Label.Text = sensor.KWH_DT.ToString();
        });
    }

    private void StartSimulatingSensorData()
    {
        sensor.TT01 = rndVal.GetRandomDouble(0, 20);
        sensor.TT02 = rndVal.GetRandomDouble(0, 10);
        sensor.TT03 = rndVal.GetRandomDouble(0, 30);
        sensor.TT04 = rndVal.GetRandomDouble(0, 30);
        sensor.TT05 = rndVal.GetRandomDouble(0, 30);
        sensor.RHT01 = rndVal.GetRandomDouble(0, 30);
        sensor.RHT02 = rndVal.GetRandomDouble(0, 30);
        sensor.RHT03 = rndVal.GetRandomDouble(0, 30);
        sensor.RHT04 = rndVal.GetRandomDouble(0, 30);
        sensor.KWH01 = rndVal.GetRandomDouble(0, 30);

        UpdateGUI();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
        _simulationStarted = false;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _isRunning = true;
        if (!_simulationStarted)
        {
            _simulationStarted = true;
            getSensorData();
        }
    }

    private async void OnLabelTapped_TT01(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT01"));
    private async void OnLabelTapped_TT02(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT02"));
    private async void OnLabelTapped_TT03(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT03"));
    private async void OnLabelTapped_TT04(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT04"));
    private async void OnLabelTapped_TT05(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT05"));
    private async void OnLabelTapped_RHT01(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("RHT01"));
    private async void OnLabelTapped_RHT02(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("RHT02"));
    private async void OnLabelTapped_RHT03(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("RHT03"));
    private async void OnLabelTapped_RHT04(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("RHT04"));
    private async void OnLabelTapped_KWH01(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("KWH01"));

    // Method to retrieve temperature data
    private async void GetLatestBuildingTemp()
    {
        string apiUrl = "/api/BuildingTemperatureItems/latest";
        try
        {
            var latestTemp = await _databaseWebAPIServices.GetLatestBuildingTempAsync(apiUrl);
            sensor.TT01 = latestTemp.Temp1;
            sensor.TT02 = latestTemp.Temp2;
            sensor.TT03 = latestTemp.Temp3;
            sensor.TT04 = latestTemp.Temp4;
            sensor.TT05 = latestTemp.Temp5;
            sensor.TT_DT = latestTemp.TempDateTime;
            Debug.WriteLine($"Id: {latestTemp.Id}");
            Debug.WriteLine($"Temp1: {latestTemp.Temp1}");
            Debug.WriteLine($"Temp2: {latestTemp.Temp2}");
            Debug.WriteLine($"Temp3: {latestTemp.Temp3}");
            Debug.WriteLine($"Temp4: {latestTemp.Temp4}");
            Debug.WriteLine($"Temp5: {latestTemp.Temp5}");
            Debug.WriteLine($"Unit of Measure: {latestTemp.TempUoM}");
            Debug.WriteLine($"DateTime: {latestTemp.TempDateTime}");
        }
        catch (Exception ex)
        {
            await HandleError(ex, "Failed to fetch temperature data");
        }
    }

    private async void GetLatestBuildingRelHumidity()
    {
        string apiUrl = "/api/BuildingRelativeHumidityItems/latest";
        try
        {
            var latestHumidity = await _databaseWebAPIServices.GetLatestBuildingRelHumidityAsync(apiUrl);
            sensor.RHT01 = latestHumidity.RelHumidity1;
            sensor.RHT02 = latestHumidity.RelHumidity2;
            sensor.RHT03 = latestHumidity.RelHumidity3;
            sensor.RHT04 = latestHumidity.RelHumidity4;
            sensor.RHT_DT = latestHumidity.RelHumidityDateTime;
            Debug.WriteLine($"Id: {latestHumidity.Id}");
            Debug.WriteLine($"RelHumidity1: {latestHumidity.RelHumidity1}");
            Debug.WriteLine($"RelHumidity2: {latestHumidity.RelHumidity2}");
            Debug.WriteLine($"RelHumidity3: {latestHumidity.RelHumidity3}");
            Debug.WriteLine($"RelHumidity4: {latestHumidity.RelHumidity4}");
            Debug.WriteLine($"Unit of Measure: {latestHumidity.RelHumidityUoM}");
            Debug.WriteLine($"DateTime: {latestHumidity.RelHumidityDateTime}");
        }
        catch (Exception ex)
        {
            await HandleError(ex, "Failed to fetch latest humidity");
        }
    }

    private async void GetLatestEnergyMeterData()
    {
        string apiUrl = "/api/BuildingEnergyMeterItems/latest";
        try
        {
            var latestEnergyMeter = await _databaseWebAPIServices.GetLatestBuildingEnergyMeterAsync(apiUrl);
            sensor.KWH01 = latestEnergyMeter.EnergyMeter1;
            sensor.KWH_DT = latestEnergyMeter.EnergyMeterDateTime;
            Debug.WriteLine($"Id: {latestEnergyMeter.Id}");
            Debug.WriteLine($"EnergyMeter1: {latestEnergyMeter.EnergyMeter1}");
            Debug.WriteLine($"Unit of Measure: {latestEnergyMeter.EnergyMeterUoM}");
            Debug.WriteLine($"DateTime: {latestEnergyMeter.EnergyMeterDateTime}");
        }
        catch (Exception ex)
        {
            await HandleError(ex, "Failed to fetch latest energy meter data");
        }
    }

    // Error handling method
    private async Task HandleError(Exception ex, string message)
    {
        await DisplayAlert("Error", $"{message}: {ex.Message}", "OK");
    }

}