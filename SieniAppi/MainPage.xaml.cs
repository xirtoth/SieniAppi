namespace SieniAppi;
using GoogleApi;
using Newtonsoft.Json;

public partial class MainPage : ContentPage
{

    int count = 0;
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;
    private List<Location> locations = new List<Location>();
    private Location testLocation;


    readonly GoogleApi.GoogleMaps test;


    private async Task OpenMap()
    {
        testLocation.Latitude = 61.54954653547623;
        testLocation.Longitude = 23.558148616781914;
        try
        {
            await Map.OpenAsync(locations[0], new MapLaunchOptions
            {
                Name = "Sieni",

            });
        }
        catch (Exception ex)
        {

        }
    }

    private void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
    {
        // Update UI Label with magnetometer state
        Testitext.TextColor = Colors.Green;
        Testitext.Text = $"Magnetometer: {e.Reading}";
    }
    public async void GetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
            {
                locations.Add(location);
                count++;
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                await DisplayAlert("Moi", location.Latitude.ToString() + " " + location.Longitude.ToString() + " Yhteensä " + locations.Count.ToString() + " Kohdetta", "OK");
                var tempLocation = new LocationDetail();
                tempLocation.name = "Sieni";
                tempLocation.MushroomType = "Kanttarelli";
                tempLocation.location.Latitude = location.Latitude;
                tempLocation.location.Longitude = location.Longitude;
                await DisplayAlert("Testi", tempLocation.location.Latitude.ToString(), "OK");

            }

        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {

        }
        finally
        {
            _isCheckingLocation = false;

        }
    }




    public MainPage()
    {

        InitializeComponent();
        Testitext.Text = "Testing";
        LocationDetail test = new LocationDetail();
        test.name = "testi1";
        test.MushroomType = "kanttarelli";
        //ToggleMagnetometer(mm);
        // ToggleCompass(compass);
        //test.location.Longitude = 61.561159978136175;
        //test.location.Latitude = 23.577795380457328;
        //InitTimer();



    }
    public async Task TestMap()
    {

        //add multiple markers to google maps


    }
    public async void OpenCatalog()
    {
        await Navigation.PushAsync(new NewPage1());
    }
    public async void SaveLocationsList()
    {
        //save list of locations to file


    }
    private void OnMushroomCatalogClicked(object Sender, EventArgs e)
    {
        OpenCatalog();
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }
    private void OnTestClicked(object sender, EventArgs e)
    {
        for (int i = 0; i < locations.Count; i++)
            Testitext.Text += locations[i].Timestamp;
    }

    private async void OnPickerSelectedItemChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        await DisplayAlert("Testi", selectedIndex.ToString(), "OK");

    }
    private async void OnCounterClicked(object sender, EventArgs e)
    {

        if (locations.Count > 0)
        {
            MushroomLocations.ItemsSource = locations;
            await DisplayAlert("Testi", "Kohteita on " + MushroomLocations.ItemsSource.Count.ToString(), "OK");
            OpenMap();
        }
        GetCurrentLocation();


        //Picker picker = new Picker { Title = "Sienimestat" };
        //picker.ItemsSource = locations;


        SemanticScreenReader.Announce(CounterBtn.Text);


        //open google maps on test.location

        //add current location to list of locations





        /* try
         {
             await Map.Default.OpenAsync(61.561159978136175, 23.577795380457328);

         }
         catch(Exception ex)
         {




         } */


    
        

    }

    private List<Location> LoadLocations()
    {
        return null;
    }
    private void SaveLocations(List<Location> loc)
    {
        string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "locations.json");
        using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
        JsonSerializer serializer = new JsonSerializer();
        using StreamWriter stremWriter = new StreamWriter(outputStream);        
        serializer.Serialize(stremWriter, loc);
        
          
    }
 
    private void OnSaveClicked(object sender, EventArgs e)
    {
        SaveLocations(locations);
    }

   
    private void OnLoadClicked(object sender, EventArgs e)
    {
        
    }
}

