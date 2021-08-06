using System.Linq;
using DestinyCustoms.Controllers;
using DestinyCustoms.Models.Home;
using MyTested.AspNetCore.Mvc;
using Xunit;

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
                    .WithData(FiveBlankArmors())
                    .AndAlso()
                    .WithData(FiveBlankWeaponsWithWeaponClass()))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<HomeViewModel>()
                    .Passing(m =>
                    {
                        Assert.Equal(4, m.Armors.Count());
                        Assert.Equal(4, m.Weapons.Count());
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
