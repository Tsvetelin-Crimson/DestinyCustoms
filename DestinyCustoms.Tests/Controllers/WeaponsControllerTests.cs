using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Controllers;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Models.Weapons;
using DestinyCustoms.Services.Weapons.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DestinyCustoms.Tests.Controllers
{
    using static Data.Weapons;

    public class WeaponsControllerTests
    {
        [Fact]
        public void AllReturnsViewWithCorrectModel()
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(FiveBlankWeaponsWithWeaponClass()))
                .Calling(c => c.All(new AllWeaponsQueryModel()))
                .ShouldReturn()
                .View(view => view
                            .WithModelOfType<AllWeaponsQueryModel>()
                            .Passing(m =>
                            {
                                Assert.Equal(5, m.AllWeapons);
                                Assert.Equal(3, m.Weapons.Count);
                                Assert.Equal(5, m.WeaponTypes.Count);
                            }));

        [Fact]
        public void GetAddReturnsViewWithCorrectModel()
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(FiveBlankWeaponClasses())
                                    .AndAlso()
                                    .WithUser())
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                            .WithModelOfType<AddWeaponFormModel>()
                            .Passing(m =>
                            {
                                Assert.Equal(5, m.Classes.Count);
                            }));

        [Theory]
        [InlineData(
            "Exotic",
            "Intrinsic",
            "IntrinsicD",
            "Catalyst",
            "CatalystCR",
            "CatalystEff",
            "https://i.kym-cdn.com/photos/images/newsfeed/001/006/166/66f.png",
            1)]
        public void PostAddCreatesWeaponCorrectlyAndReturnsRedirect(
            string name,
            string intrinsicName,
            string intrinsicDescription,
            string catalystName,
            string catalystCompletionRequirement,
            string catalystEffect,
            string imageUrl,
            int classId)
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(FiveBlankWeaponClasses())
                                    .AndAlso()
                                    .WithUser())
                .Calling(c => c.Add(new AddWeaponFormModel()
                {
                    Name = name,
                    IntrinsicName = intrinsicName,
                    IntrinsicDescription = intrinsicDescription,
                    CatalystName = catalystName,
                    CatalystCompletionRequirement = catalystCompletionRequirement,
                    CatalystEffect = catalystEffect,
                    ImageUrl = imageUrl,
                    ClassId = classId,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                        .RestrictingForHttpMethod(HttpMethod.Post)
                                        .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                        .WithSet<ExoticWeapon>(weapons => weapons
                                        .Any(w =>
                                            w.Name == name &&
                                            w.IntrinsicName == intrinsicName &&
                                            w.IntrinsicDescription == intrinsicDescription &&
                                            w.CatalystCompletionRequirement == catalystCompletionRequirement &&
                                            w.CatalystEffect == catalystEffect &&
                                            w.ImageURL == imageUrl &&
                                            w.WeaponClassId == classId)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                                        .To<WeaponsController>(c => c.Details(With.Any<string>())));

        [Theory]
        [InlineData("WeaponGuid")]
        public void DetailsReturnsViewWithCorrectModel(string weaponId) //TODO: Add Comments to test as well
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(OneWeaponsWithWeaponClassAndSetId(weaponId))
                                    .WithUser())
                .Calling(c => c.Details(weaponId))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                            .WithModelOfType<FullWeaponDetailsViewModel>()
                            .Passing(m =>
                            {
                                Assert.NotNull(m.Weapon);
                            }));

        [Theory]
        [InlineData("WeaponId", "NewName")]
        public void GetEditReturnsViewWithCorrectModel(string weaponId, string name)
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(OneWeaponsWithWeaponClassAndSetId(weaponId, name))
                                    .WithUser())
                .Calling(c => c.Edit(weaponId))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                            .WithModelOfType<AddWeaponFormModel>()
                            .Passing(m =>
                            {
                                Assert.Equal(name, m.Name);
                            }));

        [Theory]
        [InlineData(
            "WeaponId",
            "Exotic",
            "Intrinsic",
            "IntrinsicD",
            "Catalyst",
            "CatalystCR",
            "CatalystEff",
            "https://i.kym-cdn.com/photos/images/newsfeed/001/006/166/66f.png",
            1)]
        public void PostEditEditsWeaponCorrectlyAndReturnsRedirect(string weaponId,
            string name,
            string intrinsicName,
            string intrinsicDescription,
            string catalystName,
            string catalystCompletionRequirement,
            string catalystEffect,
            string imageUrl,
            int classId)
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(OneWeaponsWithWeaponClassAndSetId(weaponId))
                                    .WithUser())
                .Calling(c => c.Edit(weaponId, new AddWeaponFormModel
                {
                    Name = name,
                    IntrinsicName = intrinsicName,
                    IntrinsicDescription = intrinsicDescription,
                    CatalystName = catalystName,
                    CatalystCompletionRequirement = catalystCompletionRequirement,
                    CatalystEffect = catalystEffect,
                    ImageUrl = imageUrl,
                    ClassId = classId,
                }
                         ))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                        .RestrictingForAuthorizedRequests())
                .Data(db => db
                            .WithSet<ExoticWeapon>(weapons => weapons
                                            .Any(w => w.Name == name &&
                                                        w.IntrinsicName == intrinsicName &&
                                                        w.IntrinsicDescription == intrinsicDescription &&
                                                        w.CatalystCompletionRequirement == catalystCompletionRequirement &&
                                                        w.CatalystEffect == catalystEffect &&
                                                        w.ImageURL == imageUrl &&
                                                        w.WeaponClassId == classId)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction(nameof(WeaponsController.Details), nameof(WeaponsController).RemoveControllerFromString(), new { id = weaponId });


        [Theory]
        [InlineData("WeaponId")]
        public void DeleteRemovesWeaponAndReturnsRedirect(string weaponId)
            => MyController<WeaponsController>
                .Instance(controller => controller
                                            .WithUser()
                                            .AndAlso()
                                            .WithData(OneWeaponsWithWeaponClassAndSetId(weaponId)))
                .Calling(c => c.Delete(weaponId))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                                .RestrictingForAuthorizedRequests())
                .Data(db => db
                    .WithSet<ExoticWeapon>(weapons => !weapons.Any()))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction(nameof(WeaponsController.All), nameof(WeaponsController).RemoveControllerFromString());

        [Theory]
        [InlineData("WeaponId")]
        public void MyWeaponsReturnsCorrectView(string weaponId) //TODO: Change so this test uses 2 weapons aka add a new method in data/weapons
            => MyController<WeaponsController>
                .Instance(controller => controller
                                            .WithUser()
                                            .AndAlso()
                                            .WithData(OneWeaponsWithWeaponClassAndSetId(weaponId)))
                .Calling(c => c.MyWeapons())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                                .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                                .WithModelOfType<List<WeaponServiceModel>>()
                                .Passing(m => Assert.Single(m)));

    }
}
