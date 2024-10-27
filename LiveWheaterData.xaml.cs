using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System;

public partial class LiveWheaterData : ContentPage
{
    private readonly WeatherService weatherService = new();
    private readonly SensorSim rndVal = new();

    private bool _isRunning = true;
    private bool _simulate = false;
    private int _taskDelay = 2000;

    private double TTT = 0;
    private double dd = 0;
    private double ff = 0;
    private double ff_gust = 0;
    private double NA = 0;
    private double pr = 0;
    private double NN = 0;
    private double FOG = 0;
    private double LOW = 0;
    private double MEDIUM = 0;
    private  double HIGH = 0;
    private double TD = 0;
    DateTime currentDateTime;

    public LiveWheaterData()
    {
        if(_simulate)
        {
            _taskDelay = 2000;
        }
        else
        {
            _taskDelay = 60000;
        }

        InitializeComponent();
        getWeatherData();
    }
    private async void getWeatherData()
    {
        while (_isRunning)
        {
            if (_simulate)
            {
                StartSimulatingSensorData();
            }
            else
            {
                ReadWeatherDataAPI();
            }
            await Task.Delay(_taskDelay);
        }
    }

    private void StartSimulatingSensorData()
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
        currentDateTime = DateTime.Now;

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
            API_TEXT.Text = " Last update: " + currentDateTime;
        });
    }

    private async void ReadWeatherDataAPI()
    {
        string weatherData = await weatherService.GetWeatherAsync(59.7076562, 10.1559495, 90, 0);

        // Split by comma and then process each part
        var dataParts = weatherData.Split(',');
        string temperature = dataParts[0].Split(':')[1].Trim().Replace("°C", "");
        string windSpeed = dataParts[1].Split(':')[1].Trim().Replace("m/s", "");
        string humidity = dataParts[2].Split(':')[1].Trim().Replace("%", "");
        string pressure = dataParts[3].Split(':')[1].Trim().Replace("hPa", "");
        string cloudiness = dataParts[4].Split(':')[1].Trim().Split(' ')[0].Replace("%", "");
        currentDateTime = DateTime.Now;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            //API_TEXT.Text = temperature + " °C " + windSpeed + " m/s " + humidity + " % " + pressure + " hPa " + cloudiness + " % " + currentDateTime;
            API_TEXT.Text = " Last update: " + currentDateTime;

            TTT_Label.Text = $"TTT: {temperature:F2}";
            ff_Label.Text = $"ff: {windSpeed:F2}";
            NA_Label.Text = $"NA: {humidity:F2}";
            pr_Label.Text = $"pr: {pressure:F2}";
            NN_Label.Text = $"NN: {cloudiness:F2}";
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
}