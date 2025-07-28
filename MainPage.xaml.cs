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
                data = newData; // Store the parsed data

                // Save data to the Grid
                int rows = newData.Count;
                int cols = newData[0].Length;

                // Define fixed rows/columns
                CsvCollectionView.RowDefinitions.Clear();
                CsvCollectionView.ColumnDefinitions.Clear();

                for (int r = 0; r < rows; r++)
                    CsvCollectionView.RowDefinitions.Add(new RowDefinition { Height = 50 });

                for (int c = 0; c < cols; c++)
                    CsvCollectionView.ColumnDefinitions.Add(new ColumnDefinition { Width = 100 });

                // Add labels for each cell
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        var label = new Label
                        {
                            Text = data[r][c],
                            BackgroundColor = Colors.LightGray,
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center
                        };

                        CsvCollectionView.Add(label, c, r); // column, row
                    }
                }
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
