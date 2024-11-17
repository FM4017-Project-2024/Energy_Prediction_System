using Energy_Prediction_System.Classes;
using Newtonsoft.Json.Linq;
using Syncfusion.Maui.Charts;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Energy_Prediction_System;

public class ChartModel
{
    public string? Time { get; set; }
    public double Value { get; set; }
}

public partial class HistoricalData_1 : ContentPage
{
    public ObservableCollection<ChartModel> Data { get; set; }
    public string SensorName { get; set; }

    public HistoricalData_1(string sensorName, string unit, float[] values, DateTime? dt)
    {
        InitializeComponent();
        SensorName = sensorName;
        AxisTitle.Text = unit;

        string[] TimeStamps = new string[values.Length];
        Data = [];
        if (dt != null)
        {
            for (int i = 0; i < values.Length; i++)
            {
                TimeStamps[i] = dt.Value.AddDays(-i).ToString("dd.MM.yyyy");
                Data.Add(new ChartModel
                {
                    Time = TimeStamps[i],
                    Value = values[i]
                });
            }
            BindingContext = this;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}