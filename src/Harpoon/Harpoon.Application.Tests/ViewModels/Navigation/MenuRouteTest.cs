using NUnit.Framework;

namespace Harpoon.Application.ViewModels.Navigation
{
    [TestFixture]
    public class MenuRouteTest
    {
        [Test]
        public void TestEqual()
        {
            var route1 = new MenuRoute("Controller", "Action");
            var route2 = new MenuRoute("CONTrolleR", "actION");
            Assert.AreEqual(route1, route2);

            route1 = new MenuRoute("Controller", "Action", "Value");
            route2 = new MenuRoute("CONTrolleR", "ACTioN", "VaLUE");
            Assert.AreEqual(route1, route2);
        }
        
        [Test]
        public void TestNotEqual()
        {
            var route1 = new MenuRoute("ControllerNNN", "Action");
            var route2 = new MenuRoute("Controller", "Action");
            Assert.AreNotEqual(route1, route2);

            route1 = new MenuRoute("Controller", "ActionNNN");
            route2 = new MenuRoute("Controller", "Action");
            Assert.AreNotEqual(route1, route2);

            route1 = new MenuRoute("Controller", "Action", "ValueNNN");
            route2 = new MenuRoute("Controller", "Action", "Value");
            Assert.AreNotEqual(route1, route2);

            route1 = new MenuRoute("Controller", "Action", "Value");
            route2 = new MenuRoute("Controller", "Action");
            Assert.AreNotEqual(route1, route2);
        }

    }
}