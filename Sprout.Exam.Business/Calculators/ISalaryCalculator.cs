namespace Sprout.Exam.Business.Calculators
{
    public interface ISalaryCalculator
    {
        decimal CalculateSalary(decimal absentDays, decimal workedDays);
    }
}