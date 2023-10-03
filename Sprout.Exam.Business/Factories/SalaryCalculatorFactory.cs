using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.Calculators;
using System;

namespace Sprout.Exam.Business.Factories
{
    public class SalaryCalculatorFactory
    {
        public ISalaryCalculator Create(EmployeeType employeeType)
        {
            switch (employeeType)
            {
                case EmployeeType.Regular:
                    return new RegularSalaryCalculator();

                case EmployeeType.Contractual:
                    return new ContractualSalaryCalculator();

                default:
                    throw new InvalidOperationException("Employee Type not found");
            }
        }
    }
}