using Energy_Prediction_System.Services;


namespace Energy_Prediction_System;

public partial class Energy : ContentPage
{
    private readonly PredictionService _predictionService = new PredictionService();
    public Energy()
	{
		InitializeComponent();
	}
    private async void OnGetPredictionClicked(object sender, EventArgs e)
    {
        try
        {
            // Pass the path of the CSV file
            string prediction = await _predictionService.GetEnergyConsumptionPredictionAsync();
            predictionLabel.Text = prediction;
        }
        catch (Exception ex)
        {
            predictionLabel.Text = $"Error: {ex.Message}";
        }
    }
}