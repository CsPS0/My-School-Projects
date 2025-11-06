namespace csokigyarLib.Tests
{
    public class CsokiTests
    {
        [Theory]
        [InlineData(51, true)]
        [InlineData(50, false)]
        [InlineData(0, false)]
        public void MegfeleloMinosegu_ShouldReturnCorrectValue(int kakao, bool vart)
        {
            var csoki = new Csoki("test", new string[] { }, kakao);
            var eredmeny = csoki.MegfeleloMinosegu;
            Assert.Equal(vart, eredmeny);
        }

        [Fact]
        public void MegfeleloMinosegu_ShouldThrowException_WhenKakaoIsNegative()
        {
            var csoki = new Csoki("test", new string[] { }, -1);
            Assert.Throws<SilanyMinosegException>(() => csoki.MegfeleloMinosegu);
        }
    }
}