using FluentAssertions;
using System;
using System.Text.RegularExpressions;
using Xunit;

namespace wormhole.test
{
    public class RouteTableTests
    {
        [Fact]
        public void RouteRulePathMatch()
        {
            //var PathExpression = "^.?account/.*$";
            var PathExpression = "/account/.*";

            var regex = new Regex(PathExpression, RegexOptions.IgnoreCase);

            var path1 = "/account/id/51?q=1&a=b";
            regex.IsMatch(path1).Should().BeTrue();

            var path2 = "/Account/id/51?q=1&a=b";
            regex.IsMatch(path2).Should().BeTrue();


            var path3 = "HelloAccount/id/51?q=1&a=b";
            regex.IsMatch(path3).Should().BeFalse();

            var path4 = "XAccount/id/51?q=1&a=b";
            regex.IsMatch(path4).Should().BeFalse($"path4 [{path4}]");

            var path5 = "Account/id/51?q=1&a=b";
            regex.IsMatch(path5).Should().BeFalse("path5");

            var path6 = "/account/account/id/51?q=1&a=b";   // Patel: Need some work
            regex.IsMatch(path6,0).Should().BeFalse($"path6 [{path6}]");

            /*
/account/account/id/51?q=1&a=b

/Account/ID/51?q=1&a=b
/AccountX/ID/51?q=1&a=b
Account/ID/51?q=1&a=b

HelloAAccount/ID/51?q=1&a=b
count/ID/51?q=1&a=b
             */
        }
    }
}
