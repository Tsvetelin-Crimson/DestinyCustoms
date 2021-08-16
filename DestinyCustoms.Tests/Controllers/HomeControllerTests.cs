using System.Linq;
using MyTested.AspNetCore.Mvc;
using Xunit;
using DestinyCustoms.Controllers;
using DestinyCustoms.Models.Home;

namespace DestinyCustoms.Tests.Controllers
{
    using static Data.Armors;
    using static Data.Weapons;

    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnsCorrectViewWithCorrectAmountOfArmorsInTheModel()
            => MyController<HomeController>
                .Instance(controller => controller
                    .WithData(TenBlankArmors())
                    .AndAlso()
                    .WithData(TenBlankWeapons()))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<HomeViewModel>()
                    .Passing(m =>
                    {
                        Assert.Equal(6, m.Armors.Count());
                        Assert.Equal(6, m.Weapons.Count());
                    }));

        [Fact]
        public void ErrorReturnsView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
