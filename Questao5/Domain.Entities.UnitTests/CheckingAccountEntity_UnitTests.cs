namespace Domain.Entities.UnitTests
{
    public class CheckingAccountEntity_UnitTests
    {
        [Fact]
        public void GivenNewEntityFlagAsTrue_ShouldGenerateRequiredFields()
        {
            //arrange
            //act
            var entity = new CheckingAccountEntity(true, "holdername");

            //assert
            Assert.True(entity.Active);
            Assert.True(entity.Number> 1000);
            Assert.True(entity.Number<999999999);
            Assert.True(entity.IdCheckingAccount != null);
        }

        [Fact]
        public void GivenNewEntityFlagAsTrueAndHolderNameNull_ShouldThrowException()
        {
            //arrange
            //act
            Assert.Throws<ArgumentException>(() =>
            {
                new CheckingAccountEntity(true, null);
            }); 
        }

        [Fact]
        public void GivenNewEntityFlagAsFalseAndCorrectParams_ShouldGenerateCorrectEntity()
        {
            //arrange
            var id = new Guid("205d8e36-d594-4b79-ab31-b37a15dec746");
            var name = "holdername";
            var number = 12345;
            var active = false;

            //act
            var entity = new CheckingAccountEntity(false, name, id, number, active);

            //assert
            Assert.Equal(active, entity.Active);
            Assert.Equal(name, entity.HolderName);
            Assert.Equal(number, entity.Number);
            Assert.Equal(id, entity.IdCheckingAccount);
        }
    }
}