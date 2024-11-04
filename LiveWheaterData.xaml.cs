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
        public string NA { get; set; }
        public string pr { get; set; }
        public string NN { get; set; }
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
            TTT_Label.Text = $"Temperature: {value.TTT:F2}" + " °C";
            dd_Label.Text = $"Wind direction: {value.dd:F2}" + " deg";
            ff_Label.Text = $"Wind speed: {value.ff:F2}" + " m/s";
            NA_Label.Text = $"Humidity: {value.NA:F2}" + " %";
            pr_Label.Text = $"Pressure: {value.pr:F2}" + " hPa";
            NN_Label.Text = $"Cloudiness: {value.NN:F2}" + " %";
            LOW_Label.Text = $"Low clouds: {value.LOW:F2}" + " %";
            MEDIUM_Label.Text = $"Medium clouds: {value.MEDIUM:F2}" + " %";
            HIGH_Label.Text = $"High clouds: {value.HIGH:F2}" + " %";
            TD_Label.Text = $"Dewpoint temperature: {value.TD:F2}" + " °C";
            DT_Label.Text = value.currentDateTime.ToString();
        });
    }

    private void StartSimulatingSensorData()
    {
        value.TTT = _rndVal.GetRandomDouble(0, 20).ToString();
        value.dd = _rndVal.GetRandomDouble(0, 10).ToString();
        value.ff = _rndVal.GetRandomDouble(0, 30).ToString();
        value.NA = _rndVal.GetRandomDouble(0, 30).ToString();
        value.pr = _rndVal.GetRandomDouble(0, 30).ToString();
        value.NN = _rndVal.GetRandomDouble(0, 30).ToString();
        value.LOW = _rndVal.GetRandomDouble(0, 30).ToString();
        value.MEDIUM = _rndVal.GetRandomDouble(0, 30).ToString();
        value.HIGH = _rndVal.GetRandomDouble(0, 30).ToString();
        value.TD = _rndVal.GetRandomDouble(0, 30).ToString();
        value.currentDateTime = DateTime.Now;

        UpdateGUI();
    }

    private async void ReadWeatherDataAPI()
    {
        WeatherForecastItem weatherData = await _weatherService.GetWeatherDataAsync(59.7076562, 10.1559495, 124);

        value.currentDateTime = DateTime.Now;
        value.TTT = weatherData.Temperature.ToString();
        value.dd = weatherData.WindDirection.ToString();
        value.ff = weatherData.WindSpeed.ToString();
        value.NA = weatherData.Humidity.ToString();
        value.pr = weatherData.Pressure.ToString();
        value.NN = weatherData.Cloudiness.ToString();
        value.LOW = weatherData.LowClouds.ToString();
        value.MEDIUM = weatherData.MediumClouds.ToString();
        value.HIGH = weatherData.HighClouds.ToString();
        value.TD = weatherData.DewpointTemperature.ToString();


        UpdateGUI();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _isRunning = true;
        getWeatherData();
    }

    private async void OnLabelTapped_TTT(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TTT"));
    private async void OnLabelTapped_dd(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("dd"));
    private async void OnLabelTapped_ff(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("ff"));
    private async void OnLabelTapped_NA(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("NA"));
    private async void OnLabelTapped_pr(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("NN"));
    private async void OnLabelTapped_NN(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TD"));
    private async void OnLabelTapped_FOG(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("FOG"));
    private async void OnLabelTapped_LOW(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("LOW"));
    private async void OnLabelTapped_MEDIUM(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("MEDIUM"));
    private async void OnLabelTapped_HIGH(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("HIGH"));
    private async void OnLabelTapped_TD(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TD"));
    private async void OnAddWeatherDataButtonClicked(object sender, EventArgs e)
    {
        try
        {
            string test = await _weatherService.AddWeatherDataToDatabase(59.7076562, 10.1559495, 194);
            await DisplayAlert("Data Lagt til", "Værdata er lagt til i databasen.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Feil", $"Kunne ikke legge til værdata: {ex.Message}", "OK");
        }
    }
}