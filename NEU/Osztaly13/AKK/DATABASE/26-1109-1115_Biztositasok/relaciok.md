# Relációs séma – Biztosítások

## 3/a. Módszer: Egyetlen táblában tárolva (Single Table Inheritance)

- **Ugyfel** (**azonosito** [PK], ugyfel_nev, anyja_neve, szuletesi_ido, email_cim, telefonszam)
- **Szerzodes_Egyben** (**azonosito** [PK], *ugyfel_azonosito* [FK], tipus, kezdeti_datum, veg_datum, helyrajzi_szam [UQ], ingatlan_meret, ingosagok_biztositasi_erteke, alvaz_szam [UQ], rendszam, teljesitmeny_kw, szin)

### Idegen kulcsok
- `Szerzodes_Egyben.ugyfel_azonosito` -> `Ugyfel.azonosito`

### Megjegyzés
- A `tipus` mező különbözteti meg a `lakas` és `gepjarmu` biztosításokat.
- Az adott biztosítástípusra nem vonatkozó attribútumok `NULL` értéket kapnak.

---

## 3/b. Módszer: Külön táblákban tárolva (Class Table Inheritance)

- **Ugyfel** (**azonosito** [PK], ugyfel_nev, anyja_neve, szuletesi_ido, email_cim, telefonszam)
- **Biztositas** (**azonosito** [PK], *ugyfel_azonosito* [FK], kezdeti_datum, veg_datum)
- **Lakasbiztositas** (**biztositas_azonosito** [PK, FK], helyrajzi_szam [UQ], ingatlan_meret, ingosagok_biztositasi_erteke)
- **Gepjarmubiztositas** (**biztositas_azonosito** [PK, FK], alvaz_szam [UQ], rendszam, teljesitmeny_kw, szin)

### Idegen kulcsok
- `Biztositas.ugyfel_azonosito` -> `Ugyfel.azonosito`
- `Lakasbiztositas.biztositas_azonosito` -> `Biztositas.azonosito`
- `Gepjarmubiztositas.biztositas_azonosito` -> `Biztositas.azonosito`
