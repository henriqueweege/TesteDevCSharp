using Domain.Commands.CheckingAccountCommands;
using Domain.Entities;
using Domain.Models;

namespace Domain.Converters.UnitTests
{
    public class CheckingAccountConverter_UnitTests
    {
        public CheckingAccountConverter Converter { get; set; }
        public string HolderName { get; set; }
        public int Number { get; set; }
        public Guid Id { get; set; }
        public CheckingAccountConverter_UnitTests()
        {
            Converter= new CheckingAccountConverter();
            HolderName = "HolderName";
            Number = 12345;
            Id = Guid.NewGuid();
        }


        [Fact]
        public void GivenCommand_ConvertFromCommandCreateToEntity_SouldReturnCorrectEntity()
        {

            //arrange
            var command = new CreateCheckingAccountCommand()
            {
                HolderName = HolderName
            };

            //act
            var entity = Converter.ConvertFromCommandCreateToEntity(command);

            //assert
            Assert.True(entity.Active);
            Assert.True(entity.Number > 1000);
            Assert.False(string.IsNullOrWhiteSpace(entity.IdCheckingAccount.ToString()));
            Assert.Equal(HolderName, entity.HolderName);

        }

        [Fact]
        public void GivenEntity_ConvertFromEntityToModel_SouldReturnCorrectModel()
        {

            //arrange
            var entity = new CheckingAccountEntity(true, HolderName);


            //act
            var model = Converter.ConvertFromEntityToModel(entity);

            //assert
            Assert.True(model.ativo);
            Assert.True(model.numero > 1000);
            Assert.True(model.idcontacorrente is not null);
            Assert.Equal(HolderName, model.nome);

        }

        [Fact]
        public void GivenModel_ConvertFromModelToEntity_SouldReturnCorrectModel()
        {

            //arrange

            var model = new CheckingAccountModel() 
            {
                nome = HolderName,
                ativo = true,
                numero = Number,
                idcontacorrente= Id.ToString()
            };

            //act
            var entity = Converter.ConvertFromModelToEntity(model);

            //assert
            Assert.True(entity.Active);
            Assert.Equal(Number, entity.Number);
            Assert.Equal(HolderName, entity.HolderName);
            Assert.Equal(Id.ToString(), entity.IdCheckingAccount.ToString());

        }

        [Fact]
        public void GivenListOfModels_ConvertFromModelToEntity_SouldReturnCorrectModel()
        {

            //arrange
            var models = new List<CheckingAccountModel>();
            var qnt = 10;

            for(var i =0; i < qnt; i++)
            {
                models.Add(new CheckingAccountModel()
                {
                    nome = HolderName,
                    ativo = true,
                    numero = Number,
                    idcontacorrente = Id.ToString()
                });
            }

            //act
            var entities = Converter.ConvertFromModelToEntity(models);

            //assert
            Assert.Equal(qnt, entities.Count());
            Assert.True(entities[0].Active);
            Assert.Equal(Number, entities[0].Number);
            Assert.Equal(HolderName, entities[0].HolderName);
            Assert.Equal(Id.ToString(), entities[0].IdCheckingAccount.ToString());


        }
    }
}