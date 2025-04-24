namespace MauiCalculator.Services
{
    public class CalculatorService
    {
        private double memoryValue = 0;

        public double Add(double num1, double num2) => num1 + num2;
        public double Subtract(double num1, double num2) => num1 - num2;
        public double Multiply(double num1, double num2) => num1 * num2;
        public double Divide(double num1, double num2) => num2 == 0 ? double.NaN : num1 / num2;
        public double SquareRoot(double num) => Math.Sqrt(num);
        public double Square(double num) => Math.Pow(num, 2);
        public double Negate(double num) => -num;
        public double Percentage(double num) => num / 100;

        public void MemoryAdd(double num) => memoryValue += num;
        public double MemoryRecall() => memoryValue;
        public void MemoryClear() => memoryValue = 0;
    }
}
