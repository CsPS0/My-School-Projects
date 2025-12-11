# Teszt Jegyzőkönyv

**Projekt:** solticsongor-DemoBlaze
**Dátum:** 2025.12.07.
**Környezet:** 
- Operációs rendszer: Windows (win64)
- Böngésző: Chromium Based
- Keretrendszer: .NET 9.0, MSTest, Selenium WebDriver

## Tesztesetek Összefoglalása

A tesztelés során a [https://www.demoblaze.com/](https://www.demoblaze.com/) weboldal 4 funkcióját vizsgáltuk automatizált Selenium tesztekkel.

| Teszt Azonosító | Teszt Neve | Leírás | Elvárt Eredmény | Eredmény |
| :--- | :--- | :--- | :--- | :--- |
| **TC-01** | `Test1_HomePageTitle_ShouldBeStore` | A főoldal megnyitása és a böngészőfül címének ellenőrzése. | Az oldal címe: "STORE". | **DOMER✅** |
| **TC-02** | `Test2_CategoryNavigation_Laptops_ShouldFilterProducts` | Navigáció a "Laptops" kategóriára és a terméklista frissülésének ellenőrzése. | A listában megjelenik egy laptop (pl. "Sony vaio i5"). | **DOMER✅** |
| **TC-03** | `Test3_AddProductToCart_ShouldShowAlert` | Egy kiválasztott termék (Samsung galaxy s6) kosárba helyezése. | Megjelenik egy "Product added" üzenetű felugró ablak (alert). | **DOMER✅** |
| **TC-04** | `Test4_ContactModal_ShouldOpen` | A "Contact" menüpontra kattintás a navigációs sávban. | Megjelenik a "New message" című kapcsolati űrlap (modal). | **DOMER✅** |