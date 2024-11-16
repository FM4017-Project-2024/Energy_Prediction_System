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
        List<EnergyPredictionItem>? predictionItems = await _predictionService.GetDatabaseStoredPredictions(7);
        if (predictionItems != null)
        {
            for (int i = 0; i < 7; i++)
            {
                string dateLabel = DateTime.Now.AddDays(i+1).ToString("dd.MM.yyyy");

            switch (i)
                {
                    case 0:
                        Day01_Label.Text = $"{dateLabel}: {predictionItems[i].EnergyPrediction} {predictionItems[i].EnergyPredictionUoM}";
                        break;
                    case 1:
                        Day02_Label.Text = $"{dateLabel}: {predictionItems[i].EnergyPrediction} {predictionItems[i].EnergyPredictionUoM}";
                        break;
                    case 2:
                        Day03_Label.Text = $"{dateLabel}: {predictionItems[i].EnergyPrediction} {predictionItems[i].EnergyPredictionUoM}";
                        break;
                    case 3:
                        Day04_Label.Text = $"{dateLabel}: {predictionItems[i].EnergyPrediction} {predictionItems[i].EnergyPredictionUoM}";
                        break;
                    case 4:
                        Day05_Label.Text = $"{dateLabel}: {predictionItems[i].EnergyPrediction} {predictionItems[i].EnergyPredictionUoM}";
                        break;
                    case 5:
                        Day06_Label.Text = $"{dateLabel}: {predictionItems[i].EnergyPrediction} {predictionItems[i].EnergyPredictionUoM}";
                        break;
                    case 6:
                        Day07_Label.Text = $"{dateLabel}: {predictionItems[i].EnergyPrediction} {predictionItems[i].EnergyPredictionUoM}";
                        break;
                }
            }
        }
    }

}