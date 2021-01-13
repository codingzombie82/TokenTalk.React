using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenTalk.Models.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TalkRepositoryTest
    {
        [TestMethod]
        public async Task TalkRepositoryAllMethod()
        {
            #region [0] DbContextOptions<T> Object Creation and ILoggerFactory Object Creation
            //[0] DbContextOptions<T> Object Creation and ILoggerFactory Object Creation
            var options = new DbContextOptionsBuilder<TalkAppDbContext>()
            //.UseInMemoryDatabase(databaseName: $"TokenTalk{Guid.NewGuid()}").Options;
            .UseSqlServer("server=(localdb)\\mssqllocaldb;database=TokenTalk;integrated security=true;").Options;

            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            #endregion

            #region [1] AddAsync() Method Test
            //[1] AddAsync() Method Test
            //[1][1] Repository 클래스를 사용하여 저장
            using (var context = new TalkAppDbContext(options))
            {
                context.Database.EnsureCreated(); // 데이터베이스가 만들어져 있는지 확인

                //[A] Arrange: 1번 데이터를 아래 항목으로 저장합니다. 
                var repository = new TalkRepository(context, factory);
                var model = new Talk { Title = "C# 교과서", Description = "C# 기초를 다룹니다." };

                //[B] Act: AddAsync() 메서드 테스트
                await repository.AddAsync(model); // Id: 1
            }
            //[1][2] DbContext 클래스를 통해서 개수 및 레코드 확인 
            using (var context = new TalkAppDbContext(options))
            {
                //[C] Assert: 현재 총 데이터 개수가 1개인 것과, 1번 데이터의 Title이 "C# 교과서"인지 확인합니다. 
                Assert.AreEqual(1, await context.Talks.CountAsync());

                var model = await context.Talks.Where(n => n.Id == 1).SingleOrDefaultAsync();
                Assert.AreEqual("C# 교과서", model.Title);
            }
            #endregion

            #region [2] GetAllAsync() Method Test
            //[2] GetAllAsync() Method Test
            using (var context = new TalkAppDbContext(options))
            {
                // 트랜잭션 관련 코드는 InMemoryDatabase 공급자에서는 지원 X
                // using (var transaction = context.Database.BeginTransaction()) { transaction.Commit(); }

                //[A] Arrange
                var repository = new TalkRepository(context, factory);
                var model = new Talk { Title = "ASP.NET Core를 다루는 기술", Description = "ASP.NET 기초" };

                //[B] Act
                await repository.AddAsync(model); // Id: 2
                await repository.AddAsync(new Talk { Title = "타입스크립트", Description = "TypeScript" }); // Id: 3
            }
            using (var context = new TalkAppDbContext(options))
            {
                //[C] Assert
                var repository = new TalkRepository(context, factory);
                var models = await repository.GetAllAsync();
                Assert.AreEqual(3, models.Count()); // TotalRecords: 3
            }
            #endregion

            #region [3] GetByIdAsync() Method Test
            //[3] GetByIdAsync() Method Test
            using (var context = new TalkAppDbContext(options))
            {
                // Empty
            }
            using (var context = new TalkAppDbContext(options))
            {
                var repository = new TalkRepository(context, factory);
                var model = await repository.GetByIdAsync(2);
                Assert.IsTrue(model.Title.Contains("ASP.NET"));
                Assert.AreEqual("ASP.NET 기초", model.Description);
            }
            #endregion

            #region [4] UpdateAsync() Method Test
            //[4] UpdateAsync() Method Test
            using (var context = new TalkAppDbContext(options))
            {
                // Empty
            }
            using (var context = new TalkAppDbContext(options))
            {
                var repository = new TalkRepository(context, factory);
                var model = await repository.GetByIdAsync(2);

                model.Title = "ASP.NET & Core를 다루는 기술"; // Modified
                model.Description = "웹 개발 기술의 집합체";
                await repository.UpdateAsync(model);

                var updateModel = await repository.GetByIdAsync(2);

                Assert.IsTrue(updateModel.Title.Contains("&"));
                Assert.AreEqual("ASP.NET & Core를 다루는 기술", updateModel.Title);
                Assert.AreEqual("웹 개발 기술의 집합체",
                    (await context.Talks.Where(m => m.Id == 2).SingleOrDefaultAsync())?.Description);
            }
            #endregion

            #region [5] DeleteAsync() Method Test
            //[5] DeleteAsync() Method Test
            using (var context = new TalkAppDbContext(options))
            {
                // Empty
            }
            using (var context = new TalkAppDbContext(options))
            {
                var repository = new TalkRepository(context, factory);
                await repository.DeleteAsync(2);

                Assert.AreEqual(2, (await context.Talks.CountAsync()));
                Assert.IsNull(await repository.GetByIdAsync(2));
            }
            #endregion
        }

    }
}
