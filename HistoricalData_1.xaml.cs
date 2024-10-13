using Energy_Prediction_System.Classes;
using Syncfusion.Maui.Charts;
using System.Collections.ObjectModel;

namespace Energy_Prediction_System;

public class ChartModel
{
    public string Time { get; set; }
    public double Value { get; set; }
}

public partial class HistoricalData_1 : ContentPage
{
    private readonly SensorSim rndVal = new();
    private bool _isRunning = true;
    public ObservableCollection<ChartModel> Data { get; set; }
    private int _counter;
    
    public HistoricalData_1()
    {
        InitializeComponent();
        Data = new ObservableCollection<ChartModel>
        {
            new ChartModel { Time = "10:00", Value = 20.5 },
            new ChartModel { Time = "10:30", Value = 21.2 },
            new ChartModel { Time = "11:00", Value = 19.8 },
            new ChartModel { Time = "11:30", Value = 22.1 },
            new ChartModel { Time = "12:00", Value = 23.4 },
            new ChartModel { Time = "12:30", Value = 24.0 },
            new ChartModel { Time = "13:00", Value = 22.8 }
        };
        _counter = 8; // Start counter after initial data points
        BindingContext = this;

        StartSimulatingSensorData();
    }

    private async void StartSimulatingSensorData()
    {
        while (_isRunning)
        {
            await Task.Delay(3000);

            string newTime = $"13:{_counter * 5:00}";
            double newValue = rndVal.GetRandomDouble(0, 20);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Data.Add(new ChartModel { Time = newTime, Value = newValue });
                _counter++;
                if (Data.Count > 10)
                {
                    Data.RemoveAt(0);
                }
            });
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
}