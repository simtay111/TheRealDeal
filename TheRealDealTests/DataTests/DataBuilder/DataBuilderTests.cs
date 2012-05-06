using NUnit.Framework;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public class DataBuilderTests
    {
        SampleDataBuilder _dataBuilder = new SampleDataBuilder();

        [Test]
        [Category("Data")]
        public void BuildMeSumDater()
        {
            _dataBuilder.CreateData();
        }
    }
}