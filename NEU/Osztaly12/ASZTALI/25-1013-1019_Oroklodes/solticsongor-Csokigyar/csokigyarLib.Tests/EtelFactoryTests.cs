namespace csokigyarLib.Tests
{
    public class EtelFactoryTests
    {
        [Fact]
        public void Factory_ShouldCreateCsoki_WhenNotPremium()
        {
            var gyar = new EtelFactory();
            var adat = "tejcsokoládé;40;kakaómassza;cukor";
            var eredmeny = gyar.Factory(adat);

            Assert.IsAssignableFrom<Csoki>(eredmeny);
        }

        [Fact]
        public void Factory_ShouldCreatePremiumCsoki_WhenPremium()
        {
            var gyar = new EtelFactory();
            var adat = "étcsokoládé;81;kakaómassza;vaj;cukor;prémium";
            var eredmeny = gyar.Factory(adat);

            Assert.IsAssignableFrom<PremiumCsoki>(eredmeny);
        }
    }
}