using Domain.Commands.CheckingAccountCommands;
using Domain.Entities;
using Domain.Models;
using Tools;

namespace Domain.Converters.UnitTests
{
    public class TransactionConverter_UnitTests
    {
        private TransactionConverter Converter { get; set; }
        private Guid Id { get; set; }
        private Guid IdCheckingAccount { get; set; }
        private string  Type { get; set; }
        private double Value { get; set; }
        private DateTime Date { get; set; }
        public TransactionConverter_UnitTests()
        {
            Converter= new TransactionConverter();
            Id = Guid.NewGuid();
            IdCheckingAccount = Guid.NewGuid();
            Type = "C";
            Value = 0;
            Date = DateTime.Now;
        }

        [Fact]
        public void GivenEntity_ConvertFromEntityToModel_SouldReturnCorrectModel()
        {

            //arrange
            var entity = new TransactionEntity(Id,IdCheckingAccount,Type,0, Date, false);


            //act
            var model = Converter.ConvertFromEntityToModel(entity);

            //assert
            Assert.Equal(Id.ToString(), model.idmovimento);
            Assert.Equal(IdCheckingAccount.ToString(), model.idcontacorrente);
            Assert.Equal(Date.Date, model.datamovimento.Date);
            Assert.Equal(Value, model.valor);
            Assert.Equal(Enums.ETransactionType.Credit.ToDescription(), model.tipomovimento);

        }

        [Fact]
        public void GivenModel_ConvertFromModelToEntity_SouldReturnCorrectModel()
        {

            //arrange
            var model = new TransactionModel() 
            {
                idmovimento = Id.ToString(),
                idcontacorrente = IdCheckingAccount.ToString(),
                datamovimento = Date,
                tipomovimento = Type,
                valor = Value
            };

            //act
            var entity = Converter.ConvertFromModelToEntity(model);

            //assert
            Assert.Equal(Id, entity.Id);
            Assert.Equal(IdCheckingAccount, entity.IdCheckingAccount);
            Assert.Equal(Date.Date, entity.Date.Date);
            Assert.Equal(Value, entity.Value);
            Assert.Equal(Enums.ETransactionType.Credit, entity.Type);

        }


        [Fact]
        public void GivenCommand_ConvertFromCommandCreateToEntity_SouldReturnCorrectEntity()
        {

            //arrange
            var command = new ExecuteTransactionCommand()
            {
                Id = Id,
                IdCheckingAccount = IdCheckingAccount,
                Type = char.Parse(Type),
                Value = Value
            };

            //act
            var entities = Converter.ConvertFromCommandCreateToEntity(command);

            //assert
            Assert.Equal(Id, entities.Id);
            Assert.Equal(IdCheckingAccount, entities.IdCheckingAccount);
            Assert.Equal(Date.Date, entities.Date.Date);
            Assert.Equal(Value, entities.Value);
            Assert.Equal(Enums.ETransactionType.Credit, entities.Type);

        }


        [Fact]
        public void GivenListOfModels_ConvertFromModelToEntity_SouldReturnCorrectModel()
        {

            //arrange
            var models = new List<TransactionModel>();
            var qnt = 10;

            for(var i =0; i < qnt; i++)
            {
                models.Add(new TransactionModel()
                {
                    idmovimento = Id.ToString(),
                    idcontacorrente = IdCheckingAccount.ToString(),
                    datamovimento = Date,
                    tipomovimento = Type,
                    valor = Value
                });
            }

            //act
            var entities = Converter.ConvertFromModelToEntity(models);

            //assert
            Assert.Equal(qnt, entities.Count());
            Assert.Equal(Id, entities[0].Id);
            Assert.Equal(IdCheckingAccount, entities[0].IdCheckingAccount);
            Assert.Equal(Date.Date, entities[0].Date.Date);
            Assert.Equal(Value, entities[0].Value);
            Assert.Equal(Enums.ETransactionType.Credit, entities[0].Type);


        }
    }
}