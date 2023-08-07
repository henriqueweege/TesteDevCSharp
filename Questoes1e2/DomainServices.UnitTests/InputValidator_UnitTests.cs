using DomainServices.Utils;

namespace DomainServices.UnitTests;

public class InputValidator_UnitTests
{
    #region Validate Number Input
    [Theory]
    [InlineData("justLetters")]
    [InlineData("LettersAndNumbers12345")]
    [InlineData("12345*&^")]
    public void GivenNumberInputThatIsNotJustNumber_ValidateNumberInput_ShouldReturnFalse(string numberInput)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateNumberInput(numberInput);


        //assert
        Assert.False(isValid);

    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void GivenNumberInputThatIsEmptyOrWhiteSpace_ValidateNumberInput_ShouldReturnFalse(string numberInput)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateNumberInput(numberInput);


        //assert
        Assert.False(isValid);
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("123,12")]
    [InlineData("123.12")]
    public void GivenNumberInputThatIsJustNumber_ValidateNumberInput_ShouldReturnTrue(string numberInput)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateNumberInput(numberInput);


        //assert
        Assert.True(isValid);
    }
    #endregion

    #region Validate Name Input

    [Theory]
    [InlineData("number1 Letters")]
    [InlineData("LettersAndNumbers12345")]
    [InlineData("Letters And Numbers 12345 ")]
    [InlineData("Letters And SpecialChar *&^ ")]
    [InlineData("12345*&^")]
    [InlineData("12345 *&^")]
    public void GivenNameThatIsNotJustLetters_ValidateNameInput_ShouldReturnFalse(string input)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateNameInput(input);

        //assert
        Assert.False(isValid);

    }


    [Theory]
    [InlineData("name")]
    [InlineData("firstname lastname")]
    [InlineData("firstname middlename lastname")]
    public void GivenNameCorrect_ValidateNameInput_ShouldReturnTrue(string input)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateNameInput(input);


        //assert
        Assert.True(isValid);
    }

    #endregion

    #region Validate Team Name Input

    [Theory]
    [InlineData("number1 Letters")]
    [InlineData("LettersAndNumbers12345")]
    [InlineData("Letters And Numbers 12345 ")]
    [InlineData("12345*&^")]
    [InlineData("12345 *&^")]
    public void GivenNameThatIsNotJustLetters_ValidateTeamNameInput_ShouldReturnFalse(string input)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateTeamNameInput(input);


        //assert
        Assert.False(isValid);

    }


    [Theory]
    [InlineData("name")]
    [InlineData("firstname lastname")]
    [InlineData("Letters And SpecialChar *&^ ")]
    [InlineData("firstname middlename lastname")]
    public void GivenNameCorrect_ValidateTeamNameInput_ShouldReturnTrue(string input)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateTeamNameInput(input);


        //assert
        Assert.True(isValid);
    }

    #endregion

    #region Validate Initial Deposit Option

    [Theory]
    [InlineData("1")]
    [InlineData("%")]
    public void GivenInputThatIsNotLetters_ValidateInitialDepositOption_ShouldReturnFalse(string initialDepositInput)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateInitialDepositOptionInput(initialDepositInput);


        //assert
        Assert.False(isValid);

    }

    [Theory]
    [InlineData("aa")]
    [InlineData("aaa")]
    public void GivenInputThatIsMoreThanOneLetter_ValidateInitialDepositOption_ShouldReturnFalse(string initialDepositInput)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateInitialDepositOptionInput(initialDepositInput);


        //assert
        Assert.False(isValid);

    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void GivenInputThatIsNullEmptyOrWhiteSpace_ValidateInitialDepositOption_ShouldReturnFalse(string initialDepositInput)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateInitialDepositOptionInput(initialDepositInput);


        //assert
        Assert.False(isValid);

    }

    [Theory]
    [InlineData("a")]
    [InlineData("b")]
    [InlineData("c")]
    public void GivenInputThatIsNotSOrN_ValidateInitialDepositOption_ShouldReturnFalse(string initialDepositInput)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateInitialDepositOptionInput(initialDepositInput);


        //assert
        Assert.False(isValid);

    }

    [Theory]
    [InlineData("n")]
    [InlineData("s")]
    public void GivenInputThatIsSOrN_ValidateInitialDepositOption_ShouldReturnTrue(string initialDepositInput)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateInitialDepositOptionInput(initialDepositInput);


        //assert
        Assert.True(isValid);

    }

    #endregion

    #region Validate DepositWithdrawn Input

    [Theory]
    [InlineData("-1")]
    [InlineData("-1,4")]
    public void GivenInputThatIsNegative_ValidateInitialDepositWithdrawnInput_ShouldReturnFalse(string initialDepositInput)
    {
        //arrange
        //act
        bool isValid = InputValidator.ValidateDepositWithdrawnInput(initialDepositInput);


        //assert
        Assert.False(isValid);

    }


    #endregion
}