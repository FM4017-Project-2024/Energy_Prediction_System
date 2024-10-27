using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System;

public partial class LiveWheaterData : ContentPage
{
    private readonly WeatherService _weatherService = new();
    private readonly SensorSim _rndVal = new();

    private bool _isRunning = true;
    private bool _simulate = false;
    private int _taskDelay = 2000;

    private struct weatherData
    {
        public string TTT { get; set; }
        public string dd { get; set; }
        public string ff { get; set; }
        public string ff_gust { get; set; }
        public string NA { get; set; }
        public string pr { get; set; }
        public string NN { get; set; }
        public string FOG { get; set; }
        public string LOW { get; set; }
        public string MEDIUM { get; set; }
        public string HIGH { get; set; }
        public string TD { get; set; }
        public DateTime currentDateTime { get; set; }
    }
    weatherData value = new weatherData();

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
    private void UpdateGUI()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            TTT_Label.Text = $"TTT: {value.TTT:F2}";
            dd_Label.Text = $"dd: {value.dd:F2}";
            ff_Label.Text = $"ff: {value.ff:F2}";
            ff_gust_Label.Text = $"ff_gust: {value.ff_gust:F2}";
            NA_Label.Text = $"NA: {value.NA:F2}";
            pr_Label.Text = $"pr: {value.pr:F2}";
            NN_Label.Text = $"NN: {value.NN:F2}";
            FOG_Label.Text = $"FOG: {value.FOG:F2}";
            LOW_Label.Text = $"LOW: {value.LOW:F2}";
            MEDIUM_Label.Text = $"MEDIUM: {value.MEDIUM:F2}";
            HIGH_Label.Text = $"HIGH: {value.HIGH:F2}";
            TD_Label.Text = $"TD: {value.TD:F2}";
            DT_Label.Text = "Last update: " + value.currentDateTime;
        });
    }

    private void StartSimulatingSensorData()
    {
        value.TTT = _rndVal.GetRandomDouble(0, 20).ToString();
        value.dd = _rndVal.GetRandomDouble(0, 10).ToString();
        value.ff = _rndVal.GetRandomDouble(0, 30).ToString();
        value.ff_gust = _rndVal.GetRandomDouble(0, 30).ToString();
        value.NA = _rndVal.GetRandomDouble(0, 30).ToString();
        value.pr = _rndVal.GetRandomDouble(0, 30).ToString();
        value.NN = _rndVal.GetRandomDouble(0, 30).ToString();
        value.FOG = _rndVal.GetRandomDouble(0, 30).ToString();
        value.LOW = _rndVal.GetRandomDouble(0, 30).ToString();
        value.MEDIUM = _rndVal.GetRandomDouble(0, 30).ToString();
        value.HIGH = _rndVal.GetRandomDouble(0, 30).ToString();
        value.TD = _rndVal.GetRandomDouble(0, 30).ToString();
        value.currentDateTime = DateTime.Now;

        UpdateGUI();
    }

    private async void ReadWeatherDataAPI()
    {
        string weatherData = await _weatherService.GetWeatherAsync(59.7076562, 10.1559495, 90, 0);

        var dataParts = weatherData.Split(','); // Split by comma and then process each part
        value.TTT = dataParts[0].Split(':')[1].Trim().Replace("°C", "");
        value.ff = dataParts[1].Split(':')[1].Trim().Replace("m/s", "");
        value.NA = dataParts[2].Split(':')[1].Trim().Replace("%", "");
        value.pr = dataParts[3].Split(':')[1].Trim().Replace("hPa", "");
        value.NN = dataParts[4].Split(':')[1].Trim().Split(' ')[0].Replace("%", "");
        value.currentDateTime = DateTime.Now;

        UpdateGUI();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
}