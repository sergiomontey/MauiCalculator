using MauiCalculator.Services;
using System.Collections.ObjectModel;

namespace MauiCalculator.Views;

public partial class MainPage : ContentPage
{
    private CalculatorService calculatorService = new CalculatorService(); // Instance of CalculatorService
    private string currentEntry = "";
    private string lastEntry = "";
    private string operation = "";
    private ObservableCollection<string> historyList = new ObservableCollection<string>(); // For History Panel

    public MainPage()
    {
        InitializeComponent();
        HistoryList.ItemsSource = historyList; // Bind CollectionView to history list
    }

    #region Button Handlers

    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            // Prevent multiple decimals
            if (button.Text == "." && currentEntry.Contains("."))
                return;

            currentEntry += button.Text;
            UpdateDisplay(currentEntry);
        }
    }

    private void OnOperatorClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                lastEntry = currentEntry;
                currentEntry = "";
                operation = button.Text;
            }
        }
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        ResetCalculator();
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        try
        {
            double num1 = ParseInput(lastEntry);
            double num2 = ParseInput(currentEntry);

            double result = operation switch
            {
                "+" => calculatorService.Add(num1, num2),
                "-" => calculatorService.Subtract(num1, num2),
                "*" => calculatorService.Multiply(num1, num2),
                "/" => calculatorService.Divide(num1, num2),
                _ => 0
            };

            currentEntry = result.ToString();
            UpdateDisplay(currentEntry);
            AddToHistory($"{num1} {operation} {num2} = {FormatNumber(result)}");
        }
        catch
        {
            ShowError();
        }
    }

    private void OnAdvancedFunctionClicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is Button button)
            {
                double num = ParseInput(currentEntry);

                double result = button.Text switch
                {
                    "√" => calculatorService.SquareRoot(num),
                    "x²" => calculatorService.Square(num),
                    "+/-" => calculatorService.Negate(num),
                    "%" => calculatorService.Percentage(num),
                    _ => num
                };

                currentEntry = result.ToString();
                UpdateDisplay(currentEntry);
                AddToHistory($"{button.Text}({num}) = {FormatNumber(result)}");
            }
        }
        catch
        {
            ShowError();
        }
    }

    private void OnMemoryButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            double num = ParseInput(currentEntry);

            switch (button.Text)
            {
                case "M+":
                    calculatorService.MemoryAdd(num);
                    break;
                case "MR":
                    currentEntry = calculatorService.MemoryRecall().ToString();
                    UpdateDisplay(currentEntry);
                    break;
                case "MC":
                    calculatorService.MemoryClear();
                    break;
            }
        }
    }

    private void OnThemeToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }

    #endregion

    #region Helper Methods

    private void UpdateDisplay(string text)
    {
        if (double.TryParse(text, out double number))
        {
            ResultLabel.Text = number % 1 == 0 ? ((int)number).ToString() : number.ToString();
        }
        else
        {
            ResultLabel.Text = text;
        }
    }

    private void ResetCalculator()
    {
        currentEntry = "";
        lastEntry = "";
        operation = "";
        ResultLabel.Text = "0";
    }

    private double ParseInput(string input)
    {
        if (double.TryParse(input, out double result))
            return result;
        else
            return 0;
    }

    private void ShowError()
    {
        ResultLabel.Text = "Error";
        currentEntry = "";
    }
    private void OnClearHistoryClicked(object sender, EventArgs e)
    {
        historyList.Clear();
    }

    private void AddToHistory(string record)
    {
        historyList.Insert(0, record); // Add new record to the top
        if (historyList.Count > 10)
            historyList.RemoveAt(historyList.Count - 1); // Keep only last 10 entries
    }

    private string FormatNumber(double num)
    {
        return num.ToString("N0"); // Format with commas for large numbers
    }

    #endregion
}
