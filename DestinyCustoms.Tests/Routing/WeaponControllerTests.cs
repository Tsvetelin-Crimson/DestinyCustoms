using Xunit;
using MyTested.AspNetCore.Mvc;
using DestinyCustoms.Controllers;
using DestinyCustoms.Models.Weapons;

namespace DestinyCustoms.Tests.Routing
{
    public class WeaponControllerTests
    {
        private const string basePath = "/Weapons";

        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"{basePath}/All")
                .To<WeaponsController>(c => c.All(With.Any<AllWeaponsQueryModel>()));

        [Fact]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"{basePath}/Add")
                .To<WeaponsController>(c => c.Add());


        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/Add")
                                        .WithMethod(HttpMethod.Post))
                .To<WeaponsController>(c => c.Add(With.Any<AddWeaponFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"{basePath}/Details")
                .To<WeaponsController>(c => c.Details(With.Any<string>()));

        [Fact]
        public void GetEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/Edit")
                                        .WithMethod(HttpMethod.Post))
                .To<WeaponsController>(c => c.Edit(With.Any<string>()));

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/Edit")
                                        .WithMethod(HttpMethod.Post))
                .To<WeaponsController>(c => c.Edit(With.Any<string>(), With.Any<AddWeaponFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"{basePath}/Delete")
                .To<WeaponsController>(c => c.Delete(With.Any<string>()));

        [Fact]
        public void MyWeaponsShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap($"{basePath}/MyWeapons")
               .To<WeaponsController>(c => c.MyWeapons());
    }
}
