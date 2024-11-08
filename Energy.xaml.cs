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
            if (updated)
            {
                predictionLabel.Text = $"Database already up to date";
            }
            else
            {
                _predictionService.UpdateDatabaseWithPredictions(7);
                predictionLabel.Text = $"Database updated";
            }
        }
        catch (Exception ex)
        {
            predictionLabel.Text = $"Error: {ex.Message}";
        }
    }
    private async void OnGetPredictionClicked(object sender, EventArgs e)
    {
        try
        {
            _predictionService.UpdateDatabaseWithPredictions(7);
        }
        catch (Exception ex)
        {
            predictionLabel.Text = $"Error: {ex.Message}";
        }
    }
}