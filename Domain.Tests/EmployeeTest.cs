using EmployeeExercise.Domain;
using EmployeeExercise.Domain.Enum;
using JetBrains.Annotations;

namespace EmployeeExercise.Service.Tests;

[TestSubject(typeof(Employee))]
public class EmployeeTest
{
    private readonly Employee _employee = new();

    [Theory]
    [InlineData("")]
    [InlineData("13552025802")]
    [InlineData("123456789")]
    [InlineData("1")]
    [InlineData("123456789A")]
    [InlineData("123456789!")]
    public void CprNumber_InvalidNumLengthOrChar(string number)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _employee.SetCpr(number));
    }

    [Theory]
    [InlineData("0123456789")]
    [InlineData("1234567890")]
    public void CprNumber_ValidNumLength(string number)
    {
        // Act
        _employee.SetCpr(number);

        // Assert
        Assert.Equal(number, _employee.GetCpr());
    }

    [Theory]
    [InlineData("")]
    [InlineData("ABCDEFGHIJKABCDEFGHIJKABCDEFGHI")]
    [InlineData("ABCDE123")]
    public void FirstName_InvalidString(string firstName)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _employee.SetFirstName(firstName));
    }

    [Theory]
    [InlineData("First-name")]
    [InlineData("Firstname")]
    [InlineData("First name")]
    [InlineData("F")]
    [InlineData("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")]
    public void FirstName_ValidString(string firstName)
    {
        // Act
        _employee.SetFirstName(firstName);

        // Assert
        Assert.Equal(firstName, _employee.GetFirstName());
    }

    [Theory]
    [InlineData("")]
    [InlineData("ABCDEFGHIJKABCDEFGHIJKABCDEFGHI")]
    [InlineData("ABCDE123")]
    public void LastName_InvalidString(string lastName)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _employee.SetLastName(lastName));
    }

    [Theory]
    [InlineData("First-name")]
    [InlineData("Firstname")]
    [InlineData("First name")]
    [InlineData("F")]
    [InlineData("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")]
    public void LastName_ValidString(string lastName)
    {
        // Act
        _employee.SetLastName(lastName);

        // Assert
        Assert.Equal(lastName, _employee.GetLastName());
    }

    [Theory]
    [InlineData("HR", Department.Hr)]
    [InlineData("Finance", Department.Finance)]
    [InlineData("IT", Department.It)]
    [InlineData("Sales", Department.Sales)]
    [InlineData("General Services", Department.GeneralServices)]
    public void Department_ReturnsCorrectString(string expected, Department department)
    {
        // Act
        _employee.SetDepartment(department);
        string result = _employee.GetDepartment();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData((Department)5)]
    [InlineData((Department)999)]
    [InlineData((Department)(-1))]
    public void SetDepartment_InvalidValues_ShouldThrowArgumentException(Department department)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _employee.SetDepartment(department));
    }

    [Theory]
    [InlineData(19999)]
    [InlineData(100001)]
    [InlineData(-5000)]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    public void BaseSalary_InvalidNumbers(int number)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _employee.SetBaseSalary(number));
    }

    [Theory]
    [InlineData(20000)]
    [InlineData(100000)]
    [InlineData(60000)]
    [InlineData(60000.20)]
    public void BaseSalary_ValidNumbers(int number)
    {
        // Act
        _employee.SetBaseSalary(number);

        // Assert
        Assert.Equal(number, _employee.GetBaseSalary());
    }


    [Theory]
    [InlineData("None", EducationalLevel.None)]
    [InlineData("Primary", EducationalLevel.Primary)]
    [InlineData("Secondary", EducationalLevel.Secondary)]
    [InlineData("Tertiary", EducationalLevel.Tertiary)]
    public void Education_ReturnsCorrectString(string expected, EducationalLevel educationalLevel)
    {
        // Act
        _employee.SetEducationLevel(educationalLevel);
        string result = _employee.GetEducationLevel();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData((EducationalLevel)4)]
    [InlineData((EducationalLevel)999)]
    [InlineData((EducationalLevel)(-1))]
    public void SetEducationLevel_InvalidValues_ShouldThrowArgumentException(EducationalLevel level)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _employee.SetEducationLevel(level));
    }

    [Fact]
    public void DateOfBirth_Below18()
    {
        // Arrange
        DateTime birthDay = DateTime.Now.AddYears(-17);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _employee.SetBirthDate(birthDay));
    }

    [Fact]
    public void DateOfBirth_OneDayBefore18_ShouldThrowException()
    {
        DateTime birthDay = DateTime.Now.AddYears(-18).AddDays(1);
        Assert.Throws<ArgumentException>(() => _employee.SetBirthDate(birthDay));
    }

    [Fact]
    public void DateOfBirth_Is18()
    {
        // Arrange
        DateTime birthDay = DateTime.Now.AddYears(-18);

        // Act
        _employee.SetBirthDate(birthDay);

        // Assert
        Assert.Equal(birthDay, _employee.GetBirthDate());
    }

    [Fact]
    public void DateOfBirth_Above18()
    {
        // Arrange
        DateTime birthDay = DateTime.Now.AddYears(-35);

        // Act
        _employee.SetBirthDate(birthDay);

        // Assert
        Assert.Equal(birthDay, _employee.GetBirthDate());
    }

    [Fact]
    public void InvalidDayOfEmployment()
    {
        // Arrange
        DateTime employment = DateTime.Now.AddDays(1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _employee.SetEmploymentDate(employment));
    }

    [Fact]
    public void ValidDayOfEmployment()
    {
        // Arrange
        DateTime employment = DateTime.Now;

        // Act
        _employee.SetEmploymentDate(employment);

        // Assert
        Assert.Equal(employment, _employee.GetEmploymentDate());
    }

    [Theory]
    [InlineData("Denmark")]
    [InlineData("Sweden")]
    [InlineData("Norway")]
    public void ValidCountries(string country)
    {
        // Act
        _employee.SetCountry(country);

        // Assert
        Assert.Equal(country, _employee.GetCountry());
    }

    [Theory]
    [InlineData("  ")]
    [InlineData("")]
    public void InValidCountries(string country)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _employee.SetCountry(country));
    }

    [Theory]
    [InlineData(20000, EducationalLevel.None, 20000)]
    [InlineData(100000, EducationalLevel.Tertiary, 103660)]
    [InlineData(50000, EducationalLevel.Primary, 51220)]
    [InlineData(69420, EducationalLevel.Secondary, 71860)]
    [InlineData(75000, EducationalLevel.Tertiary, 78660)]
    public void GetSalary_ValidNumbers(double baseSalary, EducationalLevel educationalLevel, double expected)
    {
        // Arrange
        _employee.SetBaseSalary(baseSalary);
        _employee.SetEducationLevel(educationalLevel);

        // Act
        double result = _employee.GetSalary();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0.5, 0)]
    [InlineData(2, 1)]
    [InlineData(5, 2.5)]
    [InlineData(25, 12.50)]
    public void GetDiscountEmployee(int years, double expected)
    {
        // Arrange
        DateTime yearsEmployed = DateTime.Today.AddYears(-years);
        _employee.SetEmploymentDate(yearsEmployed);

        // Act
        var result = _employee.GetDiscount();

        // Assert
        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData("Denmark", 0)]
    [InlineData("DENMARK", 0)]
    [InlineData("Sweden", 0)]
    [InlineData("norway", 0)]
    [InlineData("Finland", 50)]
    [InlineData("Iceland", 50)]
    [InlineData("Ghana", 100)]
    [InlineData("123347", 100)] // Edge case - treated as unrecognized country
    public void GetShippingCostResult(string country, int expected)
    {
        // Arrange
        _employee.SetCountry(country);

        // Act
        var result = _employee.GetShippingCost();

        // Assert
        Assert.Equal(expected, result);
    }
}