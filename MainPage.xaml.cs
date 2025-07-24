namespace ExcelView;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	public async void SelectFile()
	{
		try
		{
			var result = await FilePicker.Default.PickAsync();
			if (result != null)
			{
				// File was picked
				string fileName = result.FileName;
				// You can access the file stream with result.OpenReadAsync()
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			// Handle exceptions (e.g., user canceled or permissions issue)
		}
    }
}
