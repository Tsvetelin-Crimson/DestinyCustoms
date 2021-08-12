using DestinyCustoms.Controllers;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace DestinyCustoms.Tests.Routing
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
