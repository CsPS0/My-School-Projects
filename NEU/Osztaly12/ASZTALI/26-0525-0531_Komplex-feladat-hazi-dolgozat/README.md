## Dokumentáció (Marp)

A projekt feladatleírását és bemutatóját tartalmazó `marp.md` fájl a **[Marp](https://marp.app/)** Markdown-alapú prezentációs keretrendszerrel készült.

**Megtekintés és exportálás (PDF, HTML, PPTX):**

1. **VS Code (Ajánlott):** Telepítsd a `Marp for VS Code` kiterjesztést, nyisd meg a `marp.md` fájlt, majd kattints a jobb felső Marp ikonra az exportáláshoz, vagy használd a beépített Markdown előnézetet.
2. **HTML:** Egyszerűen nyisd meg a `Szoftverleltar-feladat.html`-t dupla kattintással, vagy húzd be a kedvenc böngészőjébe.
3. **PDF:** PDF-ben is exportálva van, azonban sajnos egy kicsit el van csúszva.
4. **CLI (Node.js):** Futtasd terminálban az alábbi parancsot a PDF generálásához:
   ```bash
   npx @marp-team/marp-cli marp.md --pdf
   ```