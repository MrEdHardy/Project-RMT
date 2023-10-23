using Project_RMT.Core;
using Project_RMT.UnitTests.Common;

namespace Project_RMT.UnitTests.Core.Rip
{
    public class Rip
    {
        [Fact]
        public void ShouldGenerateUpdatedRoutingTable()
        {
            var preparedNetworkStructure = UnitTestHelpers.PrepareBaseNetworkstructure(false);

            var router1 = preparedNetworkStructure.routerList.First();
            var router3 = preparedNetworkStructure.routerList.Last();

            var router1RoutingTableCountBefore = router1.RoutingTable.Count;

            var ripAlgorithm = new RipAlgorithm();
            ripAlgorithm.UpdateRoutingTables(ref preparedNetworkStructure.routerList);

            var router1RoutingTableCountAfter = router1.RoutingTable.Count;

        }
    }
}
