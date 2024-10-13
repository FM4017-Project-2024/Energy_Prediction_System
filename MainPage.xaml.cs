namespace Energy_Prediction_System
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnSensorData1ButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new SensorData_1());
        private async void OnSensorData2ButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new SensorData_2());
        private async void OnHistData1ButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1());
    }
}

