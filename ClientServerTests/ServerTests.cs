using DBLesson.Abstracts;
using DBLesson.Models;
using DBLesson.Services;
using Moq;
using System.Net;
using System.Runtime.Intrinsics.X86;

namespace ClientServerTests
{
    public class ServerTests
    {
        [SetUp]
        public async Task Setup()
        {
            using (ChatContext ctx = new ChatContext())
            {
                ctx.Messages.RemoveRange(ctx.Messages);
                ctx.Users.RemoveRange(ctx.Users);
                ctx.SaveChanges();
            }
            var mock = new MockServerMessageSource();
            await mock.Server.Start();
        }

        [TearDown] 
        public void Teardown() 
        {
            using (ChatContext ctx = new ChatContext())
            {
                ctx.Messages.RemoveRange(ctx.Messages);
                ctx.Users.RemoveRange(ctx.Users);
                ctx.SaveChanges();
            }
        }

        [Test]
        public async Task RegisterUserTest()
        {            
            using (ChatContext ctx = new ChatContext())
            {
                Assert.IsTrue(ctx.Users.Count() == 2, "Пользователи не созданы");

                var user1 = ctx.Users.FirstOrDefault(x => x.FullName == "Вася");
                var user2 = ctx.Users.FirstOrDefault(x => x.FullName == "Юля");
                Assert.IsNotNull(user1, "Пользователь не создан");
                Assert.IsNotNull(user2, "Пользователь не создан");

                
            }
        }

        [Test]
        public void MessagesCountTest()
        {
            using (ChatContext ctx = new ChatContext())
            {
                var user1 = ctx.Users.FirstOrDefault(x => x.FullName == "Вася");
                var user2 = ctx.Users.FirstOrDefault(x => x.FullName == "Юля");
                Assert.IsTrue(user1.MessagesFrom.Count == 1);
                Assert.IsTrue(user2.MessagesFrom.Count == 1);

                Assert.IsTrue(user1.MessagesTo.Count == 1);
                Assert.IsTrue(user2.MessagesTo.Count == 1);                
            }
        }

        [Test]
        public void MessagesInfoTest()
        {
            using (ChatContext ctx = new ChatContext())
            {
                var user1 = ctx.Users.FirstOrDefault(x => x.FullName == "Вася");
                var user2 = ctx.Users.FirstOrDefault(x => x.FullName == "Юля");
                var msg1 = ctx.Messages.FirstOrDefault(x => x.UserFrom == user1 && x.UserTo == user2);
                var msg2 = ctx.Messages.FirstOrDefault(x => x.UserFrom == user2 && x.UserTo == user1);
                Assert.AreEqual("От Юли", msg2.Text);
                Assert.AreEqual("От Васи", msg1.Text);
            }
        }
    }
}