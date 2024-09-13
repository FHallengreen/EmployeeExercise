using System.Globalization;
using EmployeeExercise.Domain.Enum;
using System;

namespace EmployeeExercise.Domain;

public class Employee
{
    private string _cpr;
    private string _firstName;
    private string _lastName;
    private Department _department;
    private double _baseSalary;
    private EducationalLevel _educationalLevel;
    private DateTime _dateOfBirth;
    private DateTime _employmentDate;
    private string _country;

    public Employee(string cpr, string firstName, string lastName, Department department, double baseSalary,
        EducationalLevel educationalLevel, DateTime dateOfBirth, DateTime employmentDate, string country)
    {
        SetCpr(cpr);
        SetFirstName(firstName);
        SetLastName(lastName);
        SetDepartment(department);
        SetBaseSalary((int)baseSalary);
        SetEducationLevel(educationalLevel);
        SetBirthDate(dateOfBirth);
        SetEmploymentDate(employmentDate);
        SetCountry(country);
    }

    public Employee()
    {
    }


    public string GetCpr()
    {
        return _cpr;
    }

    public void SetCpr(string cpr)
    {
        if (cpr.Length != 10 || !IsDigit(cpr))
            throw new ArgumentException("CPR must be 10 characters long and only contain numbers");
        _cpr = cpr;
    }

    public string GetFirstName()
    {
        return _firstName;
    }

    public void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 1 || firstName.Length > 30 ||
            !IsValidName(firstName))
            throw new ArgumentException(
                "First Name is invalid. Name must be between 1-30 characters. The characters can be alphabetic, spaces or a dash");
        _firstName = firstName;
    }

    public string GetLastName()
    {
        return _lastName;
    }

    public void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 1 || lastName.Length > 30 ||
            !IsValidName(lastName))
            throw new ArgumentException(
                "Last Name is invalid. Name must be between 1-30 characters. The characters can be alphabetic, spaces or a dash");
        _lastName = lastName;
    }

    public string GetDepartment()
    {
        switch (_department)
        {
            case Department.GeneralServices:
                return "General Services";
            case Department.It:
                return "IT";
            case Department.Hr:
                return "HR";
            default:
                return _department.ToString();
        }
    }

    public void SetDepartment(Department department)
    {
        if (!System.Enum.IsDefined(typeof(Department), department))
        {
            throw new ArgumentException("Invalid department value.", nameof(department));
        }

        _department = department;
    }

    public double GetBaseSalary()
    {
        return _baseSalary;
    }

    public void SetBaseSalary(double baseSalary)
    {
        if (baseSalary is < 20000 or > 100000)
            throw new ArgumentException("Invalid base salary range");
        _baseSalary = baseSalary;
    }

    public string GetEducationLevel()
    {
        return _educationalLevel.ToString();
    }

    public void SetEducationLevel(EducationalLevel educationalLevel)
    {
        if (!System.Enum.IsDefined(typeof(EducationalLevel), educationalLevel))
        {
            throw new ArgumentException("Invalid educational level.", nameof(educationalLevel));
        }

        _educationalLevel = educationalLevel;
    }

    public DateTime GetBirthDate()
    {
        return _dateOfBirth;
    }

    public void SetBirthDate(DateTime dateOfBirth)
    {
        if (dateOfBirth > DateTime.Now.AddYears(-18))
            throw new ArgumentException("Employee must be at least 18 years old");
        _dateOfBirth = dateOfBirth;
    }

    public DateTime GetEmploymentDate()
    {
        return _employmentDate;
    }

    public void SetEmploymentDate(DateTime employmentDate)
    {
        if (employmentDate > DateTime.Now)
            throw new ArgumentException("Employment date cannot be in the future");
        _employmentDate = employmentDate;
    }

    public string GetCountry()
    {
        return _country;
    }

    public void SetCountry(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException(
                "Country is invalid.");
        _country = country;
    }


    public double GetSalary()
    {
        return _baseSalary + (int)_educationalLevel * 1220;
    }

    public double GetDiscount()
    {
        var yearsEmployed = DateTime.Now.Year - _employmentDate.Year;
        return yearsEmployed * 0.5;
    }

    public int GetShippingCost()
    {
        var country = _country.ToLower();
        switch (country)
        {
            case "denmark" or "sweden" or "norway":
                return 0;
            case "finland" or "iceland":
                return 50;
            default:
                return 100;
        }
    }

    private static bool IsValidName(string name)
    {
        foreach (var c in name)
        {
            if (!char.IsLetter(c) && c != ' ' && c != '-')
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsDigit(string cpr)
    {
        foreach (var c in cpr)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }

        return true;
    }
}