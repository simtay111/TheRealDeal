using NUnit.Framework;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public class DataBuilderTests
    {
        SampleDataBuilder _dataBuilder = new SampleDataBuilder();

        [Test]    
        public void BuildMeSumDater()
        {
            _dataBuilder.DeleteAllData();
            _dataBuilder.CreateData();
        }
    }
}