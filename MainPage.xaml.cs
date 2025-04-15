namespace MauiCalculator;

public partial class MainPage : ContentPage
{
    // This holds the number the user is currently typing
    string currentEntry = "";

    // This stores the previous number (before the operator was pressed)
    string lastEntry = "";

    // This stores the selected operator (+, -, *, /)
    string operation = "";

    public MainPage()
    {
        InitializeComponent();
    }

    // Called when a number or decimal button is pressed
    void OnButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;             // Cast sender to Button
        currentEntry += button.Text;             // Append the button text to current entry
        ResultLabel.Text = currentEntry;         // Update the display label
    }

    // Called when an operator (+, -, *, /) is pressed
    void OnOperatorClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        operation = button.Text;                 // Store the operator
        lastEntry = currentEntry;                // Store the number before operator
        currentEntry = "";                       // Clear current entry for the second number
    }

    // Resets the calculator
    void OnClearClicked(object sender, EventArgs e)
    {
        currentEntry = "";
        lastEntry = "";
        operation = "";
        ResultLabel.Text = "0";                  // Reset display
    }

    // Called when "=" button is pressed to perform the calculation
    void OnCalculateClicked(object sender, EventArgs e)
    {
        try
        {
            // Convert stored strings to numbers
            double num1 = Convert.ToDouble(lastEntry);
            double num2 = Convert.ToDouble(currentEntry);

            // Perform calculation based on the stored operator
            double result = operation switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" => num2 != 0 ? num1 / num2 : 0,    // Avoid divide by zero
                _ => 0
            };

            // Display the result and prepare it for reuse
            ResultLabel.Text = result.ToString();
            currentEntry = result.ToString();         // Allow chaining operations
        }
        catch
        {
            ResultLabel.Text = "Error";               // Fallback if input is invalid
        }
    }
}

