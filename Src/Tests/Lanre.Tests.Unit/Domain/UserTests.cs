namespace Lanre.Tests.Unit.Domain
{
    using FluentAssertions;
    using Lanre.Domain.Users;
    using Xunit;

    public class UserTests
    {
        
        [InlineData("53311435B", "    ", false)]
        [InlineData("", "hola", false)]
        [InlineData("Fulanito", "Menganito", true)]
        [Theory]
        public void TestIfICanCreateAUserValid(string firstname, string surname, bool expectedResult)
        {

            var aUserCreated = User.Create(firstname, surname);
            aUserCreated.IsSuccess.Should().Be(expectedResult);
        }
    }
}