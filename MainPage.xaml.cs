namespace MauiCalculator;

public partial class MainPage : ContentPage
{
    string currentEntry = "";
    string lastEntry = "";
    string operation = "";

    public MainPage()
    {
        InitializeComponent();
    }

    void OnButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        currentEntry += button.Text;
        ResultLabel.Text = currentEntry;
    }

    void OnOperatorClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        operation = button.Text;
        lastEntry = currentEntry;
        currentEntry = "";
    }

    void OnClearClicked(object sender, EventArgs e)
    {
        currentEntry = "";
        lastEntry = "";
        operation = "";
        ResultLabel.Text = "0";
    }

    void OnCalculateClicked(object sender, EventArgs e)
    {
        try
        {
            double num1 = Convert.ToDouble(lastEntry);
            double num2 = Convert.ToDouble(currentEntry);
            double result = operation switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" => num2 != 0 ? num1 / num2 : 0,
                _ => 0
            };
            ResultLabel.Text = result.ToString();
            currentEntry = result.ToString();
        }
        catch
        {
            ResultLabel.Text = "Error";
        }
    }
}
