using System.Text.RegularExpressions;
using Calculator.N_Endy.UserInteractionRepository;

namespace Calculator.N_Endy.CalculatorEngine
{
    public class CalculatorEngine
    {
        private readonly IUserInteraction _userInteraction;
        private readonly CalculatorLibrary.Calculator _calculator;

        public CalculatorEngine(IUserInteraction userInteraction, CalculatorLibrary.Calculator calculator)
        {
            _userInteraction = userInteraction;
            _calculator = calculator;

        }
        public void Run()
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            double numInput1 = 0;
            double numInput2 = 0;

            // Display Introductory message
            _userInteraction.DisplayIntroductoryMessage();

            // Ask user for first number
            numInput1 = _userInteraction.GetNumberFromUser();

            // Ask user for second number
            numInput2 = _userInteraction.GetNumberFromUser();

            // Ask user for operator
            string op = "";
            do
            {
                op = _userInteraction.GetOperatorFromUser();
            } while (! ValidateOperator(op));

            // Calculate and Display result
            DisplayResult(op, numInput1, numInput2);

        }


        // Validate operator input is not null and matches the pattern
        public bool ValidateOperator(string op)
        {
            if (op == null || Regex.IsMatch(op, "[a|s|m|d]"))
            {
                _userInteraction.ShowMessage("Error: Invalid operator\n");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DisplayResult(string op, double num1, double num2)
        {
            try
            {
                double result = _calculator.DoOperation(num1, num2, op);
                if (double.IsNaN(result))
                    _userInteraction.ShowMessage("This operation will result in a mathematical error.\n");
                else
                    _userInteraction.ShowMessage("Your result: {0:0.##}\n", result);
            }
            catch (Exception e)
            {
                _userInteraction.ShowMessage("Oh no! An exception occurred while trying to do the math.\n - Details: " + e.Message);
            }
        }
        // Ask user for first number
        // Ask user for second number
        // Ask user for operator
        // Calculate and display result
        // Ask to play again
    }
}