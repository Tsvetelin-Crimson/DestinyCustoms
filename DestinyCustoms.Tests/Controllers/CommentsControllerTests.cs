 
using DestinyCustoms.Controllers;
using DestinyCustoms.Data.Models;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Models.Comments;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace DestinyCustoms.Tests.Controllers
{
    using static Data.Weapons;
    using static Data.Armors;
    using static Data.Comments;

    public class CommentsControllerTests
    {
        [Theory]
        [InlineData("10 MINUTES OF CONTENT", "WeaponId")]
        public void AddWeaponCommentShouldAddCorrectCommentAndReturnRedirect(string content, string weaponId)
            => MyController<CommentsController>
                .Instance(controller => controller
                                .WithData(OneWeaponWithSetId(weaponId))
                                .AndAlso()
                                .WithUser())
                .Calling(c => c.AddWeaponComment(new AddCommentFormModel 
                { 
                    Content = content,
                    ItemId = weaponId,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                                .RestrictingForHttpMethod(HttpMethod.Post)
                                                .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(db => db
                            .WithSet<Comment>(comments => comments
                                                            .Any(c => c.Content == content &&
                                                                      c.WeaponId == weaponId)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction(
                    nameof(WeaponsController.Details),
                    nameof(WeaponsController).RemoveControllerFromString(),
                    new { id = weaponId });

        [Theory]
        [InlineData(
            "10 MORE MINUTES OF CONTENT", 
            1,
            "WeaponId")]
        public void AddWeaponReplyShouldAddCorrectReplyAndReturnRedirect(
            string content,
            int commentId,
            string weaponId)
            => MyController<CommentsController>
                .Instance(controller => controller
                                .WithData(
                                        OneCommentWithSetId(commentId, weapondId: weaponId), 
                                        OneWeaponWithSetId(weaponId))
                                .AndAlso()
                                .WithUser())
                .Calling(c => c.AddWeaponReply(new AddReplyFormModel
                {
                    Content = content,
                    CommentId = commentId,
                    ItemId = weaponId,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                                .RestrictingForHttpMethod(HttpMethod.Post)
                                                .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(db => db
                            .WithSet<Reply>(replies => replies
                                                            .Any(r => r.Content == content &&
                                                                      r.CommentId == commentId)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction(
                    nameof(WeaponsController.Details),
                    nameof(WeaponsController).RemoveControllerFromString(),
                    new { id = weaponId });

        [Theory]
        [InlineData("EVEN MORE 10 MINUTES OF CONTENT", "ArmorId")]
        public void AddArmorCommentShouldAddCorrectCommentAndReturnRedirect(string content, string armorId)
            => MyController<CommentsController>
                .Instance(controller => controller
                                .WithData(OneArmorWithSetId(armorId))
                                .AndAlso()
                                .WithUser())
                .Calling(c => c.AddArmorComment(new AddCommentFormModel
                {
                    Content = content,
                    ItemId = armorId,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                                                .RestrictingForHttpMethod(HttpMethod.Post)
                                                .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(db => db
                            .WithSet<Comment>(comments => comments
                                                            .Any(c => c.Content == content &&
                                                                      c.ArmorId == armorId)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction(
                    nameof(ArmorsController.Details),
                    nameof(ArmorsController).RemoveControllerFromString(),
                    new { id = armorId });

        [Theory]
        [InlineData(
            "YOU GOTTA LOVE 10 MINUTES OF CONTENT",
            1,
            "ArmorId")]
        public void AddArmorReplyShouldAddCorrectReplyAndReturnRedirect(
                string content,
                int commentId,
                string armorId)
                => MyController<CommentsController>
                    .Instance(controller => controller
                                    .WithData(
                                            OneCommentWithSetId(commentId, armorId: armorId),
                                            OneArmorWithSetId(armorId))
                                    .AndAlso()
                                    .WithUser())
                    .Calling(c => c.AddArmorReply(new AddReplyFormModel
                    {
                        Content = content,
                        CommentId = commentId,
                        ItemId = armorId,
                    }))
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                                                    .RestrictingForHttpMethod(HttpMethod.Post)
                                                    .RestrictingForAuthorizedRequests())
                    .ValidModelState()
                    .Data(db => db
                                .WithSet<Reply>(replies => replies
                                                                .Any(r => r.Content == content &&
                                                                          r.CommentId == commentId)))
                    .AndAlso()
                    .ShouldReturn()
                    .RedirectToAction(
                        nameof(ArmorsController.Details),
                        nameof(ArmorsController).RemoveControllerFromString(),
                        new { id = armorId });
    }
}
