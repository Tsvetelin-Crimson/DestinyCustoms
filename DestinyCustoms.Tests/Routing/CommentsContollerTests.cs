using DestinyCustoms.Controllers;
using DestinyCustoms.Models.Comments;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace DestinyCustoms.Tests.Routing
{
    public class CommentsContollerTests
    {
        private const string basePath = "/Comments";

        [Fact]
        public void AddWeaponCommentShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/AddWeaponComment")
                                        .WithMethod(HttpMethod.Post))
                .To<CommentsController>(c => c.AddWeaponComment(With.Any<AddCommentFormModel>()));

        [Fact]
        public void AddWeaponReplyShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/AddWeaponReply")
                                        .WithMethod(HttpMethod.Post))
                .To<CommentsController>(c => c.AddWeaponReply(With.Any<AddReplyFormModel>()));

        [Fact]
        public void AddArmorCommentShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/AddArmorComment")
                                        .WithMethod(HttpMethod.Post))
                .To<CommentsController>(c => c.AddArmorComment(With.Any<AddCommentFormModel>()));

        [Fact]
        public void AddArmorReplyShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                                        .WithPath($"{basePath}/AddArmorReply")
                                        .WithMethod(HttpMethod.Post))
                .To<CommentsController>(c => c.AddArmorReply(With.Any<AddReplyFormModel>()));
    }
}
