using Energy_Prediction_System.Services;
using System.Data;


namespace Energy_Prediction_System;

public partial class Energy : ContentPage
{
    private readonly PredictionService _predictionService = new PredictionService();
    public Energy()
	{
		InitializeComponent();
        UpdatePredictions();
    }

    private async void UpdatePredictions()
    {
        try
        {
            
            bool updated = await _predictionService.IsDatabaseUpdatedAsync();
            if (updated == false)
            {
                _predictionService.UpdateDatabaseWithPredictions(7);
            }
        }
        catch (Exception ex)
        {
            predictionLabel.Text = $"Error: {ex.Message}";
        }
    }
   
}