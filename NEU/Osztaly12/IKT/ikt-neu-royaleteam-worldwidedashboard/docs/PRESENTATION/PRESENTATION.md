# Presentation Draft / Speaker Notes - World Wide Dashboard

This document contains the speaker notes for the official presentation. The breakdown of the text exactly follows the order of the slides in the presentation (presentation.marp.md).

---

## Slide 1: Title and Project Goal (Ricsi) - 🇭🇺 Magyarul

* **Topic:** A bevezető, a projektünk alapötlete, és hogy miért pont ezt a témát választottuk.
* **Suggested Text:** 
  "Tisztelt Tanárnők és Tanár Urak! / Tisztelt Vizsgabizottság! Amikor elkezdtünk gondolkozni a projekten, arra a következtetésre jutottunk, hogy a mindennapokban rengeteg különböző weboldalt kell használnunk az információk begyűjtésére. Ebből kifolyólag tűztük ki célul a World Wide Dashboard létrehozását. A projektünk alapvető küldetése, hogy egyetlen központosított, letisztult felületen egyesítse a legfontosabb személyes, szórakoztató és gazdasági adatokat."

---

## Slides 2-5: Tech Stack and Architecture (Csongor) - 🇬🇧 In English

* **Topic:** Presentation of the technological background (Languages, Libraries, APIs, and Backend).
* **Suggested Text:**
  * **(Slides 2-3):** "To build this platform, we utilized modern industry standards. Our core framework is Next.js utilizing the App Router, built on React. We wrote the entire application in TypeScript for strict type safety, and we integrated Bun as our runtime environment. For data management, we utilized Prisma ORM."
  * **(Slides 4-5):** "We successfully integrated multiple external data sources, including the Steam Web API, YouTube Data API, and Finance APIs. To manage these elegantly on the backend, we utilized an Object-Oriented Strategy Pattern, strictly calling data via Server Actions to guarantee maximum modularity and security."

---

## Slide 6: Testing (Zalán) - 🇭🇺 Magyarul

* **Topic:** A szoftver tesztelése és stabilitása.
* **Suggested Text:** 
  "Annak érdekében, hogy a szoftverünk ne csupán esztétikus, hanem robusztus és hibamentes is legyen, kiemelt hangsúlyt fektettünk a minőségbiztosításra. A backend folyamatainkat dedikált xUnit integrációs tesztekkel validáltuk, így a külső API-k meghibásodása esetén sem omlik össze a rendszerünk. Ezen felül átfogó manuális tesztelést is végeztünk a frontend felületen, nagy figyelmet fordítva a cross-browser kompatibilitásra és az API végpontok adatbiztonságára."

---

## Slides 7-11: Features, UI, and FAQ (Csongor) - 🇬🇧 In English

* **Topic:** Presentation of the website's features, the User Interface (UI), and answering the Frequently Asked Questions (FAQ).
* **Suggested Text:**
  * **(Slides 7-9):** "Our application provides several core functionalities. Users can safely log in, link their gaming accounts to monitor live Steam statistics, connect their YouTube handles, and efficiently track global cryptocurrencies or political election data all from one single dashboard."
  * **(Slide 10):** "For the User Experience, we engineered a clean card-based layout featuring a dynamic Dark and Light mode switch, heavily optimized for full responsiveness and lazy-loading."
  * **(Slide 11):** "To address a few precise legal questions: First, **is this website legally publishable?** Yes, the site is publishable for portfolio and educational use. However, because we utilize official APIs, we strictly adhere to their Terms of Service, meaning we cannot monetize their data. Secondly, **do we have the legal rights?** We integrated a dedicated Privacy Policy and an MIT License viewer to ensure complete transparency regarding data handling. Thirdly, **is our web scraping legally compliant?** Yes, where official APIs are unavailable, we only scrape public, non-copyrighted statistical data and strictly limit our request rates to respect the target servers. Furthermore, **where is the signed-up users' data stored?** All sensitive user profiles, encrypted passwords, and API preferences are securely stored locally within an SQLite database managed by Prisma. **How can the application be deployed or run locally?** Because we built it with native Docker support and the Bun runtime, it can be spun up instantly using standard containerization or simply by running a single local command. Finally, **what is the foundation of our mock data?** Our mock data, such as election results or political statistics, is based on historically accurate data gathering and static JSON representations of public government statistics."
