using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;
using System.Diagnostics;


namespace Energy_Prediction_System.Classes
{
    public class ReadSensors
    {
        public Livesensors sensor { get; set; } = new Livesensors();
        public HistoricData historical { get; set; } = new HistoricData();

        private readonly DatabaseWebAPIServices _databaseWebAPIServices;

        public ReadSensors()
        {
            _databaseWebAPIServices = new DatabaseWebAPIServices();
            getSensorData();
        }

        public class Livesensors
        {
            public double TT01 { get; set; }
            public double TT02 { get; set; }
            public double TT03 { get; set; }
            public double TT04 { get; set; }
            public double TT05 { get; set; }
            public double TT_AVG { get; set; }
            public DateTime? TT_DT { get; set; }
            public double RHT01 { get; set; }
            public double RHT02 { get; set; }
            public double RHT03 { get; set; }
            public double RHT04 { get; set; }
            public double RHT_AVG { get; set; }
            public DateTime? RHT_DT { get; set; }
            public double KWH01 { get; set; }
            public DateTime? KWH_DT { get; set; }
        }
        public class HistoricData
        {
            public float[] TT01 { get; set; }
            public float[] TT02 { get; set; }
            public float[] TT03 { get; set; }
            public float[] TT04 { get; set; }
            public float[] TT05 { get; set; }
        }


        public void getSensorData()
        {
            GetLatestBuildingTemp();
            GetLatestBuildingRelHumidity();
            GetLatestEnergyMeterData();
            GetAllBuildingTemp();
        }
        // Methods to retrieve temperature data
        private async void GetAllBuildingTemp()
        {
            string apiUrl = "/api/BuildingTemperatureItems";
            try
            {
                List<BuildingTemperatureItem> temperatureItems = await _databaseWebAPIServices.GetBuildingTempsAsync(apiUrl);
               
                // Last 14 days
                List<BuildingTemperatureItem> last14TemperatureItems = temperatureItems.OrderByDescending(item => item.Id).Take(14).ToList();

                foreach (var item in last14TemperatureItems)
                {
                    Debug.WriteLine($"Temp1: {item.Temp1}, DateTime: {item.TempDateTime}");
                }
                historical.TT01 = last14TemperatureItems.Select(item => item.Temp1).ToArray();
                historical.TT02 = last14TemperatureItems.Select(item => item.Temp2).ToArray();
                historical.TT03 = last14TemperatureItems.Select(item => item.Temp3).ToArray();
                historical.TT04 = last14TemperatureItems.Select(item => item.Temp4).ToArray();
                historical.TT05 = last14TemperatureItems.Select(item => item.Temp5).ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch temperature data");
            }
        }

        // Method to retrieve temperature data
        private async void GetLatestBuildingTemp()
        {
            string apiUrl = "/api/BuildingTemperatureItems/latest";
            try
            {
                var latestTemp = await _databaseWebAPIServices.GetLatestBuildingTempAsync(apiUrl);
                sensor.TT01 = latestTemp.Temp1;
                sensor.TT02 = latestTemp.Temp2;
                sensor.TT03 = latestTemp.Temp3;
                sensor.TT04 = latestTemp.Temp4;
                sensor.TT05 = latestTemp.Temp5;
                sensor.TT_AVG = (latestTemp.Temp1 + latestTemp.Temp2 + latestTemp.Temp3 + latestTemp.Temp4 + latestTemp.Temp5) / 5.0;
                sensor.TT_DT = latestTemp.TempDateTime;
                //Debug.WriteLine($"Id: {latestTemp.Id}");
                //Debug.WriteLine($"Temp1: {latestTemp.Temp1}");
                //Debug.WriteLine($"Temp2: {latestTemp.Temp2}");
                //Debug.WriteLine($"Temp3: {latestTemp.Temp3}");
                //Debug.WriteLine($"Temp4: {latestTemp.Temp4}");
                //Debug.WriteLine($"Temp5: {latestTemp.Temp5}");
                //Debug.WriteLine($"Unit of Measure: {latestTemp.TempUoM}");
                //Debug.WriteLine($"DateTime: {latestTemp.TempDateTime}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch temperature data");
                //await HandleError(ex, "Failed to fetch temperature data");
            }
        }

        private async void GetLatestBuildingRelHumidity()
        {
            string apiUrl = "/api/BuildingRelativeHumidityItems/latest";
            try
            {
                var latestHumidity = await _databaseWebAPIServices.GetLatestBuildingRelHumidityAsync(apiUrl);
                sensor.RHT01 = latestHumidity.RelHumidity1;
                sensor.RHT02 = latestHumidity.RelHumidity2; 
                sensor.RHT03 = latestHumidity.RelHumidity3;
                sensor.RHT04 = latestHumidity.RelHumidity4;
                sensor.RHT_AVG = (latestHumidity.RelHumidity1 + latestHumidity.RelHumidity2 + latestHumidity.RelHumidity3 + latestHumidity.RelHumidity4) / 4.0;
                sensor.RHT_DT = latestHumidity.RelHumidityDateTime;
                //Debug.WriteLine($"Id: {latestHumidity.Id}");
                //Debug.WriteLine($"RelHumidity1: {latestHumidity.RelHumidity1}");
                //Debug.WriteLine($"RelHumidity2: {latestHumidity.RelHumidity2}");
                //Debug.WriteLine($"RelHumidity3: {latestHumidity.RelHumidity3}");
                //Debug.WriteLine($"RelHumidity4: {latestHumidity.RelHumidity4}");
                //Debug.WriteLine($"Unit of Measure: {latestHumidity.RelHumidityUoM}");
                //Debug.WriteLine($"DateTime: {latestHumidity.RelHumidityDateTime}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch latest humidity");
                //await HandleError(ex, "Failed to fetch latest humidity");
            }
        }

        private async void GetLatestEnergyMeterData()
        {
            string apiUrl = "/api/BuildingEnergyMeterItems/latest";
            try
            {
                var latestEnergyMeter = await _databaseWebAPIServices.GetLatestBuildingEnergyMeterAsync(apiUrl);
                sensor.KWH01 = latestEnergyMeter.EnergyMeter1;
                sensor.KWH_DT = latestEnergyMeter.EnergyMeterDateTime;
                //Debug.WriteLine($"Id: {latestEnergyMeter.Id}");
                //Debug.WriteLine($"EnergyMeter1: {latestEnergyMeter.EnergyMeter1}");
                // Debug.WriteLine($"Unit of Measure: {latestEnergyMeter.EnergyMeterUoM}");
                //Debug.WriteLine($"DateTime: {latestEnergyMeter.EnergyMeterDateTime}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch latest energy meter data");
                //await HandleError(ex, "Failed to fetch latest energy meter data");
            }
        }

        /*
        // Error handling method
        private async Task HandleError(Exception ex, string message)
        {
            await DisplayAlert("Error", $"{message}: {ex.Message}", "OK");
        }
        */
    }
}

