using Xunit;
using MyTested.AspNetCore.Mvc;
using DestinyCustoms.Controllers;
using DestinyCustoms.Models.Armors;

namespace DestinyCustoms.Tests.Routing
{
    public class ArmorsControllerTests
    {
        private const string basePath = "/Armors";

        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"{basePath}/All")
                .To<ArmorsController>(c => c.All(With.Any<AllArmorsQueryModel>()));

        [Fact]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"{basePath}/Add")
                .To<ArmorsController>(c => c.Add());


        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/Add")
                                        .WithMethod(HttpMethod.Post))
                .To<ArmorsController>(c => c.Add(With.Any<AddArmorFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"{basePath}/Details")
                .To<ArmorsController>(c => c.Details(With.Any<string>()));

        [Fact]
        public void GetEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/Edit")
                                        .WithMethod(HttpMethod.Post))
                .To<ArmorsController>(c => c.Edit(With.Any<string>()));

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/Edit")
                                        .WithMethod(HttpMethod.Post))
                .To<ArmorsController>(c => c.Edit(With.Any<string>(), With.Any<AddArmorFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"{basePath}/Delete")
                .To<ArmorsController>(c => c.Delete(With.Any<string>()));

        [Fact]
        public void MyArmorsShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap($"{basePath}/MyArmors")
               .To<ArmorsController>(c => c.MyArmors());
    }
}
