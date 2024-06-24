using GemsOfWarsWARSTAT.TextAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataBaseTest
{
    [TestClass]
    public class UnitTestWarDayReader
    {
       
        [TestMethod]
        public void TestMethodFindDefence()
        {
            var test1 = "Ё123ыф 1232 Защита: 1/3 sdkfljs";
            var result1 = WarDayReader.FindDefence(test1);
            Assert.AreEqual(Tuple.Create(1, 3), result1);

            var test2 = "Ё123ыф 1232 Защита: 013 sdkfljs";
            var result2 = WarDayReader.FindDefence(test2);
            Assert.AreEqual( Tuple.Create(0, 3),result2 );

            var test3 = "Ё123sdfsыф 1232 Защита: 373sds sdkfljs";
            var result3 = WarDayReader.FindDefence(test3);
            Assert.AreEqual(Tuple.Create(3, 3), result3);

            var test4 = "Ё123sdfsыф 1232 Защита: 10/23sds sdkfljs";
            var result4 = WarDayReader.FindDefence(test4);
            Assert.AreEqual(Tuple.Create(10, 23), result4);

            var test5 = "Ё123sdfsыф";
            var result5  = WarDayReader.FindDefence(test5);
            Assert.AreEqual(Tuple.Create(0, 0), result5);
        }
    }
}
