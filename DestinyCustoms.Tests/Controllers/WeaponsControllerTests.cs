using System.Linq;
using System.Collections.Generic;
using MyTested.AspNetCore.Mvc;
using Xunit;
using DestinyCustoms.Controllers;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Models.Weapons;
using DestinyCustoms.Services.Weapons.Models;

namespace DestinyCustoms.Tests.Controllers
{
    using static Data.Weapons;
    using static Data.Comments;

    public class WeaponsControllerTests
    {
        [Fact]
        public void AllReturnsViewWithCorrectModel()
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(TenBlankWeapons()))
                .Calling(c => c.All(new AllWeaponsQueryModel()))
                .ShouldReturn()
                .View(view => view
                            .WithModelOfType<AllWeaponsQueryModel>()
                            .Passing(m =>
                            {
                                Assert.Equal(10, m.AllWeapons);
                                Assert.Equal(3, m.Weapons.Count);
                                Assert.Equal(10, m.WeaponTypes.Count);
                            }));

        [Fact]
        public void GetAddReturnsViewWithCorrectModel()
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(TenBlankWeaponClasses())
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
                                Assert.Equal(10, m.Classes.Count);
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
                                    .WithData(TenBlankWeaponClasses())
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
                                            w.CatalystName == catalystName &&
                                            w.CatalystCompletionRequirement == catalystCompletionRequirement &&
                                            w.CatalystEffect == catalystEffect &&
                                            w.ImageURL == imageUrl &&
                                            w.WeaponClassId == classId)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                                        .To<WeaponsController>(c => c.Details(With.Any<string>())));

        [Theory]
        [InlineData("WeaponGuid", 1)]
        public void DetailsReturnsViewWithCorrectModel(string weaponId, int commentId)
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(
                                        OneWeaponWithSetId(weaponId),
                                        OneCommentWithSetId(commentId, weapondId: weaponId))
                                    .WithUser())
                .Calling(c => c.Details(weaponId))
                .ShouldReturn()
                .View(view => view
                            .WithModelOfType<FullWeaponDetailsViewModel>()
                            .Passing(m =>
                            {
                                Assert.Equal(weaponId ,m.Weapon.Id);
                                Assert.Equal(commentId, m.CommentClass.Comments.FirstOrDefault().Id);
                            }));

        [Theory]
        [InlineData("WeaponId", "NewName")]
        public void GetEditReturnsViewWithCorrectModel(string weaponId, string name)
            => MyController<WeaponsController>
                .Instance(controller => controller
                                    .WithData(OneWeaponWithSetId(weaponId, name))
                                    .AndAlso()
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
        public void PostEditEditsWeaponCorrectlyAndReturnsRedirect(
            string weaponId,
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
                                    .WithData(OneWeaponWithSetId(weaponId))
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
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                        .RestrictingForAuthorizedRequests())
                .Data(db => db
                            .WithSet<ExoticWeapon>(weapons => weapons
                                            .Any(w => w.Name == name &&
                                                      w.IntrinsicName == intrinsicName &&
                                                      w.IntrinsicDescription == intrinsicDescription &&
                                                      w.CatalystName == catalystName &&
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
                                            .WithData(OneWeaponWithSetId(weaponId)))
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
        [InlineData("WeaponId", "SecondUserId")]
        public void MyWeaponsReturnsCorrectView(string weaponId, string userId)
            => MyController<WeaponsController>
                .Instance(controller => controller
                                            .WithUser(u => 
                                                u.WithIdentifier(userId))
                                            .AndAlso()
                                            .WithData(ThreeWeaponsOneUserOwned(weaponId, userId)))
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
