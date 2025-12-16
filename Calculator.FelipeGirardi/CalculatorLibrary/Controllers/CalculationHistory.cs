using CalculatorLibrary.Models;

namespace CalculatorLibrary.Controllers;
public class CalculationHistory
{
    public List<Calculation> Calculations = MockDatabase.Calculations;

    public void ShowCalculationHistory()
    {
        int nCalculation = 1;

        if (Calculations.Count == 0)
        {
            Console.WriteLine("No calculations were made yet.");
            return;
        }

        foreach (Calculation calculation in Calculations)
        {
            if (calculation.op != "√" && calculation.op != "sin" && calculation.op != "cos")
                Console.WriteLine($"{nCalculation}) {calculation.operand1.ToString("0.##")} {calculation.op} {calculation.operand2.ToString("0.##")} = {calculation.result.ToString("0.##")}");
            else
                Console.WriteLine($"{nCalculation}) {calculation.op} {calculation.operand1.ToString("0.##")} = {calculation.result.ToString("0.##")}");
            nCalculation++;
        }
    }

    public void DeleteCalculationHistory()
    {
        MockDatabase.Calculations.Clear();
        Console.WriteLine("Calculation list deleted.");
    }

    public bool IsCalculationListEmpty()
    {
        return Calculations.Count == 0;
    }
}
