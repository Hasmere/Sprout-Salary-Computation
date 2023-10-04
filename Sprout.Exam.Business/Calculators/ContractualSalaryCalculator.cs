namespace Sprout.Exam.Business.Calculators
{
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