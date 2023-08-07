using Domain.Models;
using Domain.Repositories.Base;
using Infrastructure.Data;

namespace Domain.IntegrationTests
{
    public class BaseRepository_IntegrationTests : IDisposable
    {

        private SQLiteDbContext DbContext { get; set; }
        private BaseRepository<CheckingAccountModel> Repository { get; set; }
        private Random Random { get; set; }
        public BaseRepository_IntegrationTests()
        {
            DbContext = new SQLiteDbContext();
            DbContext.CreateDatabase();

            Repository = new BaseRepository<CheckingAccountModel>(DbContext);
            Random = new Random();
        }

        [Fact]
        public void GivenCorrectObject_ShouldSave()
        {
            //arrange

            var checkingAccountModel = new CheckingAccountModel()
            {
                idcontacorrente = Guid.NewGuid().ToString(),
                numero = Random.Next(),
                nome = "nome",
                ativo = true
            };

            //act
            var result = Repository.Save(checkingAccountModel);

            //assert
            var getAll = Repository.GetAll();
            Assert.True(result > 0);
            Assert.True(getAll.Count() > 0);
        }

        [Fact]
        public void CallToGetAll_ShouldReturnMoreThanOneObject()
        {
            //arrange

            for (var i = 0; i < 2; i++)
            {
                Repository.Save(new CheckingAccountModel()
                {
                    idcontacorrente = Guid.NewGuid().ToString(),
                    numero = Random.Next(),
                    nome = "nome",
                    ativo = true
                });
            }

            //act
            var getAll = Repository.GetAll();

            //assert
            Assert.True(getAll.Count() > 1);
        }

        [Fact]
        public void CallToGetById_ShouldReturnCorrectObject()
        {
            //arrange

            var id = Guid.NewGuid();


            Repository.Save(new CheckingAccountModel()
            {
                idcontacorrente = id.ToString(),
                numero = Random.Next(),
                nome = "nome",
                ativo = true
            });

            //act
            var getById = Repository.GetById(id.ToString());

            //assert
            Assert.Equal(id.ToString(), getById.idcontacorrente);
        }

        [Fact]
        public void CallToUpdate_ShouldUpdateObject()
        {
            //arrange
            var id = Guid.NewGuid();
            var nameToUpdate = "nome atualizado";

            var obj = new CheckingAccountModel()
            {
                idcontacorrente = id.ToString(),
                numero = Random.Next(),
                nome = "nome",
                ativo = true
            };
            Repository.Save(obj);

            obj.nome = nameToUpdate;

            //act
            var updated = Repository.Update(obj);

            //assert
            Assert.True(updated);

            var objUpdated = Repository.GetById(id.ToString());
            Assert.Equal(nameToUpdate, objUpdated.nome);
        }

        [Fact]
        public void CallToDelete_ShouldDeleteObject()
        {
            //arrange
            var id = Guid.NewGuid();

            var obj = new CheckingAccountModel()
            {
                idcontacorrente = id.ToString(),
                numero = Random.Next(),
                nome = "nome",
                ativo = true
            };
            Repository.Save(obj);


            //act
            var deleted = Repository.Delete(obj);

            //assert
            Assert.True(deleted);

            var tryGet = Repository.GetById(id.ToString());
            Assert.True(tryGet is null);
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}