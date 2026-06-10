## 2. Data Fetching Process (Flowchart)
This diagram illustrates the complete lifecycle of data flow: starting from user interaction, through local caching, out to external APIs, and back to the UI.

```mermaid
flowchart TD

    User((User))
    UI[Client Interface / Cards]
    Manager{DashboardManager}
    Cache[(Local Cache)]
    
    subgraph DataSources [External API Services]
        Steam[Steam API]
        YT[YouTube API]
        Pol[Politics API]
        Fin[Finance API]
        Mus[Last.fm API]
        Trn[Tracker.gg API]
        Exo[Exophase Scraper]
    end


    User -->|Open Page| UI
    UI -->|Call fetchAllData method| Manager
    Manager -->|Search by ID| Cache
    
    Cache -- Data is fresh and valid --> Manager
    Cache -- Data is missing or expired --> Steam & YT & Pol & Fin & Mus & Trn & Exo
    
    Steam -->|Raw Data| Manager
    YT -->|Raw Data| Manager
    Pol -->|Raw Data| Manager
    Fin -->|Raw Data| Manager
    Mus -->|Raw Data| Manager
    Trn -->|Raw Data| Manager
    Exo -->|Scraped HTML| Manager
    
    Manager -.->|Save new data| Cache
    Manager -->|Formatted objects| UI
    UI -->|Animated rendering using Anime.js| User
    
    classDef api fill:#14213d,stroke:#fca311,stroke-width:2px,color:#fff;
    class Steam,YT,Pol,Fin,Mus,Trn,Exo api;
```