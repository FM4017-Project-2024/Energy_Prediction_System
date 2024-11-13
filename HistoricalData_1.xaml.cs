using Energy_Prediction_System.Classes;
using Newtonsoft.Json.Linq;
using Syncfusion.Maui.Charts;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
    public string SensorName { get; set; }

    public HistoricalData_1(string sensorName, string unit, float[] values, DateTime? dt)
    {

        InitializeComponent();
        SensorName = sensorName;
        AxisTitle.Text = unit;

        string[] TimeStamps = new string[values.Length];
        DateTime? currentDate = dt;
        for (int i = 0; i < values.Length; i++)
        {
            DateTime date = currentDate.Value.AddDays(-i);
            TimeStamps[i] = date.ToString("dd/MM/yyyy");
        }

        Data = new ObservableCollection<ChartModel>();
        for (int i = 0; i < values.Length; i++)
        {
            Data.Add(new ChartModel
            {
                Time = TimeStamps[i],
                Value = values[i]
            });
        }

        BindingContext = this;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
}