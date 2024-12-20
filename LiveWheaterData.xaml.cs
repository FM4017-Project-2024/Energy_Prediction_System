using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System;

public partial class LiveWheaterData : ContentPage
{
    private readonly SensorSim _rndVal = new();

    private bool _isRunning = true;
    private readonly bool _simulate = false;
    private readonly int _taskDelay = 2000;

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
            UpdateGUI();
            await Task.Delay(_taskDelay);
        }
    }

    private void UpdateGUI()
    {
        var weatherValues = App.WeatherValues;

        if(_simulate)
        {
            TTT_Label.Text = _rndVal.GetRandomDouble(0, 20).ToString();
            dd_Label.Text = _rndVal.GetRandomDouble(0, 10).ToString();
            ff_Label.Text = _rndVal.GetRandomDouble(0, 30).ToString();
            NA_Label.Text = _rndVal.GetRandomDouble(0, 30).ToString();
            pr_Label.Text = _rndVal.GetRandomDouble(0, 30).ToString();
            NN_Label.Text = _rndVal.GetRandomDouble(0, 30).ToString();
            LOW_Label.Text = _rndVal.GetRandomDouble(0, 30).ToString();
            MEDIUM_Label.Text = _rndVal.GetRandomDouble(0, 30).ToString();
            HIGH_Label.Text = _rndVal.GetRandomDouble(0, 30).ToString();
            TD_Label.Text = _rndVal.GetRandomDouble(0, 30).ToString();
            DT_Label.Text = DateTime.Now.ToString();
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TTT_Label.Text = $"Temperature: {weatherValues.weather.TTT:F2}" + " �C";
                dd_Label.Text = $"Wind direction: {weatherValues.weather.dd:F2}" + " �";
                ff_Label.Text = $"Wind speed: {weatherValues.weather.ff:F2}" + " m/s";
                NA_Label.Text = $"Humidity: {weatherValues.weather.NA:F2}" + " %";
                pr_Label.Text = $"Pressure: {weatherValues.weather.pr:F2}" + " hPa";
                NN_Label.Text = $"Cloudiness: {weatherValues.weather.NN:F2}" + " %";
                LOW_Label.Text = $"Low clouds: {weatherValues.weather.LOW:F2}" + " %";
                MEDIUM_Label.Text = $"Medium clouds: {weatherValues.weather.MEDIUM:F2}" + " %";
                HIGH_Label.Text = $"High clouds: {weatherValues.weather.HIGH:F2}" + " %";
                TD_Label.Text = $"Dewpoint temperature: {weatherValues.weather.TD:F2}" + " �C";
                DT_Label.Text = weatherValues.weather.currentDateTime.ToString();
            });
        }
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

    private async void OnLabelTapped_TTT(object sender, EventArgs e)
    {
        if (App.SensorValues.historicalWeather.Temperature != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside temperature", "Temperature[�C]", App.SensorValues.historicalWeather.Temperature, App.SensorValues.historicalWeather.DT.Max()));
        }   
    }

    private async void OnLabelTapped_dd(object sender, EventArgs e)
    {
        if (App.SensorValues.historicalWeather.WindDirection != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside wind direction", "Wind direction[�]", App.SensorValues.historicalWeather.WindDirection, App.SensorValues.historicalWeather.DT.Max()));
        }
    }

    private async void OnLabelTapped_ff(object sender, EventArgs e)
    {
        if (App.SensorValues.historicalWeather.WindSpeed != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside wind speed", "Wind speed[m/s]", App.SensorValues.historicalWeather.WindSpeed, App.SensorValues.historicalWeather.DT.Max()));
        }
    }

    private async void OnLabelTapped_NA(object sender, EventArgs e)
    {
        if (App.SensorValues.historicalWeather.Humidity != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside humididty", "Humididty[%]", App.SensorValues.historicalWeather.Humidity, App.SensorValues.historicalWeather.DT.Max()));
        }
    }

    private async void OnLabelTapped_pr(object sender, EventArgs e)
    {
        if (App.SensorValues.historicalWeather.Pressure != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside pressure", "Pressure[hPa]", App.SensorValues.historicalWeather.Pressure, App.SensorValues.historicalWeather.DT.Max()));
        }
    }

    private async void OnLabelTapped_NN(object sender, EventArgs e)
    {
        if (App.SensorValues.historicalWeather.Cloudiness != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside cloudiness", "Cloudiness[%]", App.SensorValues.historicalWeather.Cloudiness, App.SensorValues.historicalWeather.DT.Max()));
        }   
    }

    private async void OnLabelTapped_LOW(object sender, EventArgs e)
    {
        if (App.SensorValues.historicalWeather.LowClouds != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside low clouds", "Low clouds[%]", App.SensorValues.historicalWeather.LowClouds, App.SensorValues.historicalWeather.DT.Max()));
        }
    }

    private async void OnLabelTapped_MEDIUM(object sender, EventArgs e)
    {
        if (App.SensorValues.historicalWeather.MediumClouds != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside medium clouds", "Medium clouds[%]", App.SensorValues.historicalWeather.MediumClouds, App.SensorValues.historicalWeather.DT.Max()));
        }    
    }

    private async void OnLabelTapped_HIGH(object sender, EventArgs e)
    {
        if (App.SensorValues.historicalWeather.HighClouds != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside high clouds", "High clouds[%]", App.SensorValues.historicalWeather.HighClouds, App.SensorValues.historicalWeather.DT.Max()));
        }
    }

    private async void OnLabelTapped_TD(object sender, EventArgs e)
    {
        if(App.SensorValues.historicalWeather.DewpointTemperature != null && App.SensorValues.historicalWeather.DT != null)
        {
            await Navigation.PushAsync(new HistoricalData_1("Outside dew point temperature", "Temperature[�C]", App.SensorValues.historicalWeather.DewpointTemperature, App.SensorValues.historicalWeather.DT.Max()));
        }
    }
}