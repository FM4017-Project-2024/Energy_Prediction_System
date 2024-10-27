using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System;

public partial class LiveWheaterData : ContentPage
{
    private bool _isRunning = true;
    private readonly SensorSim rndVal = new();
    private bool _simulate = true;

    private readonly WeatherService weatherService = new();

    public LiveWheaterData()
    {
        InitializeComponent();
        StartSimulatingSensorData();
        LoadWeatherData();
    }
    private async void StartSimulatingSensorData()
    {
        while (_isRunning)
        {
            await Task.Delay(2000);
            
            double TTT = 0;
            double dd = 0;
            double ff = 0;
            double ff_gust = 0;
            double NA = 0;
            double pr = 0;
            double NN = 0;
            double FOG = 0;
            double LOW = 0;
            double MEDIUM = 0;
            double HIGH = 0;
            double TD = 0;
            double API_Txt = 0;


            if (_simulate)
            {
                TTT = rndVal.GetRandomDouble(0, 20);
                dd = rndVal.GetRandomDouble(0, 10);
                ff = rndVal.GetRandomDouble(0, 30);
                ff_gust = rndVal.GetRandomDouble(0, 30);
                NA = rndVal.GetRandomDouble(0, 30);
                pr = rndVal.GetRandomDouble(0, 30);
                NN = rndVal.GetRandomDouble(0, 30);
                FOG = rndVal.GetRandomDouble(0, 30);
                LOW = rndVal.GetRandomDouble(0, 30);
                MEDIUM = rndVal.GetRandomDouble(0, 30);
                HIGH = rndVal.GetRandomDouble(0, 30);
                TD = rndVal.GetRandomDouble(0, 30);
                API_Txt = rndVal.GetRandomDouble(0, 30);
            }
            else
            {
                // Read from webAPI or something
            }
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TTT_Label.Text = $"TTT: {TTT:F2}";
                dd_Label.Text = $"dd: {dd:F2}";
                ff_Label.Text = $"ff: {ff:F2}";
                ff_gust_Label.Text = $"ff_gust: {ff_gust:F2}";
                NA_Label.Text = $"NA: {NA:F2}";
                pr_Label.Text = $"pr: {pr:F2}";
                NN_Label.Text = $"NN: {NN:F2}";
                FOG_Label.Text = $"FOG: {FOG:F2}";
                LOW_Label.Text = $"LOW: {LOW:F2}";
                MEDIUM_Label.Text = $"MEDIUM: {MEDIUM:F2}";
                HIGH_Label.Text = $"HIGH: {HIGH:F2}";
                TD_Label.Text = $"TD: {TD:F2}";
                //API_TEXT.Text = $"API_TEXT: {API_Txt:F2}";

            });
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
    private async void LoadWeatherData()
    {
        string weatherData = await weatherService.GetWeatherAsync(59.7076562, 10.1559495, 90, 0);
        //API_TEXT.Text = weatherData;

        // Split by comma and then process each part
        var dataParts = weatherData.Split(',');
        // Extract values only (remove units)
        string temperature = dataParts[0].Split(':')[1].Trim().Replace("°C", "");
        string windSpeed = dataParts[1].Split(':')[1].Trim().Replace("m/s", "");
        string humidity = dataParts[2].Split(':')[1].Trim().Replace("%", "");
        string pressure = dataParts[3].Split(':')[1].Trim().Replace("hPa", "");
        string cloudiness = dataParts[4].Split(':')[1].Trim().Split(' ')[0].Replace("%", "");
     
        API_TEXT.Text = temperature + " °C " + windSpeed + " m/s "  + humidity + " % " + pressure + " hPa " + cloudiness + " % ";

    }
}