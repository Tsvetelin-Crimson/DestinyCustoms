using System.Linq;
using System.Collections.Generic;
using DestinyCustoms.Controllers;
using DestinyCustoms.Common.Enums;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Models.Armors;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Services.Armors.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DestinyCustoms.Tests.Controllers
{

    using static Data.Armors;
    public class ArmorControllerTests
    {
        [Fact]
        public void AllReturnsViewWithCorrectModel()
           => MyController<ArmorsController>
               .Instance(controller => controller
                                   .WithData(TenBlankArmors()))
               .Calling(c => c.All(new AllArmorsQueryModel()))
               .ShouldReturn()
               .View(view => view
                           .WithModelOfType<AllArmorsQueryModel>()
                           .Passing(m =>
                           {
                               Assert.Equal(10, m.AllArmorsCount);
                               Assert.Equal(3, m.Armors.Count);
                               Assert.Equal(3, m.Classes.Count);
                           }));

        [Fact]
        public void GetAddReturnsViewWithCorrectModel()
            => MyController<ArmorsController>
                .Instance(controller => controller
                                    .WithUser())
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();


        [Theory]
        [InlineData(
            "Exotic",
            "Intrinsic",
            "IntrinsicD",
            "https://i.kym-cdn.com/photos/images/newsfeed/001/006/166/66f.png",  //Maybe change to the default image url in common fole
            CharacterClass.Warlock)]
        public void PostAddCreatesArmorCorrectlyAndReturnsRedirect(
            string name,
            string intrinsicName,
            string intrinsicDescription,
            string imageUrl,
            CharacterClass characterClass)
            => MyController<ArmorsController>
                .Instance(controller => controller
                                    .WithData(TenBlankArmors())
                                    .AndAlso()
                                    .WithUser())
                .Calling(c => c.Add(new AddArmorFormModel()
                {
                    Name = name,
                    IntrinsicName = intrinsicName,
                    IntrinsicDescription = intrinsicDescription,
                    ImageUrl = imageUrl,
                    Class = characterClass.ToString(),
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                        .RestrictingForHttpMethod(HttpMethod.Post)
                                        .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                        .WithSet<ExoticArmor>(weapons => weapons
                                        .Any(a =>
                                            a.Name == name &&
                                            a.IntrinsicName == intrinsicName &&
                                            a.IntrinsicDescription == intrinsicDescription &&
                                            a.ImageURL == imageUrl &&
                                            a.CharacterClass == characterClass)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                                        .To<ArmorsController>(c => c.Details(With.Any<string>())));

        [Theory]
        [InlineData("ArmorGuid")]
        public void DetailsReturnsViewWithCorrectModel(string armorId) //TODO: Add Comments to test as well
            => MyController<ArmorsController>
                .Instance(controller => controller
                                    .WithData(OneArmorWithSetId(armorId))
                                    .WithUser())
                .Calling(c => c.Details(armorId))
                .ShouldReturn()
                .View(view => view
                            .WithModelOfType<FullArmorDetailsViewModel>()
                            .Passing(m =>
                            {
                                Assert.NotNull(m.Armor);
                            }));

        [Theory]
        [InlineData("ArmorId", "NewName")]
        public void GetEditReturnsViewWithCorrectModel(string armorId, string name)
                => MyController<ArmorsController>
                    .Instance(controller => controller
                                        .WithData(OneArmorWithSetId(armorId, name))
                                        .AndAlso()
                                        .WithUser())
                    .Calling(c => c.Edit(armorId))
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                                            .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View(view => view
                                .WithModelOfType<AddArmorFormModel>()
                                .Passing(m =>
                                {
                                    Assert.Equal(name, m.Name);
                                }));

        [Theory]
        [InlineData(
                "ArmorId",
                "Exotic",
                "Intrinsic",
                "IntrinsicD",
                "https://i.kym-cdn.com/photos/images/newsfeed/001/006/166/66f.png",
                CharacterClass.Warlock)]
        public void PostEditEditsArmorCorrectlyAndReturnsRedirect(
                string armorId,
                string name,
                string intrinsicName,
                string intrinsicDescription,
                string imageUrl,
                CharacterClass characterClass)
                => MyController<ArmorsController>
                    .Instance(controller => controller
                                        .WithData(OneArmorWithSetId(armorId))
                                        .WithUser())
                    .Calling(c => c.Edit(armorId, new AddArmorFormModel
                    {
                        Name = name,
                        IntrinsicName = intrinsicName,
                        IntrinsicDescription = intrinsicDescription,
                        ImageUrl = imageUrl,
                        Class = characterClass.ToString(),
                    }))
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                                            .RestrictingForAuthorizedRequests())
                    .Data(db => db
                                .WithSet<ExoticArmor>(weapons => weapons
                                                .Any(a => a.Name == name &&
                                                          a.IntrinsicName == intrinsicName &&
                                                          a.IntrinsicDescription == intrinsicDescription &&
                                                          a.ImageURL == imageUrl &&
                                                          a.CharacterClass == characterClass)))
                    .AndAlso()
                    .ShouldReturn()
                    .RedirectToAction(nameof(ArmorsController.Details), nameof(ArmorsController).RemoveControllerFromString(), new { id = armorId });


        [Theory]
        [InlineData("ArmorId")]
        public void DeleteRemovesArmorAndReturnsRedirect(string armorId)
                => MyController<ArmorsController>
                    .Instance(controller => controller
                                                .WithUser()
                                                .AndAlso()
                                                .WithData(OneArmorWithSetId(armorId)))
                    .Calling(c => c.Delete(armorId))
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                                                    .RestrictingForAuthorizedRequests())
                    .Data(db => db
                        .WithSet<ExoticArmor>(armors => !armors.Any()))
                    .AndAlso()
                    .ShouldReturn()
                    .RedirectToAction(nameof(ArmorsController.All), nameof(ArmorsController).RemoveControllerFromString());

        [Theory]
        [InlineData("ArmorId", "SecondUserId")]
        public void MyWeaponsReturnsCorrectView(string armorId, string userId)
                => MyController<ArmorsController>
                    .Instance(controller => controller
                                                .WithUser(u =>
                                                    u.WithIdentifier(userId))
                                                .AndAlso()
                                                .WithData(ThreeArmorsOneUserOwned(armorId, userId)))
                    .Calling(c => c.MyArmors())
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                                                    .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View(view => view
                                    .WithModelOfType<List<ArmorServiceModel>>()
                                    .Passing(m => Assert.Single(m)));
    }
}
