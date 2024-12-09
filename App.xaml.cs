using CPSY200FinalRentalProject.Data;
namespace CPSY200FinalRentalProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            DBHandler.InitializeDatabase();
        }
    }
}
