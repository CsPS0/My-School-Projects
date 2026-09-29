using TreasureChest;

namespace Tests
{
    public class Tests
    {
        private Chest chest = null!;

        [SetUp]
        public void Setup()
        {
            chest = new Chest("vasveretes faláda", 10);
        }

        // láda kinyit, felnyit, hogy ne akadjon bele
        private void OpenChest()
        {
            chest.UnLock();
            chest.Open();
        }

        [Test]
        public void NewChestIsLockedAndClosed()
        {
            Assert.That(chest.IsLocked, Is.True,
                "HIBA: az új láda nincs bezárva, pedig alapból zárt állapotban kell létrejönnie.");
            Assert.That(chest.IsOpen, Is.False,
                "HIBA: az új láda fel van nyitva, pedig alapból lecsukott állapotban kell létrejönnie.");
        }

        [Test]
        public void NewChestStoresNameAndVolume()
        {
            Assert.That(chest.Name, Is.EqualTo("vasveretes faláda"),
                "HIBA: a láda nem a konstruktorban megadott nevet tárolja.");
            Assert.That(chest.Volume, Is.EqualTo(10),
                "HIBA: a láda nem a konstruktorban megadott térfogatot tárolja.");
        }

        [Test]
        public void NewChestZeroVolumeIsAllowed()
        {
            Assert.DoesNotThrow(() => new Chest("üres doboz", 0),
                "HIBA: a 0 térfogatú láda létrehozása kivételt dob, pedig a leírás szerint a 0 megengedett.");
        }

        [Test]
        public void NewChestNegativeVolumeIsRejected()
        {
            Assert.Throws<ArgumentException>(() => new Chest("hibás láda", -1),
                "HIBA: a láda negatív térfogattal is létrejön, a leírás szerint ez nem megengedett.");
        }

        [TestCase(0)]
        [TestCase(-5)]
        public void NewTreasureZeroOrNegativeVolumeIsRejected(int volume)
        {
            Assert.Throws<ArgumentException>(() => new Treasure("hibás kincs", volume),
                $"HIBA: a kincs {volume} térfogattal is létrejön, a leírás szerint a térfogat nem lehet 0 vagy negatív.");
        }

        [Test]
        public void NewTreasureStoresNameAndVolume()
        {
            Treasure t = new Treasure("hosszúkard", 3);
            Assert.That(t.Name, Is.EqualTo("hosszúkard"),
                "HIBA: a kincs nem a konstruktorban megadott nevet tárolja.");
            Assert.That(t.Volume, Is.EqualTo(3),
                "HIBA: a kincs nem a konstruktorban megadott térfogatot tárolja.");
        }

        [Test]
        public void isItOpen()
        {
            chest.UnLock();
            Assert.That(chest.Open(), Is.True,
                "HIBA: a nem bezárt, lecsukott láda Open() hívása false-t ad, pedig fel kell nyílnia.");
            Assert.That(chest.IsOpen, Is.True,
                "HIBA: az Open() után az IsOpen false, a láda nem nyílt fel.");
        }

        [Test]
        public void OpenFailsWhenLocked()
        {
            Assert.That(chest.Open(), Is.False,
                "HIBA: a bezárt láda Open() hívása true-t ad, pedig bezárt ládát nem lehet felnyitni.");
            Assert.That(chest.IsOpen, Is.False,
                "HIBA: a bezárt láda felnyílt.");
        }

        [Test]
        public void OpenFailsWhenAlreadyOpen()
        {
            OpenChest();
            Assert.That(chest.Open(), Is.False,
                "HIBA: a már nyitott láda Open() hívása true-t ad, pedig csak lecsukott ládát lehet felnyitni.");
            Assert.That(chest.IsOpen, Is.True,
                "HIBA: a sikertelen Open() hívás megváltoztatta a láda nyitott állapotát.");
        }

        [Test]
        public void isItClosed()
        {
            OpenChest();
            Assert.That(chest.Close(), Is.True,
                "HIBA: a nyitott láda Close() hívása false-t ad, pedig le kell csukódnia.");
            Assert.That(chest.IsOpen, Is.False,
                "HIBA: a Close() után az IsOpen true, a láda nem csukódott le.");
        }

        [Test]
        public void CloseFailsWhenAlreadyClosed()
        {
            chest.UnLock();
            Assert.That(chest.Close(), Is.False,
                "HIBA: a már lecsukott láda Close() hívása true-t ad, pedig csak nyitott ládát lehet lecsukni.");
            Assert.That(chest.IsOpen, Is.False,
                "HIBA: a sikertelen Close() hívás felnyitotta a ládát.");
        }

        [Test]
        public void isItLocked()
        {
            chest.UnLock();
            Assert.That(chest.Lock(), Is.True,
                "HIBA: a lecsukott, nem bezárt láda Lock() hívása false-t ad, pedig be kell záródnia.");
            Assert.That(chest.IsLocked, Is.True,
                "HIBA: a Lock() után az IsLocked false, a láda nem záródott be.");
        }

        [Test]
        public void LockFailsWhenOpen()
        {
            OpenChest();
            Assert.That(chest.Lock(), Is.False,
                "HIBA: a nyitott láda Lock() hívása true-t ad, pedig csak lecsukott ládát lehet bezárni.");
            Assert.That(chest.IsLocked, Is.False,
                "HIBA: a nyitott láda bezáródott.");
        }

        [Test]
        public void LockFailsWhenAlreadyLocked()
        {
            Assert.That(chest.Lock(), Is.False,
                "HIBA: a már bezárt láda Lock() hívása true-t ad, pedig csak nem bezárt ládát lehet bezárni.");
            Assert.That(chest.IsLocked, Is.True,
                "HIBA: a sikertelen Lock() hívás feloldotta a zárat.");
        }

        [Test]
        public void isItUnlocked()
        {
            Assert.That(chest.UnLock(), Is.True,
                "HIBA: a bezárt láda UnLock() hívása false-t ad, pedig a zárnak ki kell nyílnia.");
            Assert.That(chest.IsLocked, Is.False,
                "HIBA: az UnLock() után az IsLocked true, a zár nem nyílt ki.");
        }

        [Test]
        public void UnLockFailsWhenNotLocked()
        {
            chest.UnLock();
            Assert.That(chest.UnLock(), Is.False,
                "HIBA: a nem bezárt láda UnLock() hívása true-t ad, pedig csak bezárt ládát lehet kinyitni.");
            Assert.That(chest.IsLocked, Is.False,
                "HIBA: a sikertelen UnLock() hívás bezárta a ládát.");
        }

        [Test]
        public void isItStored()
        {
            OpenChest();
            Assert.That(chest.Store(new Treasure("aranyserleg", 4)), Is.True,
                "HIBA: a Store() false-t ad, pedig a kincs elfér a nyitott ládában.");
            Assert.That(chest.ToString(), Does.Contain("aranyserleg"),
                "HIBA: a sikeres Store() után a kincs nem szerepel a láda tartalmában.");
        }

        [Test]
        public void isItFull()
        {
            OpenChest();
            chest.Store(new Treasure("pajzs", 8));
            Assert.That(chest.Store(new Treasure("hosszúkard", 3)), Is.False,
                "HIBA: a Store() true-t ad, pedig a kincs nagyobb, mint a szabad hely.");
            Assert.That(chest.ToString(), Does.Not.Contain("hosszúkard"),
                "HIBA: a helyhiány miatt elutasított kincs mégis bekerült a ládába.");
        }


        [Test]
        public void isItAccessible()
        {
            OpenChest();
            Assert.That(chest.Store(new Treasure("gyűrű", 1)), Is.True,
                "HIBA: a nyitott ládába nem lehet kincset tenni.");
            Assert.That(chest.TakeOutLast(), Is.Not.Null,
                "HIBA: a nyitott ládából nem lehet kivenni a behelyezett kincset.");
        }

        [Test]
        public void isItInaccessible()
        {
            OpenChest();
            chest.Store(new Treasure("gyűrű", 1));
            chest.Close();

            Assert.That(chest.Store(new Treasure("nyaklánc", 1)), Is.False,
                "HIBA: a lecsukott ládába be lehet tenni kincset.");
            Assert.That(chest.TakeOutLast(), Is.Null,
                "HIBA: a lecsukott ládából ki lehet venni az utolsó kincset.");
            Assert.That(chest.TakeOut("gyűrű"), Is.Null,
                "HIBA: a lecsukott ládából ki lehet venni kincset név szerint.");
        }

        [Test]
        public void TakenOutLast()
        {
            OpenChest();
            chest.Store(new Treasure("pajzs", 2));
            chest.Store(new Treasure("hosszúkard", 3));

            Treasure? t = chest.TakeOutLast();

            Assert.That(t, Is.Not.Null,
                "HIBA: a TakeOutLast() null-t ad, pedig a láda nem üres.");
            Assert.That(t!.Name, Is.EqualTo("hosszúkard"),
                "HIBA: a TakeOutLast() nem az utoljára behelyezett kincset adja vissza.");
            Assert.That(chest.ToString(), Does.Not.Contain("hosszúkard"),
                "HIBA: a kivett kincs a ládában maradt.");
            Assert.That(chest.ToString(), Does.Contain("pajzs"),
                "HIBA: a TakeOutLast() más kincset is eltávolított a ládából.");
        }

        [Test]
        public void TakeOutLastReturnsNullWhenEmpty()
        {
            OpenChest();
            Assert.That(chest.TakeOutLast(), Is.Null,
                "HIBA: az üres ládán hívott TakeOutLast() nem null-t ad vissza.");
        }

        [Test]
        public void TakenOut()
        {
            OpenChest();
            chest.Store(new Treasure("pajzs", 2));
            chest.Store(new Treasure("hosszúkard", 3));

            Treasure? t = chest.TakeOut("pajzs");

            Assert.That(t, Is.Not.Null,
                "HIBA: a TakeOut() null-t ad, pedig van ilyen nevű kincs a ládában.");
            Assert.That(t!.Name, Is.EqualTo("pajzs"),
                "HIBA: a TakeOut() nem a kért nevű kincset adja vissza.");
            Assert.That(chest.ToString(), Does.Not.Contain("pajzs"),
                "HIBA: a kivett kincs a ládában maradt.");
            Assert.That(chest.ToString(), Does.Contain("hosszúkard"),
                "HIBA: a TakeOut() más nevű kincset is eltávolított a ládából.");
        }

        [Test]
        public void TakeOutRemovesOnlyTheFirstMatch()
        {
            OpenChest();
            chest.Store(new Treasure("hosszúkard", 1));
            chest.Store(new Treasure("pajzs", 1));
            chest.Store(new Treasure("hosszúkard", 1));

            chest.TakeOut("hosszúkard");

            Assert.That(chest.ToString(), Does.EndWith("pajzs, hosszúkard"),
                "HIBA: a TakeOut() az összes azonos nevű kincset kiveszi, nem csak az elsőt.");
        }

        [Test]
        public void TakeOutReturnsNullWhenNameNotFound()
        {
            OpenChest();
            chest.Store(new Treasure("hosszúkard", 2));
            Assert.That(chest.TakeOut("kard"), Is.Null,
                "HIBA: a TakeOut() nem null-t ad, pedig nincs pontosan ilyen nevű kincs a ládában.");
        }

        [Test]
        public void isItEmpty()
        {
            string text = chest.ToString();
            Assert.That(text, Does.Contain("vasveretes faláda"),
                "HIBA: a ToString() nem írja ki a láda nevét.");
            Assert.That(text, Does.Contain("Nincs benne semmi"),
                "HIBA: az üres láda ToString() kimenete nem jelzi, hogy nincs benne semmi.");
        }

        [Test]
        public void ToStringListsContentsWithCommas()
        {
            OpenChest();
            chest.Store(new Treasure("pajzs", 2));
            chest.Store(new Treasure("hosszúkard", 3));
            chest.Store(new Treasure("gyűrű", 1));

            string text = chest.ToString();

            Assert.That(text, Does.Contain("vasveretes faláda"),
                "HIBA: a ToString() nem írja ki a láda nevét.");
            Assert.That(text, Does.Contain("pajzs, hosszúkard, gyűrű"),
                "HIBA: a ToString() nem vesszővel elválasztva, behelyezési sorrendben sorolja fel a tartalmat.");
            Assert.That(text, Does.Not.Contain("Nincs benne semmi"),
                "HIBA: a nem üres láda ToString() kimenete azt írja, hogy nincs benne semmi.");
        }
    }
}
