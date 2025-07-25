namespace ExcelView;

public partial class MainPage : ContentPage
{
    int count = 0;
    string selectedFile = "";
    string filePath = "";
    List<string[]> data = new List<string[]>(); // Fixed the syntax and initialization

    public MainPage()
    {
        InitializeComponent();
    }

    /*
    private void OnCounterClicked(object? sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
    */

    public async void SelectFile(object? sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                // File was picked
                string fileName = result.FileName;
                // You can access the file stream with result.OpenReadAsync()
                Console.WriteLine(fileName);
                FileSelector.Text = $"Selected ({fileName})\nselect different file";
                selectedFile = fileName;
                filePath = result.FullPath;
                var newData = ParseCsv(filePath);
                CsvCollectionView.ItemsSource = newData;
                data = newData; // Store the parsed data
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            // Handle exceptions (e.g., user canceled or permissions issue)
        }
    }

    public List<string[]> ParseCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        return lines.Select(line => line.Split(',')).ToList();
    }

}
