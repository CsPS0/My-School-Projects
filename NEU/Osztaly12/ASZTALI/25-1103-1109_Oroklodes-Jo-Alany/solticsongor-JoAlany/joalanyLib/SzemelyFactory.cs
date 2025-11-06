using System.Globalization;
﻿
﻿namespace joalanyLib
﻿{
﻿    public class SzemelyFactory
﻿    {
﻿        public static Szemely Factory(string adat)
﻿        {
﻿            string[] adatok = adat.Split(';');
﻿            string tipus = adatok[0];
﻿            string nev = adatok[1];
﻿            DateTime szuletesiDatum = DateTime.Parse(adatok[2]);
﻿
﻿            if (tipus == "t")
﻿            {
﻿                double jegyekAtlaga = double.Parse(adatok[3].Replace(',', '.'), CultureInfo.InvariantCulture);
﻿                return new Tanar(nev, szuletesiDatum, jegyekAtlaga);
﻿            }
﻿            else if (tipus == "d")
﻿            {
﻿                int puskakSzama = int.Parse(adatok[3]);
﻿                return new Diak(nev, szuletesiDatum, puskakSzama);
﻿            }
﻿            else
﻿            {
﻿                throw new ArgumentException("Ismeretlen típus");
﻿            }
﻿        }
﻿    }
﻿}
﻿