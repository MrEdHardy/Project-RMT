using Project_RMT.Core;
using Project_RMT.UnitTests.Common;

namespace Project_RMT.UnitTests.Core.Rip
{
    public class Rip
    {
        [Fact]
        public void ShouldGenerateUpdatedRoutingTable()
        {
            var preparedNetworkStructure = UnitTestHelpers.PrepareBaseNetworkstructure();

            var router1 = preparedNetworkStructure.routerList.First();
            var router3 = preparedNetworkStructure.routerList.Last();
            var client1 = preparedNetworkStructure.clientList.First();
            var client2 = preparedNetworkStructure.clientList.Last();

            var router1RoutingTableCountBefore = router1.RoutingTable.Count;
            var router3RoutingTableCountBefore = router3.RoutingTable.Count;

            var ripAlgorithm = new RipAlgorithm();
            ripAlgorithm.UpdateRoutingTables(ref preparedNetworkStructure.routerList);

            var router1RoutingTableCountAfter = router1.RoutingTable.Count;
            var router3RoutingTableCountAfter = router3.RoutingTable.Count;
            Assert.True(router1RoutingTableCountAfter > router1RoutingTableCountBefore);
            Assert.True(router3RoutingTableCountAfter > router3RoutingTableCountBefore);


            Assert.Contains(router1.RoutingTable, re => re.TargetIPAdress == client2.IPAdress);
            Assert.True(router1.RoutingTable.Single(re => re.TargetIPAdress == client2.IPAdress).Metric == 1);

            Assert.Contains(router3.RoutingTable, re => re.TargetIPAdress == client1.IPAdress);
            Assert.Contains(router3.RoutingTable, re => re.TargetIPAdress == client2.IPAdress);
            Assert.True(router3.RoutingTable.Single(re => re.TargetIPAdress == client1.IPAdress).Metric == 1);
            Assert.True(router3.RoutingTable.Single(re => re.TargetIPAdress == client2.IPAdress).Metric == 1);
        }
    }
}
