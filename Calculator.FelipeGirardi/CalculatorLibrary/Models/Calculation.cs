namespace CalculatorLibrary.Models;
public class Calculation
{
    public double operand1 { get; set; }
    public string op { get; set; }
    public double operand2 { get; set; }
    public double result { get; set; }

    public Calculation(double operand1, string op, double operand2, double result)
    {
        this.operand1 = operand1;
        this.op = op;
        this.operand2 = operand2;
        this.result = result;
    }
}