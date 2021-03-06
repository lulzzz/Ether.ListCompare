using System;
using System.Collections.Generic;
using ListCompare.Tests.Models;
using Xunit;

namespace ListCompare.Tests
{
    public class UniqueIntKeyComparerTests
    {
        [Fact]
        public void UniqueIntKeyComparer_Works()
        {            
            var roberts = new Pirate() { Name = "Dread Pirate Roberts", PirateId = 1 };
            var silver = new Pirate() { Name = "Long John Silver", PirateId = 2 };
            var blackbeard = new Pirate() { Name = "Blackbeard", PirateId = 3 };

            var pirateList = new List<int>() { 3, 4 };
            var pirates = new List<Pirate> {roberts, silver, blackbeard};

            var comparer = ListComparison.ListCompare.Compare(pirateList, pirates)
                .ByIntegerKey(left => left, right => right.PirateId)
                .Go();

            // Common Tests
            var commonLeft = comparer.CommonItemsAsLeft();

            Assert.True(commonLeft.Count == 1);
            Assert.Contains(3, commonLeft);

            var commonRight = comparer.CommonItemsAsRight();

            Assert.True(commonRight.Count == 1);
            Assert.Contains(blackbeard, commonRight);

            var common = comparer.CommonItems();

            Assert.True(common.Count == 1);
            Assert.True(common[0].LeftItem == 3 && common[0].RightItem == blackbeard);

            var missing = comparer.MissingItems();

            Assert.True(missing.Count == 3);
            Assert.Contains(roberts, missing);
            Assert.Contains(silver, missing);
            Assert.Contains(4, missing);

            var missingLeft = comparer.MissingFromLeft();

            Assert.True(missingLeft.Count == 2);
            Assert.Contains(roberts, missingLeft);
            Assert.Contains(silver, missingLeft);

            var missingRight = comparer.MissingFromRight();

            Assert.True(missingRight.Count == 1);
            Assert.Contains(4, missingRight);
        }
    }
}
