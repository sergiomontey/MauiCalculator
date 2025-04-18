namespace MauiCalculator;

public partial class MainPage : ContentPage
{
    // Store the number the user is currently entering
    private string currentEntry = "";

    // Store the previous number before an operation is chosen
    private string lastEntry = "";

    // Store the selected operator (+, -, *, /)
    private string operation = "";

    public MainPage()
    {
        InitializeComponent();
    }

    #region Button Handlers

    // Called when a number or decimal button is pressed
    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            // Append the clicked button's text (e.g., "7", "3", ".") to current input
            currentEntry += button.Text;
            UpdateDisplay(currentEntry);
        }
    }

    // Called when an operator button (+, -, *, /) is pressed
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                // Save the current number as the first operand
                lastEntry = currentEntry;

                // Clear currentEntry to get ready for the second number
                currentEntry = "";

                // Store the selected operation
                operation = button.Text;
            }
        }
    }

    // Called when the "C" button is pressed (Clear all)
    private void OnClearClicked(object sender, EventArgs e)
    {
        ResetCalculator();
    }

    // Called when "=" is pressed to calculate the result
    private void OnCalculateClicked(object sender, EventArgs e)
    {
        try
        {
            // Parse stored numbers
            double num1 = ParseInput(lastEntry);
            double num2 = ParseInput(currentEntry);

            // Perform calculation based on stored operator
            double result = operation switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" when num2 != 0 => num1 / num2, // Handle divide by zero safely
                "/" => double.NaN,                 // If division by zero, return Not-a-Number
                _ => 0
            };

            // Store and display result
            currentEntry = result.ToString();
            UpdateDisplay(currentEntry);
        }
        catch
        {
            // In case of any unexpected error, show a generic error
            ShowError();
        }
    }

    // Called when an advanced function button (√, x², +/-, %) is pressed
    private void OnAdvancedFunctionClicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is Button button)
            {
                double num = ParseInput(currentEntry);

                // Apply the appropriate mathematical operation
                double result = button.Text switch
                {
                    "√" => Math.Sqrt(num),
                    "x²" => Math.Pow(num, 2),
                    "+/-" => -num,
                    "%" => num / 100,
                    _ => num // Default: return the same number if no match
                };

                // Store and display result
                currentEntry = result.ToString();
                UpdateDisplay(currentEntry);
            }
        }
        catch
        {
            ShowError();
        }
    }

    #endregion

    #region Helper Methods

    // Updates the display label with formatted text
    private void UpdateDisplay(string text)
    {
        if (double.TryParse(text, out double number))
        {
            // If the number is an integer (e.g., 5.0), show it without decimal
            ResultLabel.Text = number % 1 == 0 ? ((int)number).ToString() : number.ToString();
        }
        else
        {
            ResultLabel.Text = text;
        }
    }

    // Resets the calculator state to default
    private void ResetCalculator()
    {
        currentEntry = "";
        lastEntry = "";
        operation = "";
        ResultLabel.Text = "0";
    }

    // Parses a string into a double safely
    private double ParseInput(string input)
    {
        if (double.TryParse(input, out double result))
            return result;
        else
            return 0;
    }

    // Displays a generic error message and clears the input
    private void ShowError()
    {
        ResultLabel.Text = "Error";
        currentEntry = "";
    }

    #endregion
}
