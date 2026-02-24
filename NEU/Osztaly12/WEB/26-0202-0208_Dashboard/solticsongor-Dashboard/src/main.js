import "@assets/app.css";
import { formatEmoji } from "./js/helpers.js";

const h = { "Accept": "application/json" };

fetch("https://randomuser.me/api?nat=us,gb", { headers: h })
  .then(r => r.json())
  .then(d => {
    const u = d.results[0];
    document.getElementById("name").textContent = `${u.name.first} ${u.name.last}`;
  });

fetch("https://emojihub.yurace.pro/api/random/category/smileys-and-people", { headers: h })
  .then(r => r.json())
  .then(d => {
    document.getElementById("emoji").textContent = formatEmoji(d.unicode[0]);
  });

fetch("https://api.fiscaldata.treasury.gov/services/api/fiscal_service/v1/accounting/od/rates_of_exchange?filter=country_currency_desc:eq:Hungary-Forint,record_date:eq:2025-12-31", { headers: h })
  .then(r => r.json())
  .then(d => {
    const rate = parseFloat(d.data[0].exchange_rate);
    document.getElementById("huf").textContent = (Math.round(rate * 100) / 100).toFixed(2);
  });

fetch("https://api.spaceflightnewsapi.net/v4/articles/?limit=6", { headers: h })
  .then(r => r.json())
  .then(d => {
    const container = document.getElementById("news");
    const template = document.getElementById("news-template");
    d.results.forEach(a => {
      const clone = template.content.cloneNode(true);
      const img = clone.querySelector("img");
      img.src = a.image_url;
      img.alt = a.title;
      clone.querySelector("h3").textContent = a.title;
      clone.querySelector("p").textContent = a.summary;
      container.appendChild(clone);
    });
  });

fetch("https://api.open-meteo.com/v1/forecast?latitude=47.29&longitude=29.04&daily=temperature_2m_max,temperature_2m_min,rain_sum&timezone=Europe%2FBerlin", { headers: h })
  .then(r => r.json())
  .then(d => {
    const container = document.getElementById("weather");
    const template = document.getElementById("weather-template");
    const daily = d.daily;
    daily.time.forEach((t, i) => {
      const clone = template.content.cloneNode(true);
      clone.querySelector(".time").textContent = t;
      clone.querySelector(".max").textContent = Math.round(daily.temperature_2m_max[i]);
      clone.querySelector(".min").textContent = Math.round(daily.temperature_2m_min[i]);
      clone.querySelector(".rain").textContent = daily.rain_sum[i];
      container.appendChild(clone);
    });
  });
