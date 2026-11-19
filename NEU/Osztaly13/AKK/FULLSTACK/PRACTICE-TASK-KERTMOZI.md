# Practice task: Kertmozi (frontend, Vue komponensek)

This is an invented exam in the same style as ÖBB, CourseHUB, ASDA and Kaufland. It's ÖBB-sized and mixes in all the newer material.

- Each task has the **Hungarian sentence the way a PDF would write it**, then **what it means / what to do**, then a **small piece of code**.
- At the end of each component or page there's the **whole file**.
- The whole solution was run on the `fullstack2627` base in your VM and checked in a real browser: every task works, with no console errors or warnings.

**How to practise:** read the Hungarian sentence, cover the rest, and write the code yourself. Then compare.

Where things are in the VM:

| Folder | What |
|---|---|
| `~/Projects/gyakorlas-kertmozi` | the **finished solution**, running (open `http://frontend.vm1.test` in the VM) |
| your own clone (see "Start your own attempt") | where you practise |

Pattern numbers like **(4.19)** point to `FRONTEND-TEST-GUIDE.md`.

---

## Start your own attempt (in the VM's terminal)

```bash
cd ~/Projects/gyakorlas-kertmozi && docker compose stop     # only one project can use port 80
cd ~/Projects
git clone https://github.com/rcsnjszg/fullstack2627 sajat-kertmozi
cd sajat-kertmozi
just init
docker compose exec backend php artisan install:api -n
cp ~/Projects/gyakorlas-kertmozi/backend/practice-movies.json backend/
cp ~/Projects/gyakorlas-kertmozi/backend/routes/api.php backend/routes/
```

Check the API at `http://backend.vm1.test/api/movies` (in the VM's browser). The data comes wrapped in `data`, and the ids are numbers.

To switch back to the solution: `docker compose stop` in `sajat-kertmozi`, then `docker compose up -d` in `gyakorlas-kertmozi`.

---

# The "PDF"

> **Kertmozi**
>
> Az Ön feladata egy szabadtéri mozi (Kertmozi) műsorát bemutató weboldal elkészítése. A szerveroldali kiszolgáló már elkészült, a feladat csak a kliensoldali alkalmazás.

### A movies tábla adatszerkezete

| Mező neve | Típus | Leírás |
|---|---|---|
| id | Egész [PK] | A film azonosítója |
| title | Szöveg | A film címe |
| genre | Szöveg | A film műfaja (drama, comedy, animation, thriller) |
| duration | Egész | A film hossza percben |
| age_limit | Egész | Korhatár (0 = korhatár nélkül) |
| price | Egész | Normál jegyár forintban |
| discount | Valós | Kedvezmény mértéke (pl. 0.2), null ha nincs |
| vip_price | Egész | VIP jegy ára, null ha nincs VIP jegy |
| is_premiere | Logikai | Premier film-e |
| image | Szöveg | A plakát képfájljának neve |
| description | Szöveg | Rövid leírás |
| screenings | Tömb | Vetítések: `{ id, time, hall }` |

API: `GET /api/movies`, `GET /api/movies/{id}`

---

## Beállítások

### 1.
> Állítsa be a környezeti változókban, hogy az alkalmazás neve „Kertmozi” legyen! Gondoskodjon róla, hogy a mintának szánt fájlban is módosítva legyen!

**What to do:** change `APP_NAME` in the root `.env` **and** `.env.example` ("mintának szánt fájl" = the example file). The tab title becomes "… | Kertmozi". Then restart the frontend.

```bash
# .env and .env.example
APP_NAME=Kertmozi
```

```bash
docker compose restart frontend
```

### 2.
> Állítsa át, hogy az oldal nyelve magyar legyen!

**What to do:** set the `lang` attribute in `frontend/index.html`.

```html
<html lang="hu">
```

### 3.
> Állítsa be a main.css-ben, hogy a primary változó értéke #b91c1c, a primary-foreground fehér legyen!

**What to do:** in `frontend/src/assets/main.css`, in the **first** block (`:root`, not `.dark`), replace the two values. After that, `bg-primary`, `text-primary` and `outline-primary` use this red.

```css
:root {
  /* ... */
  --primary: #b91c1c;
  --primary-foreground: #ffffff;
```

### 4.
> A main.css fájl végére illessze be az alábbi kódrészletet, amely egy screen-reader-text osztállyal bővíti a utilities réteget!

**What to do:** paste it at the end of `main.css`. Elements with this class are hidden from the eye, but screen readers still read them.

```css
@layer utilities {
  .screen-reader-text {
    @apply absolute h-px w-px -m-px overflow-hidden p-0;
  }
}
```

---

## Útvonalak

### 5.
> Valósítsa meg a kliensoldali útvonalválasztást az alábbiak szerint!
>
> | Útvonal neve | URI | Oldal címe |
> |---|---|---|
> | index | / | Műsor |
> | movies.show | /movies/:id | Film |

**What to do:** the base uses **file-based routing**. Create the files and put a `<route>` block at the bottom of each. `:id` in the URL means `[id]` in the file name.

| File | URL |
|---|---|
| `src/pages/index.vue` | `/` |
| `src/pages/movies/[id].vue` | `/movies/:id` |

```vue
<!-- bottom of pages/index.vue -->
<route lang="yaml">
name: index
meta:
  title: Műsor
</route>

<!-- bottom of pages/movies/[id].vue -->
<route lang="yaml">
name: movies.show
meta:
  title: Film
</route>
```

---

## Segédfüggvények

### 6.
> Hozza létre a utils/helper.js fájlt! Exportáljon belőle egy genres objektumot, amely a műfajok magyar megnevezését tartalmazza az alábbi táblázat alapján!
>
> | genre | Magyar megnevezés |
> |---|---|
> | drama | Dráma |
> | comedy | Vígjáték |
> | animation | Animáció |
> | thriller | Thriller |

**What to do:** a table "value → name" becomes an **object**. "Exportáljon" means `export`, so other files can `import` it.

```js
export const genres = {
  drama: 'Dráma',
  comedy: 'Vígjáték',
  animation: 'Animáció',
  thriller: 'Thriller'
}
```

### 7.
> Bővítse a helper.js fájlt egy formatDuration függvénnyel, amely a percben megadott hosszt „2 óra 11 perc” vagy „45 perc” formában adja vissza attól függően, hogy egy óránál hosszabb-e!

**What to do:** integer division by 60 gives the hours, `% 60` the remaining minutes. Show the hour part only if it's more than 0.

```js
export function formatDuration(minutes) {
  const hours = Math.floor(minutes / 60)
  const rest = minutes % 60
  return hours > 0 ? `${hours} óra ${rest} perc` : `${rest} perc`
}
```

---

## Komponensek

### A Badge komponens

### 8.
> Készítsen egy komponenst Badge néven, amely a szülőtől egy color nevű, szöveg típusú, opcionális prop-ot kap, amelynek alapértelmezett értéke üres szöveg legyen!

**What to do:** create `src/components/Badge.vue` with `defineProps`, type `String` and `default: ''`. You need `props.` later in the computed, so store it in a variable.

```js
const props = defineProps({
  color: { type: String, default: '' }
})
```

### 9.
> Hozzon létre egy számított állapotot az alábbi táblázat alapján, amely a color értéke alapján megadja a színeket! Ha a táblázatban nem szereplő értéket kap, akkor bg-gray-100 text-gray-800 legyen!
>
> | color | Osztályok |
> |---|---|
> | drama | bg-purple-100 text-purple-800 |
> | comedy | bg-yellow-100 text-yellow-800 |
> | animation | bg-sky-100 text-sky-800 |
> | thriller | bg-gray-800 text-white |

**What to do:** "számított állapot" means `computed`, and the table becomes a lookup object (4.19). `??` gives the fallback for anything not in the table.

```js
const colorClass = computed(() => {
  const colors = {
    drama: 'bg-purple-100 text-purple-800',
    comedy: 'bg-yellow-100 text-yellow-800',
    animation: 'bg-sky-100 text-sky-800',
    thriller: 'bg-gray-800 text-white'
  }
  return colors[props.color] ?? 'bg-gray-100 text-gray-800'
})
```

### 10.
> A komponens egyetlen span elemet tartalmazzon, amely vízszintesen 2, függőlegesen 1 egység belső margót kapjon, legyen teljesen lekerekített, a betűmérete extra kicsi, a vastagsága közepes legyen! Kapja meg a számított állapot osztályait is! A tartalma slot-on keresztül jelenjen meg!

**What to do:**
- belső margó vízszintesen / függőlegesen → `px-2 py-1`
- teljesen lekerekített → `rounded-full`
- extra kicsi → `text-xs`, közepes vastagság → `font-medium`
- the static classes go in `class`, the computed ones in `:class`. Vue merges the two.

**Whole file `components/Badge.vue`:**

```vue
<script setup>
import { computed } from 'vue'

const props = defineProps({
  color: { type: String, default: '' }
})

const colorClass = computed(() => {
  const colors = {
    drama: 'bg-purple-100 text-purple-800',
    comedy: 'bg-yellow-100 text-yellow-800',
    animation: 'bg-sky-100 text-sky-800',
    thriller: 'bg-gray-800 text-white'
  }
  return colors[props.color] ?? 'bg-gray-100 text-gray-800'
})
</script>

<template>
  <span class="px-2 py-1 rounded-full text-xs font-medium" :class="colorClass">
    <slot />
  </span>
</template>
```

### A Button komponens

### 11.
> Készítsen egy Button komponenst, amely a szülőtől a type és variant nevű, opcionális stringeket kapja! A type alapértelmezett értéke button, a variant alapértelmezett értéke primary legyen!

**What to do:** two optional props, each with a `default`.

```js
defineProps({
  type: { type: String, default: 'button' },
  variant: { type: String, default: 'primary' }
})
```

### 12.
> Hozzon létre egy classes objektumot a két lehetséges variant értékhez!
> - primary: primary háttérszín, primary-foreground szövegszín; ha a felhasználó ráviszi a kurzort, fehér háttér és primary szövegszín, valamint primary színű körvonal jelenjen meg.
> - outline: fehér háttér, primary szövegszín és primary színű szegély.

**What to do:** a plain object (not `computed`, because the PDF says "objektum"). "ráviszi a kurzort" means `hover:`. A körvonal (outline) and a szegély (border) are different things.

```js
const classes = {
  primary: 'bg-primary text-primary-foreground hover:bg-white hover:text-primary hover:outline outline-primary',
  outline: 'bg-white text-primary border border-primary'
}
```

### 13.
> A komponensben legyen egy gomb, amely attribútumként kapja meg a type értékét, a px-4 py-2 rounded cursor-pointer osztályok mellett kapja meg az aktuális variant osztályait! A gomb tartalmát a szülőelem adja meg!

**What to do:** `:type="type"`, an array `:class` (fixed string + `classes[variant]`), and a `<slot />`.

**Whole file `components/Button.vue`:**

```vue
<script setup>
defineProps({
  type: { type: String, default: 'button' },
  variant: { type: String, default: 'primary' }
})

const classes = {
  primary: 'bg-primary text-primary-foreground hover:bg-white hover:text-primary hover:outline outline-primary',
  outline: 'bg-white text-primary border border-primary'
}
</script>

<template>
  <button :type="type" :class="['px-4 py-2 rounded cursor-pointer', classes[variant]]">
    <slot />
  </button>
</template>
```

### A Price komponens

### 14.
> Készítsen egy Price komponenst, amely két számot kap a szülőtől: a price legyen kötelező, a discount opcionális, alapértelmezett értéke 0!

```js
const props = defineProps({
  price: { type: Number, required: true },
  discount: { type: Number, default: 0 }
})
```

### 15.
> Készítsen egy finalPrice nevű számított állapotot, amely a kedvezményes árat adja vissza egészre kerekítve, az alábbi képlet alapján: price - (price * discount)! A komponens egy span elemben jelenítse meg forintban (pl. „1600 Ft”)!

**What to do:** the formula goes literally into a `computed` named `finalPrice`. "Egészre kerekítve" means `Math.round`.

⚠️ The `default: 0` only applies if the parent passes **nothing** (`undefined`). If the data contains `null`, pass `?? 0` from the parent (see task 20).

**Whole file `components/Price.vue`:**

```vue
<script setup>
import { computed } from 'vue'

const props = defineProps({
  price: { type: Number, required: true },
  discount: { type: Number, default: 0 }
})

const finalPrice = computed(() => Math.round(props.price - props.price * props.discount))
</script>

<template>
  <span>{{ finalPrice }} Ft</span>
</template>
```

### Az Input komponens

### 16.
> Készítsen egy Input komponenst, amely v-model-t kap a szülőelemtől! Benne egy beviteli mező legyen, amely ezt a modelt kapja meg, teljes szélességű, 2 egység belső margójú, gray-400 színű szegélyű, lekerekített sarkú legyen, és fókusz esetén kapjon 2 pixeles primary színű outline-t!

**What to do:** "v-model-t kap" means `defineModel()`. Attributes the parent writes on `<Input>` (`id`, `placeholder`, `autocomplete`, `class`) automatically land on the `<input>` (fallthrough, 4.7).

**Whole file `components/Input.vue`:**

```vue
<script setup>
const model = defineModel()
</script>

<template>
  <input v-model="model" class="w-full p-2 border border-gray-400 rounded focus:outline-2 focus:outline-primary" />
</template>
```

### A Navbar komponens

### 17.
> Készítsen egy Navbar komponenst! Hozzon létre benne egy objektumokból álló tömböt links néven az alábbi táblázat alapján! A page kulcs értéke egy olyan objektum legyen, amelyet át tud adni egy RouterLink elemnek!
>
> | title | page |
> |---|---|
> | Műsor | index |
> | Jegyárak | index |
> | Kapcsolat | index |

**What to do:** "átadható RouterLink-nek" means `{ name: 'index' }`.

```js
const links = [
  { title: 'Műsor', page: { name: 'index' } },
  { title: 'Jegyárak', page: { name: 'index' } },
  { title: 'Kapcsolat', page: { name: 'index' } }
]
```

### 18.
> Hozzon létre egy open nevű állapotot, amelynek alapértelmezett értéke hamis! Készítsen egy openClass nevű számított állapotot, amely ha az open igaz, a max-h-60, különben a max-h-14 md:max-h-none stringet adja vissza! Ezt adja hozzá a nav elem meglévő osztályaihoz!

**What to do:** `ref(false)` plus a `computed` with a ternary. "Adja hozzá a meglévőkhöz": in `:class`, an array of [the fixed string, openClass].

```js
const open = ref(false)
const openClass = computed(() => (open.value ? 'max-h-60' : 'max-h-14 md:max-h-none'))
```

### 19.
> A nav elemen belül egy div-ben helyezzen el egy gombot (☰), amelyre kattintva az open állapot az ellentétére változzon; közepes eszköztől rejtse el! Mellette egy RouterLink „Kertmozi” szöveggel a főoldalra mutasson! A div alatt a links tömbből generáljon RouterLink-eket, amelyek szövege középre igazított és flex-grow értéke 1!

**What to do:**
- toggle: `@click="open = !open"`
- "közepes eszköztől rejtse el": `md:hidden`
- "flex-grow 1": `grow`
- give the menu button an `aria-label`, because it has no readable text

**Whole file `components/Navbar.vue`:**

```vue
<script setup>
import { ref, computed } from 'vue'
import { RouterLink } from 'vue-router'

const links = [
  { title: 'Műsor', page: { name: 'index' } },
  { title: 'Jegyárak', page: { name: 'index' } },
  { title: 'Kapcsolat', page: { name: 'index' } }
]

const open = ref(false)

const openClass = computed(() => (open.value ? 'max-h-60' : 'max-h-14 md:max-h-none'))
</script>

<template>
  <nav
    :class="[
      'sticky top-0 z-50 bg-white shadow flex flex-col md:flex-row md:items-center gap-2 p-3 overflow-hidden transition-all',
      openClass
    ]"
  >
    <div class="flex items-center gap-3">
      <button type="button" class="md:hidden cursor-pointer text-2xl" aria-label="Menü" @click="open = !open">☰</button>
      <RouterLink :to="{ name: 'index' }" class="text-2xl font-bold text-primary">Kertmozi</RouterLink>
    </div>
    <RouterLink v-for="(link, index) in links" :key="index" :to="link.page" class="grow text-center">
      {{ link.title }}
    </RouterLink>
  </nav>
</template>
```

### A MovieCard komponens

### 20.
> Készítsen egy MovieCard komponenst, amely kötelezően egy movie nevű objektumot kap a szülőtől! Készítsen benne egy genreText nevű számított állapotot, amely a helper.js genres objektuma alapján a film műfajának magyar nevét adja vissza!

**What to do:** a required `Object` prop, and the import from `helper.js`. If the PDF names the computed (`genreText`), it **must** exist under that name. Don't do the lookup inline in the template.

```js
import { genres, formatDuration } from '@utils/helper.js'

const props = defineProps({
  movie: { type: Object, required: true }
})

const genreText = computed(() => genres[props.movie.genre] ?? props.movie.genre)
```

### 21.
> A kártyán jelenjen meg:
> a) a film plakátja (a képek az images mappában találhatók), alternatív szövege a film címe legyen;
> b) a film címe LG méretű, félkövér 3-as szintű címsorban;
> c) egy Badge, amely color-ként a film műfaját kapja, tartalma a genreText legyen; mellette a film hossza a formatDuration függvénnyel formázva;
> d) a Price komponens a film árával és kedvezményével;
> e) egy „Részletek” feliratú link, amely a film oldalára mutat.

**What to do:**
- a) `public/images/x.jpg` is reached as `/images/x.jpg`
- c) a function used in the template must be imported in `<script setup>`
- d) `discount` is `null` for some films, hence `?? 0`
- e) `RouterLink` with `name` + `params`

**Whole file `components/MovieCard.vue`:**

```vue
<script setup>
import { computed } from 'vue'
import { RouterLink } from 'vue-router'
import Badge from '@components/Badge.vue'
import Price from '@components/Price.vue'
import { genres, formatDuration } from '@utils/helper.js'

const props = defineProps({
  movie: { type: Object, required: true }
})

const genreText = computed(() => genres[props.movie.genre] ?? props.movie.genre)
</script>

<template>
  <div class="bg-white rounded-xl shadow flex flex-col overflow-hidden">
    <img :src="`/images/${movie.image}`" :alt="movie.title" class="w-full aspect-[2/3] object-cover bg-gray-200" />
    <div class="p-4 flex flex-col gap-2 grow">
      <h3 class="text-lg font-bold">{{ movie.title }}</h3>
      <div class="flex items-center gap-2">
        <Badge :color="movie.genre">{{ genreText }}</Badge>
        <span class="text-sm text-gray-500">{{ formatDuration(movie.duration) }}</span>
      </div>
      <p class="font-bold mt-auto"><Price :price="movie.price" :discount="movie.discount ?? 0" /></p>
      <RouterLink
        :to="{ name: 'movies.show', params: { id: movie.id } }"
        class="bg-primary text-primary-foreground text-center rounded-full py-2 px-4"
      >
        Részletek
      </RouterLink>
    </div>
  </div>
</template>
```

### A SearchForm komponens

### 22.
> Készítsen egy SearchForm komponenst, amely v-model-t kap a szülőtől! Ez egy objektum lesz, amely a szűrési feltételeket tartalmazza (title, genre, premiereOnly)! Kötelezően kapjon egy genres nevű objektumot is!

**What to do:** `defineModel` with an object type, plus an ordinary prop. You change the object's keys (`model.title`), and the parent's `search` changes with them.

```js
const model = defineModel({ type: Object, required: true })

defineProps({
  genres: { type: Object, required: true }
})
```

### 23.
> Az űrlapon helyezzen el:
> a) egy title azonosítójú Input komponenst, amely a model title kulcsához legyen kötve, „Film címe” helykitöltő szöveggel, kikapcsolt automatikus kiegészítéssel;
> b) egy genre azonosítójú lenyíló listát, amely a model genre kulcsához legyen kötve! Az üres értékű „Minden műfaj” opció alá generáljon minden műfajnak egy-egy opciót, amelynek értéke az angol, szövege a magyar név legyen;
> c) egy premiere azonosítójú jelölőnégyzetet „Csak premierek” címkével, a model premiereOnly kulcsához kötve!
> Az űrlap elküldését akadályozza meg (prevent)!

**What to do:**
- a) `v-model="model.title"` **on your own `Input` component**. The `id` falls through to the `<input>`.
- b) `v-for="(label, key) in genres"`: over an **object** the order is (value, key) (4.20).
- c) on a checkbox, `v-model` gives `true`/`false`.
- prevent: `@submit.prevent` with no handler, so Enter doesn't reload the page.

```vue
<Input id="title" v-model="model.title" placeholder="Film címe" autocomplete="off" />
<select id="genre" v-model="model.genre">
  <option value="">Minden műfaj</option>
  <option v-for="(label, key) in genres" :key="key" :value="key">{{ label }}</option>
</select>
<input id="premiere" v-model="model.premiereOnly" type="checkbox" />
```

### 24.
> Helyezzen el az űrlapon egy outline variant-ú Button komponenst „Szűrők törlése” felirattal, amelyre kattintva a komponens emit-elje a reset eseményt! Ne felejtse a defineEmits-et!

**What to do:** `defineEmits(['reset'])`, then `emit('reset')`. `@click` on your Button component reaches the inner `<button>` (fallthrough). It's not a submit button, because the Button's default `type` is `button`.

**Whole file `components/SearchForm.vue`:**

```vue
<script setup>
import Input from '@components/Input.vue'
import Button from '@components/Button.vue'

const model = defineModel({ type: Object, required: true })

defineProps({
  genres: { type: Object, required: true }
})

const emit = defineEmits(['reset'])
</script>

<template>
  <form class="grid md:grid-cols-4 gap-4 items-center bg-white p-4 rounded-xl shadow" @submit.prevent>
    <Input id="title" v-model="model.title" placeholder="Film címe" autocomplete="off" class="md:col-span-2" />
    <select id="genre" v-model="model.genre" class="p-2 border border-gray-400 rounded">
      <option value="">Minden műfaj</option>
      <option v-for="(label, key) in genres" :key="key" :value="key">{{ label }}</option>
    </select>
    <label class="flex items-center gap-2">
      <input id="premiere" v-model="model.premiereOnly" type="checkbox" />
      Csak premierek
    </label>
    <Button variant="outline" class="md:col-start-4" @click="emit('reset')">Szűrők törlése</Button>
  </form>
</template>
```

### A TicketCard komponens

### 25.
> Készítsen egy TicketCard komponenst, amely kötelezően egy title (szöveg) és egy price (szám) értéket kap! Jelenítse meg a címet 3-as szintű címsorban, az árat forintban egy bekezdésben! Alatta egy „Kiválasztom” feliratú Button legyen, amelyre kattintva a komponens emit-elje a selected eseményt!

**Whole file `components/TicketCard.vue`:**

```vue
<script setup>
import Button from '@components/Button.vue'

defineProps({
  title: { type: String, required: true },
  price: { type: Number, required: true }
})

const emit = defineEmits(['selected'])
</script>

<template>
  <div class="bg-white border rounded-xl p-4 flex flex-col items-center gap-2">
    <h3 class="text-lg font-bold">{{ title }}</h3>
    <p class="text-2xl font-black">{{ price }} Ft</p>
    <Button @click="emit('selected')">Kiválasztom</Button>
  </div>
</template>
```

---

## Oldalak

### Az index oldal (`pages/index.vue`)

### 26.
> Az oldal tetején jelenjen meg a Navbar komponens, alatta egy main elem, amely maximum 6xl széles, középre igazított, vízszintesen 4, függőlegesen 8 egység belső margójú!

```vue
<Navbar />
<main class="max-w-6xl mx-auto px-4 py-8">
  ...
</main>
```

(This page doesn't use `BaseLayout`, because it has its own Navbar, like ÖBB. If a PDF says nothing, `<BaseLayout>` is fine.)

### 27.
> Helyezzen el a main elembe egy „Kertmozi” szövegű főcímet, amely kapja meg a screen-reader-text osztályt! Alatta egy „Műsoron” szövegű, 3XL méretű, félkövér alcím legyen 6 egység alsó margóval!

```vue
<h1 class="screen-reader-text">Kertmozi</h1>
<h2 class="text-3xl font-bold mb-6">Műsoron</h2>
```

### 28.
> Hozzon létre egy movies nevű állapotot a filmek tárolására, valamint egy loading állapotot, amelynek kiinduló értéke igaz! Hozzon létre egy search nevű állapotot, amely egy objektumot tartalmaz title és genre kulccsal (üres szövegek) és premiereOnly kulccsal (hamis)!

```js
const movies = ref([])
const loading = ref(true)
const search = ref({ title: '', genre: '', premiereOnly: false })
```

### 29.
> Készítsen egy getMovies függvényt, amely lekéri a /api/movies végpontról a filmeket és eltárolja a movies állapotban! A kérés sikeres végrehajtása után a loading értéke legyen hamis! Az oldal betöltődésekor fusson le a függvény!

**What to do:**
- `/api` is **not** in the base URL of this base, so write it yourself.
- `response.data.data`: axios `data`, then Laravel's `data`.
- "sikeres végrehajtása után" means `loading.value = false` right after the assignment, inside `try`.

```js
const getMovies = async () => {
  try {
    const response = await api.get('/api/movies')
    movies.value = response.data.data
    loading.value = false
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => {
  getMovies()
})
```

### 30.
> Készítsen egy filteredMovies nevű számított állapotot, amely a search állapot alapján szűri a filmeket!
> - Csak azok jelenjenek meg, amelyek címe bárhol tartalmazza a keresett szöveget (kis- és nagybetű nem számít)!
> - Ha a műfaj szűrő nem üres, csak az adott műfajú filmek jelenjenek meg!
> - Ha a premiereOnly igaz, csak a premier filmek jelenjenek meg!

**What to do:** one condition per bullet, all joined with AND. In the script it's `search.value.title`, because `search` is a `ref`.

```js
const filteredMovies = computed(() =>
  movies.value.filter((movie) => {
    const matchesTitle = movie.title.toLowerCase().includes(search.value.title.toLowerCase())
    const matchesGenre = search.value.genre === '' || movie.genre === search.value.genre
    const matchesPremiere = !search.value.premiereOnly || movie.is_premiere
    return matchesTitle && matchesGenre && matchesPremiere
  })
)
```

### 31.
> Helyezze el az alcím alá a SearchForm komponenst! Kösse rá v-model-lel a search állapotot, adja át a genres objektumot, kapjon 8 egység alsó margót! A reset eseményre fusson le egy resetSearch nevű függvény, amely a search állapotot visszaállítja a kiinduló értékére!

**What to do:**
- `v-model="search"` on the component
- `:genres="genres"`, with `genres` imported from `helper.js`
- `class="mb-8"` lands on the form (fallthrough)
- `@reset` listens to the emit

```js
const resetSearch = () => {
  search.value = { title: '', genre: '', premiereOnly: false }
}
```

```vue
<SearchForm v-model="search" :genres="genres" class="mb-8" @reset="resetSearch" />
```

### 32.
> Amíg az adatok töltődnek, egy „Filmek betöltése...” szövegű bekezdés jelenjen meg! Ha betöltődtek, de nincs találat, a „Nincs a keresésnek megfelelő film.” szöveg jelenjen meg középre igazítva! Egyébként egy grid elrendezésű div jelenjen meg, alapértelmezetten 1, közepes eszköztől 3, extra nagy eszköztől 4 oszloppal, 6 egység távolsággal!

**What to do:** the `v-if` / `v-else-if` / `v-else` chain (4.9). Breakpoints: plain class → `md:` → `xl:`.

```vue
<p v-if="loading">Filmek betöltése...</p>
<p v-else-if="filteredMovies.length === 0" class="text-center">Nincs a keresésnek megfelelő film.</p>
<div v-else class="grid grid-cols-1 md:grid-cols-3 xl:grid-cols-4 gap-6">
  ...
</div>
```

### 33.
> A grid elembe a filteredMovies minden eleméhez generáljon egy MovieCard komponenst! Ügyeljen a kulcsokra! A premier filmek kártyája kapja meg a ring-2 ring-primary osztályokat!

**What to do:** `v-for` + `:key`, and a conditional class with the object syntax. It lands on the card's root `div`.

```vue
<MovieCard
  v-for="movie in filteredMovies"
  :key="movie.id"
  :movie="movie"
  :class="{ 'ring-2 ring-primary': movie.is_premiere }"
/>
```

**Whole file `pages/index.vue`:**

```vue
<script setup>
import { ref, computed, onMounted } from 'vue'
import { api } from '@utils/http.mjs'
import { genres } from '@utils/helper.js'
import Navbar from '@components/Navbar.vue'
import SearchForm from '@components/SearchForm.vue'
import MovieCard from '@components/MovieCard.vue'

const movies = ref([])
const loading = ref(true)
const search = ref({ title: '', genre: '', premiereOnly: false })

const getMovies = async () => {
  try {
    const response = await api.get('/api/movies')
    movies.value = response.data.data
    loading.value = false
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => {
  getMovies()
})

const filteredMovies = computed(() =>
  movies.value.filter((movie) => {
    const matchesTitle = movie.title.toLowerCase().includes(search.value.title.toLowerCase())
    const matchesGenre = search.value.genre === '' || movie.genre === search.value.genre
    const matchesPremiere = !search.value.premiereOnly || movie.is_premiere
    return matchesTitle && matchesGenre && matchesPremiere
  })
)

const resetSearch = () => {
  search.value = { title: '', genre: '', premiereOnly: false }
}
</script>

<template>
  <Navbar />
  <main class="max-w-6xl mx-auto px-4 py-8">
    <h1 class="screen-reader-text">Kertmozi</h1>
    <h2 class="text-3xl font-bold mb-6">Műsoron</h2>

    <SearchForm v-model="search" :genres="genres" class="mb-8" @reset="resetSearch" />

    <p v-if="loading">Filmek betöltése...</p>
    <p v-else-if="filteredMovies.length === 0" class="text-center">Nincs a keresésnek megfelelő film.</p>
    <div v-else class="grid grid-cols-1 md:grid-cols-3 xl:grid-cols-4 gap-6">
      <MovieCard
        v-for="movie in filteredMovies"
        :key="movie.id"
        :movie="movie"
        :class="{ 'ring-2 ring-primary': movie.is_premiere }"
      />
    </div>
  </main>
</template>

<route lang="yaml">
name: index
meta:
  title: Műsor
</route>
```

### A movies.show oldal (`pages/movies/[id].vue`)

### 34.
> Hozzon létre egy movie állapotot (kiinduló értéke null), egy loading állapotot (kiinduló értéke igaz) és egy selectedTicket állapotot, amelynek kiinduló értéke -1 (nincs kiválasztott jegy)!

```js
const movie = ref(null)
const loading = ref(true)
const selectedTicket = ref(-1)
```

### 35.
> Készítsen egy getMovie függvényt, amely a /api/movies/{id} végpontról lekéri a film adatait és a movie állapotban tárolja! Az azonosítót az útvonalból nyerje ki! Sikeres kérés után a loading legyen hamis! Az oldal betöltődésekor fusson le!

**What to do:** `useRoute()` gives `route.params.id`. Pass the id in as a parameter, so `watch` (next task) can reuse the function.

```js
const route = useRoute()

const getMovie = async (id) => {
  loading.value = true
  selectedTicket.value = -1
  try {
    const response = await api.get(`/api/movies/${id}`)
    movie.value = response.data.data
    loading.value = false
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => {
  getMovie(route.params.id)
})
```

### 36.
> Állítsa be, hogy ha megváltozik az útvonalban az azonosító, az adatok töltődjenek újra!

**What to do:** going from `/movies/2` to `/movies/3` reuses the same page component, so `onMounted` does **not** run again. `watch` catches the change.

```js
watch(() => route.params.id, (newId) => {
  if (newId) getMovie(newId)
})
```

### 37.
> Amíg tölt az oldal, egy „Film betöltése...” bekezdés jelenjen meg! Az első div csak akkor legyen látható, ha az oldal betöltődött!

**What to do:** use `v-if`, **not** `v-show`. While `movie` is `null`, `movie.title` would crash.

```vue
<p v-if="loading">Film betöltése...</p>
<div v-if="!loading" class="grid md:grid-cols-3 gap-8">
  ...
</div>
```

### 38.
> Készítsen két számított állapotot!
> - normalPrice: a kedvezménnyel csökkentett jegyár egészre kerekítve (ha a discount null, akkor 0-nak számítson)!
> - ageText: ha az age_limit 0, akkor „Korhatár nélkül”, különben például „16+”!

**What to do:** these computeds only run inside the `v-if="!loading"` div, so `movie.value` already exists by then.

```js
const normalPrice = computed(() => Math.round(movie.value.price * (1 - (movie.value.discount ?? 0))))
const ageText = computed(() => (movie.value.age_limit === 0 ? 'Korhatár nélkül' : `${movie.value.age_limit}+`))
```

### 39.
> A div-ben jelenjen meg: a film plakátja teljes szélességben (alternatív szöveg a cím); a film címe 4XL méretű félkövér főcímben; egy Badge a műfaj magyar nevével; a film hossza formatDuration-nel; az ageText félkövéren; a film leírása egy bekezdésben!

```vue
<img :src="`/images/${movie.image}`" :alt="movie.title" class="w-full ..." />
<h1 class="text-4xl font-bold">{{ movie.title }}</h1>
<Badge :color="movie.genre">{{ genres[movie.genre] }}</Badge>
<span>{{ formatDuration(movie.duration) }}</span>
<span class="font-bold">{{ ageText }}</span>
<p>{{ movie.description }}</p>
```

### 40.
> Ha a filmre van kedvezmény, jelenjen meg egy bekezdés, benne az eredeti ár áthúzva (s elem), mellette egy Badge-ben a kedvezmény százalékos értéke (pl. „-20%”)!

```vue
<p v-if="movie.discount">
  <s>{{ movie.price }} Ft</s>
  <Badge class="ml-2">-{{ Math.round(movie.discount * 100) }}%</Badge>
</p>
```

(`v-if="movie.discount"` is false for both `null` and `0`.)

### 41.
> Egy „Vetítések” alcím alatt listázza ki (ul/li) a film vetítéseit „18:00 – A terem” formában! Ha nincs vetítés, a „Jelenleg nincs vetítés.” szöveg jelenjen meg!

```vue
<h2 class="text-2xl font-bold">Vetítések</h2>
<ul v-if="movie.screenings.length > 0" class="list-disc ml-6">
  <li v-for="screening in movie.screenings" :key="screening.id">{{ screening.time }} – {{ screening.hall }} terem</li>
</ul>
<p v-else>Jelenleg nincs vetítés.</p>
```

### 42.
> Helyezzen el egy „Jegyek” alcímet screen-reader-text osztállyal, alatta egy grid elrendezésű (közepes eszköztől 3 oszlopos, 4 egység távolságú) div-et, benne TicketCard komponensekkel az alábbi táblázat alapján! A körvonal feltétele határozza meg, mikor kapja meg a kártya az outline-2 outline-primary osztályokat!
>
> | title | price | Megjelenés feltétele | Körvonal feltétele | selected eseménykor |
> |---|---|---|---|---|
> | Normál | normalPrice | mindig | selectedTicket === 0 | selectedTicket = 0 |
> | Diák | normalPrice 80%-a, egészre kerekítve | mindig | selectedTicket === 1 | selectedTicket = 1 |
> | VIP | movie.vip_price | ha a vip_price nem null | selectedTicket === 2 | selectedTicket = 2 |

**What to do:** this is the ÖBB TicketCard task again. `!= null` (two `=`) catches both `null` and `undefined`.

```vue
<h2 class="screen-reader-text">Jegyek</h2>
<div class="grid md:grid-cols-3 gap-4">
  <TicketCard
    title="Normál"
    :price="normalPrice"
    :class="{ 'outline-2 outline-primary': selectedTicket === 0 }"
    @selected="selectedTicket = 0"
  />
  <TicketCard
    title="Diák"
    :price="Math.round(normalPrice * 0.8)"
    :class="{ 'outline-2 outline-primary': selectedTicket === 1 }"
    @selected="selectedTicket = 1"
  />
  <TicketCard
    v-if="movie.vip_price != null"
    title="VIP"
    :price="movie.vip_price"
    :class="{ 'outline-2 outline-primary': selectedTicket === 2 }"
    @selected="selectedTicket = 2"
  />
</div>
```

### 43.
> Ha van kiválasztott jegy, egy félkövér bekezdésben jelenjen meg: „Kiválasztott jegy: Diák” (a jegy neve)! A jegyek nevét egy ticketNames nevű tömbben tárolja!

```js
const ticketNames = ['Normál', 'Diák', 'VIP']
```

```vue
<p v-if="selectedTicket !== -1" class="font-bold">Kiválasztott jegy: {{ ticketNames[selectedTicket] }}</p>
```

### 44.
> Az oldal alján legyen egy „Vissza a műsorhoz” link a főoldalra, és egy „Következő film” link, amely a következő azonosítójú film oldalára mutat!

**What to do:** the ids are numbers in the Laravel API, so `movie.id + 1` works. If they were strings (JSON server), you'd need `Number(movie.id) + 1`. Clicking "Következő film" is what tests the `watch` from task 36.

```vue
<RouterLink :to="{ name: 'index' }" class="underline">Vissza a műsorhoz</RouterLink>
<RouterLink :to="{ name: 'movies.show', params: { id: movie.id + 1 } }" class="underline">Következő film</RouterLink>
```

**Whole file `pages/movies/[id].vue`:**

```vue
<script setup>
import { ref, computed, onMounted, watch } from 'vue'
import { useRoute, RouterLink } from 'vue-router'
import { api } from '@utils/http.mjs'
import { genres, formatDuration } from '@utils/helper.js'
import Navbar from '@components/Navbar.vue'
import Badge from '@components/Badge.vue'
import TicketCard from '@components/TicketCard.vue'

const route = useRoute()
const movie = ref(null)
const loading = ref(true)
const selectedTicket = ref(-1)
const ticketNames = ['Normál', 'Diák', 'VIP']

const getMovie = async (id) => {
  loading.value = true
  selectedTicket.value = -1
  try {
    const response = await api.get(`/api/movies/${id}`)
    movie.value = response.data.data
    loading.value = false
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => {
  getMovie(route.params.id)
})

watch(() => route.params.id, (newId) => {
  if (newId) getMovie(newId)
})

const normalPrice = computed(() => Math.round(movie.value.price * (1 - (movie.value.discount ?? 0))))
const ageText = computed(() => (movie.value.age_limit === 0 ? 'Korhatár nélkül' : `${movie.value.age_limit}+`))
</script>

<template>
  <Navbar />
  <main class="max-w-5xl mx-auto px-4 py-8">
    <p v-if="loading">Film betöltése...</p>
    <div v-if="!loading" class="grid md:grid-cols-3 gap-8">
      <img :src="`/images/${movie.image}`" :alt="movie.title" class="w-full aspect-[2/3] object-cover rounded-xl bg-gray-200" />
      <div class="md:col-span-2 flex flex-col gap-4">
        <h1 class="text-4xl font-bold">{{ movie.title }}</h1>
        <div class="flex items-center gap-3">
          <Badge :color="movie.genre">{{ genres[movie.genre] }}</Badge>
          <span>{{ formatDuration(movie.duration) }}</span>
          <span class="font-bold">{{ ageText }}</span>
        </div>
        <p>{{ movie.description }}</p>
        <p v-if="movie.discount">
          <s>{{ movie.price }} Ft</s>
          <Badge class="ml-2">-{{ Math.round(movie.discount * 100) }}%</Badge>
        </p>

        <h2 class="text-2xl font-bold">Vetítések</h2>
        <ul v-if="movie.screenings.length > 0" class="list-disc ml-6">
          <li v-for="screening in movie.screenings" :key="screening.id">{{ screening.time }} – {{ screening.hall }} terem</li>
        </ul>
        <p v-else>Jelenleg nincs vetítés.</p>

        <h2 class="screen-reader-text">Jegyek</h2>
        <div class="grid md:grid-cols-3 gap-4">
          <TicketCard
            title="Normál"
            :price="normalPrice"
            :class="{ 'outline-2 outline-primary': selectedTicket === 0 }"
            @selected="selectedTicket = 0"
          />
          <TicketCard
            title="Diák"
            :price="Math.round(normalPrice * 0.8)"
            :class="{ 'outline-2 outline-primary': selectedTicket === 1 }"
            @selected="selectedTicket = 1"
          />
          <TicketCard
            v-if="movie.vip_price != null"
            title="VIP"
            :price="movie.vip_price"
            :class="{ 'outline-2 outline-primary': selectedTicket === 2 }"
            @selected="selectedTicket = 2"
          />
        </div>
        <p v-if="selectedTicket !== -1" class="font-bold">Kiválasztott jegy: {{ ticketNames[selectedTicket] }}</p>

        <div class="flex gap-4">
          <RouterLink :to="{ name: 'index' }" class="underline">Vissza a műsorhoz</RouterLink>
          <RouterLink :to="{ name: 'movies.show', params: { id: movie.id + 1 } }" class="underline">Következő film</RouterLink>
        </div>
      </div>
    </div>
  </main>
</template>

<route lang="yaml">
name: movies.show
meta:
  title: Film
</route>
```

---

## Check yourself (what the browser check confirmed on the solution)

Open `http://frontend.vm1.test` in the VM's browser and go through this list. Every line was checked on the solution in `~/Projects/gyakorlas-kertmozi`.

- [ ] Tab title "Műsor | Kertmozi", `<html lang="hu">`, red primary color.
- [ ] 6 cards, the 3 premiere films have a red ring, the first badge is "Dráma", and the first duration is "2 óra 11 perc".
- [ ] Prices with discount: 2400, **1600**, **1620**, 2600, 1500, **1650** Ft.
- [ ] Typing "KERT" → 1 card (case doesn't matter). Genre "Vígjáték" → 2. Plus "Csak premierek" → 1. Thriller + premiere → "Nincs a keresésnek megfelelő film."
- [ ] "Szűrők törlése" → all 6 are back, the input is empty and the checkbox unchecked.
- [ ] Phone width: the menu is collapsed, ☰ opens it.
- [ ] `/movies/2`: title "Film | Kertmozi", "Korhatár nélkül", ~~2000 Ft~~ -20%, tickets Normál 1600 / Diák 1280, no VIP.
- [ ] Clicking "Kiválasztom" on Diák → red outline + "Kiválasztott jegy: Diák".
- [ ] "Következő film" → `/movies/3` loads (that's `watch`), and the selection is cleared.
- [ ] `/movies/1` has a VIP ticket and no strikethrough. `/movies/5` shows "Jelenleg nincs vetítés.".
- [ ] F12 → Console: no red errors, no yellow warnings.
