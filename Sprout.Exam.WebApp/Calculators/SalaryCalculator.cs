namespace Sprout.Exam.Business.Calculators
{
    public interface ISalaryCalculator
    {
        decimal CalculateSalary(decimal absentDays, decimal workedDays);
    }

    public class RegularSalaryCalculator : ISalaryCalculator
    {
        public decimal CalculateSalary(decimal absentDays, decimal workedDays)
        {
            decimal baseSalary = 20000m;
            decimal tax = 0.12m * baseSalary;
            decimal deductions = baseSalary / 22 * absentDays;
            decimal salary = baseSalary - deductions - tax;
            return salary;
        }
    }

    public class ContractualSalaryCalculator : ISalaryCalculator
    {
        public decimal CalculateSalary(decimal absentDays, decimal workedDays)
        {
            decimal ratePerDay = 500m;
            decimal salary = ratePerDay * workedDays;
            return salary;
        }
    }
}