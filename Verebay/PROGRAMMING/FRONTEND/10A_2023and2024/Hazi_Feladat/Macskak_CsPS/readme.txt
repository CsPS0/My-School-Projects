Solti Csongor Péter - 10.ASZ2

[ajánlott VSC-ben olvasni]

0.Lépés - Ami nem sikerült:
    - bár az "Ugrás az oldal tetejére" benne van a kódban, a CSS-ben nem tudtam reszponzívan kiírni, így az nem látszik.
    - képek egy bizonyos pont után nem reszponzívak (500<px).
    - Murci nem nem fekvő kép, így sajnos kilóg az alja 9:16-on.

1.Lépés - Csinálunk egy mappát az asztalon, mert meg kellene csinálni ezt az érdekes projektet! Legalább 4-es osztályzat kell. 👀

2.Lépés - Ebben a mappában létrehozunk egy "index.html" és egy "style.css" dolgot.
    2A.Lépés - Ha azt szeretné (ön), hogy a mappája jól nézzen ki VSC-ben, töltse le a VSCODE icons kiterjesztést, és a css, js, képek és bootstrap konfigurációkhoz hozzon létre egy mappát, kivéve a html fájlt.
    2B.Lépés - Miután elkészítettük a mappákat, helyezze be a fontos fájlokat a megfelelő helyre. ('min.bootstrap.css', 'bootstrap.bundle.min.js', +kepek).

3.Lépés - A html fájlban elkezdjük beírni a '!' vagy 'html:5'.
    3A.Lépés - Felépítjük a html-t, mint a példában.
    3B.Lépés - Miután felépítettük a fejlécet, a fejlécet, a navigációt, a mainet és a láblécet, behelyeztük a címkéket és az attribútumokat, hogy úgy nézzenek ki, mint a példa. (ezek nagy valószínűséggel divek és szekciók)
    3C.Lépés - A html tagben a nyelvet 'en'-ről 'hu'-ra tesszük.
    3D.Lépés - HEAD:
        a. Behelyeztük az UTF-8 dekódolót.
        b. Behelyeztünk egy meta viewportot, hogy a weboldal reszponzív legyen.
        c. Betesszük a 2 css stíluslapot, a link címkével.
        d. A címsorba olyasmit írunk, ami hasonló a weboldal témájához.
        e. Hozzáadunk egy ikont, aminek a neve web_icon.jpg (opcionális, de ajánlott, hogy jól nézzen ki.)
        f. Ha akarjuk, hozzáadhatunk egyedi metacímkéket, például leírást, kulcsszavakat vagy szerzőt.
    3E.Lépés - HEADER:
        a. Létrehozunk egy divet, aminek megadjuk a felso-kep és az img-fluid class-t, majd egy top id-t is.
        b. Képnek brakjuk a header.jpg képet, szabadon eldöntjük, addunk-e altot és title-t. Class-nak az img-fluid-ot adjuk.
        c. A szövegnek is csinálunk egy divet, melynek a text-overlay class-t adjuk.
        d. Az alcímbe (h3) beírjuk amit kell. (lásd a dokkumentáció végén)
    3F.Lépés - NAV:
        a. Létrehozunk egy navot, majd megadjuk a classokat, amik a navbar,navbar-expand-lg, navbar-light, és a rounded-4-et.
        b. Létrehozunk egy divet és megadunk neki egy container-fluid class-ot.
        c. Létrehozunk még egy divet és szintén megadjuk a navbar-collapse és a justify-content-center class-t.
        d. Létrehozunk ehy ul-t aminek a navbar-nav class-t adjuk.
        e. Végül létrehozunk 5 db li-t amiknek nav-item a class-a, ezeken belül az a tageknek, pedig nav-linke classot.
    3G.Lépés - MAIN:
        a. Csinálunk egy divet, aminek a container-fluid classot adjuk.
        b. Csinálunk egy divet, aminek a content-section classot adjuk.
        c. Csinálunk 5 db divet, aminek a content-box, border, border-2, border-dark, classokat és a Cicához tartozó id-t adjuk.
        d. Hozzáadunk egy képet a macskának megfelelően mind az 5 divhez aminek a macska-kap és img-fluid classokat adjuk.
        e. Addunk egy szöveget a macskának megfelelően mind az 5 divhez.
        f. Addunk mind az 5 divhez egy "Ugrás az oldal tetejére" linket.
    3H.Lépés - FOOTER:
        a. Létrehozunk egy footer-t és adunk neki egy footer és container-fluid class-t.
        b. Addunk egy alcímet(h5) és még kettő paragrafust.
        c. Végül megadjuk a visszagombot a jobb alsó sarokba.
    3.I.Lépés - Script:
        - Hozzáadjuk az oldal aljára a bootstrap.bundle.min.js-t.

4.Lépés - CSS [felesleges mindent is leírni]:
    a.Általános stílusok: megadjuk az általunk lemért vagy jónak gondolt adatokat.
    b.HEADER: beálítjuk a .felso-kepet és a .text-overlay-t.
    c.NAVBAR: navbárnál minden elemét a minte szerint formázzunk.
    d.Content-Section: megformázzuk a betűket képeket linket stb.
    e.Footer: berakjuk középre a szövegeket.
    f.@media: beállítjuk a dolgokat, hogy nagyjából reszponzívak legyenek.

5.Másolandó szövegek:
    A.Header:
        a. MACSKÁK MINDEN MENNYISÉGBEN!
        b. A macskák évszázadok óta jelen vannak az emberek életében. Nem véletlen, hogy sokan imádják ezeket az állatokat, hiszen úgy vélik, hogy önálló és független természetük rendkívül vonzó.
    
    B.Content-Section:
        a. Cirmi:
            - Ő itt Cirmi
            - Ő egy olyan macska, ami jól alkalmazkodik az emberrel való együttéléshez, nem alá-, vagy fölérendelt, hanem mellérendelt köztük a viszony. Élvezi, hogy körülötted lehet, szeretetteljes, és nagyrészt kiegyensúlyozott. Jól alkalmazkodik, sok szempontból ő az ideális macska és társ. Boldogan tölt veled időt anélkül, hogy agressziót vagy erőszakot alkalmazna, bármilyen negatív viselkedést produkálna.
        
        b. Murci:
            - Ő itt Murci
            - Bár nem teljesen elvadúlt típus, ennek ellenére nem igazán szereti, ha megérintik, felveszik vagy interakcióba lépnek vele (természetesen az etetést és a jutalomfalatokat kivéve). Gyakran frusztrált, és abban sem lehetsz biztos, hogy számára lényelmes az, hogy Te is osztozol vele a házon.
        
        c. Szotyi:
            - Ő itt Szotyi
            - Igazán társas lény, csak éppen nem téged, hanem egy másik macskát választ társnak. Igazán szeretetteljes, bújós és dorombolós, de ideje nagy részét másik macskával való összefonódással, ápoltgatással és játékkal tölti.
        
        d. Pille:
            - Ő itt Pille
            - Lehet, hogy az egyik másodpercben még az öledben fekve dorombol, a következőben pedig a legnagyobb sebességel  rohangál és a függönyt tépi.
        
        e. Berci:
            - Ő itt Berci
            - Szívesen és gyakran kerül összetűzésbe a ház többi lakójával, jól bírja a konfrontációt, lesből is szuperül támad bokára! A határozottság és a következetesség sokat segít a visekedése tompításásban! A gazdinak mindenképpen ki kell okosodnia, hogy lehet a dominanciájának határt szabni!
        
        f. Kopasz:
            - Ő itt Kopasz
            - A cica nem megy oda bárkihez, és csak azokat tiszteli meg jelenlétével, akik kiérdemlik azt. Ápolása: elhivatott gazdit igényel! A sűrű aljszőr rendkívüli módón tud filcesedni, a keletkezett csomók kibontása pedig nemcsak nehézkes, de az állat számára fájdalmas is lehet. A legjobb mód leborotválni.
    
    C.Footer:
        a. MÉG MINDÍG AKARSZ MACSKÁT?
        b. Ha szeretsz minden reggel 4:50-kor kelni, utálod, ha van magánéleted, és nem bírsz meglenni a kiszóródott cicakaja felsöprése nélkül, akkor ez téged érint.
        c. © Copyright 2024

6.Az általam használt bővítmények [csak frontendekhez (úgy értem, hogy vannak más is, csak lehet, hogy túl sok adat van ebben a szakaszban)]:
    - Auto Close Tag
    - Auto Rename Tag
    - colorize
    - CSS Flexbox Cheatsheets
    - CSS Peek
    - HTML Class Suggestions
    - HTML CSS Support
    - HTML Format
    - Icon Fonts
    - indent-rainbow
    - Live Server
    - Peacock
    - Polacode
    - Prettier (not  just an extension, u actually have to change setting in the        settings.json vsc file)
    - Turbo Console Log
    - vscode-icons (most important one)
    - vscode-pdf