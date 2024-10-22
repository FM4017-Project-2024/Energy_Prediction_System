using Syncfusion.Maui.Core.Hosting;
namespace Energy_Prediction_System
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF5cWWtCf1NpR2NGfV5ycEVDal9YTndcUiweQnxTdEFiWX5dcHFWT2FfWER3Xg==");
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
