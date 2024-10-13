using Energy_Prediction_System.Classes;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;


namespace Energy_Prediction_System;

public partial class SensorData_1 : ContentPage
{
    private bool _isRunning = true;
    private readonly SensorSim rndVal = new();
   
    public SensorData_1()
    {
        InitializeComponent();
        StartSimulatingSensorData();
    }
    private async void StartSimulatingSensorData()
    {
        while (_isRunning)
        {
            await Task.Delay(1000);

            double temp1 = rndVal.GetRandomDouble(0, 20);
            double temp2 = rndVal.GetRandomDouble(0, 10);
            double temp3 = rndVal.GetRandomDouble(0, 30);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                T1Label.Text = $"T_1: {temp1:F2}";
                T2Label.Text = $"T_2: {temp2:F2}";
                T3Label.Text = $"T_3: {temp3:F2}";
            });
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
    private async void OnSensorData2ButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SensorData_2());
    }
}