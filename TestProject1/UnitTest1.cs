

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var parser = new MathExpression();
            string test = "7 8 * 8 +";
            Assert.AreEqual<decimal>(64, parser.ParsePolishNotation(test));
        }

        [TestMethod]
        public void TestMethodFloatingPoint()
        {
            var parser = new MathExpression();
            string test = "1,1 1,1 *";
            Assert.AreEqual<decimal>(1.21m, parser.ParsePolishNotation(test));
        }

        [TestMethod]
        public void TestMethodSum()
        {
            var parser = new MathExpression();
            string test = "2,7 6,3 +";
            Assert.AreEqual<decimal>(9m, parser.ParsePolishNotation(test));
        }

        [TestMethod]
        public void TestMethodMultiplication()
        {
            var parser = new MathExpression();
            string test = "2,89 6,12 *";
            Assert.AreEqual<decimal>(2.89m*6.12m, parser.ParsePolishNotation(test));
        }

        [TestMethod]
        public void TestMethodDivision()
        {
            var parser = new MathExpression();
            string test = "2,89 6,12 /";
            Assert.AreEqual<decimal>(2.89m / 6.12m, parser.ParsePolishNotation(test));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]

        public void TestMethodDivisionZero()
        {
            var parser = new MathExpression();
            string test = "2,89 0 /";
            parser.ParsePolishNotation(test);
        }

        [TestMethod]
        public void TestMethodMinus()
        {
            var parser = new MathExpression();
            string test = "2,89 5,79 -";
            Assert.AreEqual<decimal>(2.89m - 5.79m, parser.ParsePolishNotation(test));
        }

        [TestMethod]
        public void TestMethodPrimer()
        {
            var parser = new MathExpression();
            string test = "0 2 - 9 * 0 - 2 /";
            Assert.AreEqual<decimal>( -9, parser.ParsePolishNotation(test));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        public void TestMethodDaTrash()
        {
            var parser = new MathExpression();
            string test = "2,89 0 sgr";
            parser.ParsePolishNotation(test);
        }

    }
}