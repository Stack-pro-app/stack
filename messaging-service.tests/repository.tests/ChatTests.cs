using Amazon.S3;
using AutoMapper;
using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.Repository;
using System.Threading.Tasks;
using Moq;
using Bogus;
using Microsoft.EntityFrameworkCore;
namespace messaging_service.tests.repository.tests
{
    public class FakeDataGenerator
    {
        public static List<Chat> GenerateFakeChats(int count)
        {
            var faker = new Faker<Chat>()
                .RuleFor(c => c.Id, f => f.IndexVariable++)
                .RuleFor(c => c.MessageId, f => f.Random.Guid())
                .RuleFor(c => c.UserId, f => f.Random.Int(1, 100))
                .RuleFor(c => c.ChannelId, f => f.Random.Int(1, 10))
                .RuleFor(c => c.Message, f => f.Lorem.Sentence())
                .RuleFor(c => c.Is_deleted, f => f.Random.Bool())
                .RuleFor(c => c.Created_at, f => f.Date.Past())
                .RuleFor(c => c.Attachement_Url, f => f.Internet.Url())
                .RuleFor(c => c.Attachement_Name, f => f.System.FileName())
                .RuleFor(c => c.Attachement_Key, f => f.Random.AlphaNumeric(10))
                .FinishWith((f, chat) =>
                {
                    chat.Modified_at = f.Date.Past();
                    chat.ParentId = f.Random.Int(1, 100);
                });

            return faker.Generate(count);
        }
        public static Chat GenerateFakeChat()
        {
            var faker = new Faker<Chat>()
                .RuleFor(c => c.Id, f => f.IndexVariable++)
                .RuleFor(c => c.MessageId, f => f.Random.Guid())
                .RuleFor(c => c.UserId, f => f.Random.Int(1, 100))
                .RuleFor(c => c.ChannelId, f => f.Random.Int(1, 10))
                .RuleFor(c => c.Message, f => f.Lorem.Sentence())
                .RuleFor(c => c.Is_deleted, f => f.Random.Bool())
                .RuleFor(c => c.Created_at, f => f.Date.Past())
                .RuleFor(c => c.Attachement_Url, f => f.Internet.Url())
                .RuleFor(c => c.Attachement_Name, f => f.System.FileName())
                .RuleFor(c => c.Attachement_Key, f => f.Random.AlphaNumeric(10))
                .FinishWith((f, chat) =>
                {
                    chat.Modified_at = f.Date.Past();
                    chat.ParentId = f.Random.Int(1, 100);
                });

            return faker;
        }
    }
    public class ChatTests
    {
        [Fact]
        public async Task Add_AddsToDatabase()
        {
            //Arrange
            var chatSet = new Mock<DbSet<Chat>>();
            var context = new Mock<AppDbContext>();
            context.SetupGet(db => db.Chats).Returns(chatSet.Object);
            context.Setup(db => db.SaveChangesAsync(default)).Returns(Task.FromResult(0));
            var s3 = Mock.Of<IAmazonS3>();
            var mapper = Mock.Of<IMapper>();
            var repository = new ChatRepository(context.Object,mapper,s3);
            Chat chat = FakeDataGenerator.GenerateFakeChat();
            //Act
            await repository.CreateChatAsync(chat);

            //Assert
            chatSet.Verify(dbSet=>dbSet.Add(chat),Times.Once);
            context.Verify(db=>db.SaveChangesAsync(default),Times.Once);
        }

    }
}
