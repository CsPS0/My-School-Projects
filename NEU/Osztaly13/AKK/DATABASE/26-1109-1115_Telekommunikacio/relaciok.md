# Relációs séma – Telekommunikációs szerződések

## 3/a. Módszer: Egyetlen táblában tárolva (Single Table Inheritance)

- **Ugyfel** (**azonosito** [PK], ugyfel_nev, anyja_neve, szuletesi_ido, email_cim, telefonszam)
- **Szerzodes_Egyben** (**szerzodes_szam** [PK], *ugyfel_azonosito* [FK], szolgaltatas_tipus, kezdesi_datum, fizetendo_osszeg, telefonszam [UQ], mobil_tipus, huseg_lejarat, csomag_nev, extra_koltseg, max_letoltes, max_feltoltes, router_tulajdon)

### Idegen kulcsok
- `Szerzodes_Egyben.ugyfel_azonosito` -> `Ugyfel.azonosito`

### Megjegyzés
- A `szolgaltatas_tipus` mező különbözteti meg a `mobiltelefon`, `kabelteve` és `internet` szerződéseket.
- Az adott szerződéstípusra nem vonatkozó mezők `NULL` értéket kapnak.

---

## 3/b. Módszer: Külön táblákban tárolva (Class Table Inheritance)

- **Ugyfel** (**azonosito** [PK], ugyfel_nev, anyja_neve, szuletesi_ido, email_cim, telefonszam)
- **Szerzodes** (**szerzodes_szam** [PK], *ugyfel_azonosito* [FK], kezdesi_datum, fizetendo_osszeg)
- **Mobiltelefon** (**szerzodes_szam** [PK, FK], telefonszam [UQ], tipus, huseg_lejarat)
- **Kabelteve** (**szerzodes_szam** [PK, FK], csomag_nev, extra_koltseg)
- **Internet** (**szerzodes_szam** [PK, FK], max_letoltes, max_feltoltes, router_tulajdon)

### Idegen kulcsok
- `Szerzodes.ugyfel_azonosito` -> `Ugyfel.azonosito`
- `Mobiltelefon.szerzodes_szam` -> `Szerzodes.szerzodes_szam`
- `Kabelteve.szerzodes_szam` -> `Szerzodes.szerzodes_szam`
- `Internet.szerzodes_szam` -> `Szerzodes.szerzodes_szam`
