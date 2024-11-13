using Energy_Prediction_System.Classes;
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
            if (!updated)
            {
                await _predictionService.UpdateDatabaseWithPredictions(7);
            }
            FillEnergyPage();
        }
        catch (Exception ex)
        {
            predictionLabel.Text = $"Error: {ex.Message}";
        }
    }

    private async void FillEnergyPage()
    {
        /*
        Fills the energy page with predictions stored in database
        */
        List<EnergyPredictionItem>? predictionItems = await _predictionService.GetDatabaseStoredPredictions(7);
        if (predictionItems != null)
        {
            Day01_Label.Text = "Day 1: "+ predictionItems[0].EnergyPrediction.ToString() + predictionItems[0].EnergyPredictionUoM;
            Day02_Label.Text = "Day 2: " + predictionItems[1].EnergyPrediction.ToString() + predictionItems[1].EnergyPredictionUoM;
            Day03_Label.Text = "Day 3: " + predictionItems[2].EnergyPrediction.ToString() + predictionItems[2].EnergyPredictionUoM;
            Day04_Label.Text = "Day 4: " + predictionItems[3].EnergyPrediction.ToString() + predictionItems[3].EnergyPredictionUoM;
            Day05_Label.Text = "Day 5: " + predictionItems[4].EnergyPrediction.ToString() + predictionItems[4].EnergyPredictionUoM;
            Day06_Label.Text = "Day 6: " + predictionItems[5].EnergyPrediction.ToString() + predictionItems[5].EnergyPredictionUoM;
            Day07_Label.Text = "Day 7: " + predictionItems[6].EnergyPrediction.ToString() + predictionItems[6].EnergyPredictionUoM;
        }
    }

}