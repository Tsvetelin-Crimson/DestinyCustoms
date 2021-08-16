using DestinyCustoms.Infrastructure;
using DestinyCustoms.Areas.Admin.Models.Home;
using DestinyCustoms.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DestinyCustoms.Tests.Controllers
{
    using static Common.WebConstants;
    using static Data.Weapons;
    using static Data.Armors;

    public class AdminHomeControllerTests
    {
        [Fact]
        public void MostRecentWeaponsWithAdminUserReturnsViewWithCorrectModel()
            => MyController<Areas.Admin.Controllers.HomeController>
                .Instance(controller => controller
                                    .WithData(ThirtyBlankWeapons())
                                    .WithUser(adminRoleName))
                .Calling(c => c.MostRecentWeapons())
                .ShouldReturn()
                .View(view => view
                            .WithModelOfType<MostRecentItemsViewModel>()
                            .Passing(m =>
                            {
                                Assert.Equal(AdminHomePageNumberOfItems, m.Items.Count);
                                Assert.Equal(nameof(WeaponsController).RemoveControllerFromString(), m.ItemLocation);
                            }));

        [Fact]
        public void MostRecentArmorsWithAdminUserReturnsViewWithCorrectModel()
            => MyController<Areas.Admin.Controllers.HomeController>
                .Instance(controller => controller
                                    .WithData(ThirtyBlankArmors())
                                    .WithUser(adminRoleName))
                .Calling(c => c.MostRecentArmors())
                .ShouldReturn()
                .View(view => view
                            .WithModelOfType<MostRecentItemsViewModel>()
                            .Passing(m =>
                            {
                                Assert.Equal(AdminHomePageNumberOfItems, m.Items.Count);
                                Assert.Equal(nameof(ArmorsController).RemoveControllerFromString(), m.ItemLocation);
                            }));
    }
}
