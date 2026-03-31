using System;
using System.Collections.Generic;
using System.IO;

namespace labirintusConsole
{
    public class LabSim
    {
        private List<string> Adatsorok;
        private char[,] Lab;

        public bool KeresesKesz { get; set; }
        public int KijaratOszlopindex { get; private set; }
        public int KijaratSorindex { get; private set; }
        public bool NincsMegoldas { get; set; }
        public int OszlopokSzama { get; private set; }
        public int SorokSzama { get; private set; }

        public LabSim(string forras)
        {
            Adatsorok = new List<string>();
            BeolvasAdatsorok(forras);
            
            SorokSzama = Adatsorok.Count;
            OszlopokSzama = Adatsorok[0].Length;
            KijaratSorindex = SorokSzama - 2;
            KijaratOszlopindex = OszlopokSzama - 1;
            
            Lab = new char[SorokSzama, OszlopokSzama];
            FeltoltLab();
        }

        private void BeolvasAdatsorok(string forras)
        {
            try
            {
                Adatsorok = new List<string>(File.ReadAllLines(forras));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a beolvasáskor: {ex.Message}");
            }
        }

        private void FeltoltLab()
        {
            for (int i = 0; i < SorokSzama; i++)
            {
                for (int j = 0; j < OszlopokSzama; j++)
                {
                    Lab[i, j] = Adatsorok[i][j];
                }
            }
        }

        public void KiirLab()
        {
            for (int i = 0; i < SorokSzama; i++)
            {
                for (int j = 0; j < OszlopokSzama; j++)
                {
                    Console.Write(Lab[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void Utkereses()
        {
            KeresesKesz = false;
            NincsMegoldas = false;
            int r = 1; // segéd, a vizsgált cella sorindexe
            int c = 0; // segéd, a vizsgált cella oszlopindexe

            while (!KeresesKesz && !NincsMegoldas)
            {
                Lab[r, c] = 'O';
                if (c + 1 < OszlopokSzama && Lab[r, c + 1] == ' ')
                {
                    c++; // jobbra lépünk
                }
                else if (r + 1 < SorokSzama && Lab[r + 1, c] == ' ')
                {
                    r++; // lefelé lépünk
                }
                else
                {
                    Lab[r, c] = '-'; // erre többet nem jövünk
                    if (c - 1 >= 0 && Lab[r, c - 1] == 'O')
                    {
                        c--; // visszalépés balra
                    }
                    else
                    {
                        r--; // visszalépés felfelé
                    }
                }

                KeresesKesz = (r == KijaratSorindex && c == KijaratOszlopindex);
                if (KeresesKesz)
                {
                    Lab[r, c] = 'O';
                }
                NincsMegoldas = (r == 1 && c == 0);
                
                // Szimuláció (8. feladat)
                // Console.Clear();
                // KiirLab();
                // System.Threading.Thread.Sleep(50); 
            }
        }
    }
}
