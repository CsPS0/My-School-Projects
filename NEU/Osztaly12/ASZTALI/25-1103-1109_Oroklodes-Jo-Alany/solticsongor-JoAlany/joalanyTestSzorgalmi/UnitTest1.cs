
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
﻿using System;
﻿using joalanyLib;
﻿
﻿namespace joalanyTestSzorgalmi
﻿{
﻿    [TestClass]
﻿    public class UnitTest1
﻿    {
﻿        [TestMethod]
﻿        public void KorJoSzamlalas()
﻿        {
﻿            DateTime szuletesiDatum = new DateTime(2000, 1, 1);
﻿            Szemely szemely = new Szemely("Teszt Elek", szuletesiDatum);
﻿            int kor = DateTime.Now.Year - szuletesiDatum.Year;
﻿            if (szuletesiDatum.DayOfYear > DateTime.Now.DayOfYear)
﻿            {
﻿                kor--;
﻿            }
﻿            Assert.AreEqual(kor, szemely.Kor);
﻿        }
﻿
﻿        [TestMethod]
﻿        public void OkReturnNoPuska()
﻿        {
﻿            Diak diak = new Diak("Teszt Diak", new DateTime(2005, 5, 15), 0);
﻿            Assert.IsTrue(diak.JoAlanyE());
﻿        }
﻿
﻿        [TestMethod]
﻿        public void TulAlacsonyEletkor()
﻿        {
﻿            Tanar tanar = new Tanar("Teszt Tanar", DateTime.Now.AddYears(-35), 3.0);
﻿            Assert.IsFalse(tanar.JoAlanyE());
﻿        }
﻿    }
﻿}﻿