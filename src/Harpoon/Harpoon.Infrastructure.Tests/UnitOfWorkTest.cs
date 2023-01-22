using System;
using System.Data.Entity;
using System.Threading;
using NUnit.Framework;

namespace Harpoon.Infrastructure
{
    [TestFixture]
    public class UnitOfWorkTest
    {
        private static Exception threadException;

        [SetUp]
        public void SetUp()
        {
            threadException = null;
        }

        [Test]
        public void TestUnitOfWorkScope()
        {
            var repositoryA = new FakeRepository();
            var dbContext_1 = repositoryA.GetDbContext();
            Assert.IsNotNull(dbContext_1);

            Assert.IsFalse(UnitOfWork.HasDbContext());
            Assert.IsNull(UnitOfWork.GetDbContext());

            using (new UnitOfWork())
            {
                Assert.IsTrue(UnitOfWork.HasDbContext());
                var uowDbContext = UnitOfWork.GetDbContext();
                Assert.IsNotNull(uowDbContext);

                Assert.AreNotSame(uowDbContext, dbContext_1);

                var dbContext_2 = repositoryA.GetDbContext();
                Assert.NotNull(dbContext_2);
                Assert.AreSame(uowDbContext, dbContext_2);

                var repositoryB = new FakeRepository();
                var dbContext_3 = repositoryB.GetDbContext();
                Assert.NotNull(dbContext_3);
                Assert.AreSame(uowDbContext, dbContext_3);
            }

            Assert.IsFalse(UnitOfWork.HasDbContext());
            Assert.IsNull(UnitOfWork.GetDbContext());
        }

        [Test]
        public void TestThreadIsolation()
        {
            using (new UnitOfWork())
            {
                Assert.IsTrue(UnitOfWork.HasDbContext());
                var dbContext = UnitOfWork.GetDbContext();
                Assert.IsNotNull(dbContext);

                var thread = new Thread(RunThread);
                thread.Start(dbContext);
                thread.Join();

                if (threadException != null)
                {
                    throw threadException;
                }
            }
        }

        private static void RunThread(object param)
        {
            try
            {
                var otherDbContext = (DbContext) param;

                using (new UnitOfWork())
                {
                    Assert.IsTrue(UnitOfWork.HasDbContext());
                    var dbContext = UnitOfWork.GetDbContext();
                    Assert.IsNotNull(dbContext);

                    Assert.AreNotSame(dbContext, otherDbContext);
                }
            }
            catch (Exception ex)
            {
                threadException = ex;
            }
        }

        [Test]
        public void TestNestedUnitOfWork()
        {
            using (new UnitOfWork())
            {
                Assert.IsTrue(UnitOfWork.HasDbContext());
                var dbContext_1 = UnitOfWork.GetDbContext();
                Assert.IsNotNull(dbContext_1);

                using (new UnitOfWork())
                {
                    Assert.IsTrue(UnitOfWork.HasDbContext());
                    var dbContext_2 = UnitOfWork.GetDbContext();
                    Assert.IsNotNull(dbContext_1);

                    Assert.AreSame(dbContext_1, dbContext_2);
                }

                Assert.IsTrue(UnitOfWork.HasDbContext());
            }

            Assert.IsFalse(UnitOfWork.HasDbContext());
        }

        [Test]
        public void TestCantMultipleCommit()
        {
            using(var uow = new UnitOfWork())
            {
                Assert.IsFalse(uow.IsCommited);

                uow.Commit();
                Assert.IsTrue(uow.IsCommited);

                // expect exception
                try
                {
                    uow.Commit();
                    Assert.Fail("Exception not found");
                }
                catch (InfrastructureException)
                {
                    // OK
                }
            }
        }

    }
}
