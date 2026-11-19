# Frontend test guide (Vue 3 + Tailwind 4 + vue-router)

Built from your Fullstack projects:

- **ÖBB** is the test the whole class failed and now has to rewrite. It's the main worked example (section 7), because it covers the most Vue concepts.
- **CourseHUB, ASDA and Kaufland** are the newer frontend tasks, and they all have the same shape: small slot components, a price or class component, a card, a list page with search, and a detail page. That shape is section 6. Expect the retake to be ÖBB-sized with these patterns mixed in.
- **Pufi Pékség** and the QueryBuilder folders are backend only, so they add nothing here.

👉 **Practice exam with a full walkthrough:** [PRACTICE-TASK-KERTMOZI.md](PRACTICE-TASK-KERTMOZI.md). It has 44 tasks in PDF-style Hungarian, each with what to do and the code. The solution was tested in the VM.

Assumption: the teacher gives you a running backend, a PDF with numbered subtasks, and a `sources` folder (HTML templates, ready-made components, images). **Every numbered subtask is worth points on its own.** The goal is to collect as many of them as possible, not to build one perfect page.

---

## 0. The school base: `rcsnjszg/fullstack2627` (tested in your VM)

At school you put your work into this base: <https://github.com/rcsnjszg/fullstack2627>. Its frontend is **identical** to your CourseHUB base (same `package.json`, `vite.config.js`, router, `http.mjs`, justfile). Only `APP_NAME=Fullstack` and one placeholder menu link differ. So everything in this guide applies to it as is.

What I checked in your VM on 2026-11-19, in a fresh clone at `~/Projects/gyakorlas-fullstack2627`:

| Fact | Consequence |
|---|---|
| Router imports `vue-router/auto-routes` | **File-based routing**: `pages/index.vue`, `pages/products/[id].vue`, plus a `<route lang="yaml">` block for `name` and `meta.title` |
| `http.mjs`: `baseURL: VITE_BACKEND_URL`, and `.env`: `VITE_BACKEND_URL=http://${DOMAIN_BACKEND}` | `/api` is **not** in the base URL, so write `api.get('/api/products')` |
| Tab title = `meta.title + ' \| ' + VITE_APP_NAME` | `VITE_APP_NAME` comes from `APP_NAME` in the root `.env` (default `Fullstack`). If the PDF names the app ("ÖBB"), change `APP_NAME` in **both** `.env` and `.env.example`, then restart the frontend. |
| The Laravel backend sends `Access-Control-Allow-Origin: *` on `/api/*` | The frontend at `frontend.vm1.test` can call `backend.vm1.test/api/...` from the browser ✔ |
| The **JSON server sends no CORS headers** | A page on `frontend.vm1.test` **cannot** read `jsonserver.vm1.test` in the browser. curl works, the page stays on "loading". In the test you use the Laravel backend. |
| The JSON server wraps answers in `data` too (`config.json`: `"wrapper": true`) | `response.data.data` is right for both servers |
| `BaseLayout` = `BaseHeader` + `<main class="container mx-auto"><slot /></main>` | Wrap each page in `<BaseLayout>` unless the PDF wants its own Navbar (ÖBB) |
| `BaseHeader` has a `links` array (`{ label, to }`) | Menu tasks go there (CourseHUB task 45) |
| `docker compose exec frontend pnpm vitest run` | Runs the 4 sample tests: passed ✔ |
| In the VM, `just` is in `~/bin` | It works in a normal terminal (`.profile` adds `~/bin` to PATH) |

**Verified working practice app in the VM.** `~/Projects/gyakorlas-fullstack2627` is a Kaufland-style app built only with the patterns in sections 4 and 6, running on the base with the Laravel backend:

- `Badge` (lookup computed), `Price` (formula), `Input` (`defineModel`), `OfferCard` (`defineEmits`) and `ProductCard` (computed `categoryText`)
- `pages/index.vue` (fetch, search, select from an object, filter, empty text)
- `pages/products/[id].vue` (route param, loading, `v-if`s, emit + outline class, `watch` on the id)

A headless Chrome check in the VM confirmed all of it: typing filters the list, the select filters, the empty text appears, clicking Choose outlines the card, "Next product" reloads through `watch`, and the titles are right. **No console errors or warnings.** Open it in the VM's browser at `http://frontend.vm1.test` and compare it with your own code.

To start it again after a VM restart:

```bash
cd ~/Projects/gyakorlas-fullstack2627
docker compose up -d frontend backend webserver db proxy
```

### Starting from the base at school

```bash
git clone https://github.com/rcsnjszg/fullstack2627 vezeteknev-keresztnev-feladat
cd vezeteknev-keresztnev-feladat
just init          # creates .env, installs frontend packages, starts everything, migrates
```

Then open `http://frontend.vm1.test` in the VM's browser. (`frontend.vm1.test` is only in the VM's hosts file, not in Windows'.)

---

## 1. Rules that protect your points

1. **Use the exact names from the PDF.** States, functions, components, props, events and route names (`heroContents`, `getHeroContents()`, `filteredOrigins`, `selected`, `trains.show`...). Graders and tests look for those names. `heroContent` instead of `heroContents` can cost the point.
2. **Follow the hints literally**, even if your own way also works. If the PDF says `toISOString()`, `toLocaleTimeString()`, `slice()` or "replace `/` with `%2F`", use exactly that.
   - Example from your ÖBB: the hint said `toLocaleTimeString(...)` and you used `toTimeString()`. It works, but it is not what they asked for.
   - Example from your Kaufland: the PDF asked for a **computed state named `categoryText`** in ProductCard. Your card did `categories[product.category]` inline in the template. It displays the same thing, but there is no `categoryText`, so that point is gone. **"Készítsen egy számított állapotot X néven" always means a `const X = computed(...)` must exist.**
3. **Never leave a broken file.** One syntax error in a `.vue` file shows a blank page, and you lose every point on that page. If something is unfinished, comment it out (`<!-- -->` in the template, `//` in the script) before moving on.
4. **When you convert a given HTML template into a component, keep every class.** Only add bindings (`:src`, `v-for`, `{{ }}`, `@click`). Don't "clean up" their markup.
5. **Don't get stuck.** If one subtask takes more than about 8 minutes, leave a `// TODO 54b` comment and go on. Later subtasks are often easier, and they don't depend on the one you skipped.
6. **Partial credit exists.** A component with correct props but imperfect styling still earns the props point. Write the skeleton (props, state, function) even if you can't finish the looks.

---

## 2. Test-day playbook (in time order)

### First ~10 minutes: no coding yet

- [ ] Read the **whole** PDF once. Mark every name you must create: components, states, functions, routes, props, events.
- [ ] Open `http://swagger.vm1.test` and call the real endpoints in the browser (for example `http://backend.vm1.test/api/courses`). Check two things:
  - **Is the data wrapped in `data`?** Usually yes, so you read `response.data.data` (axios's `.data`, then Laravel's `data` key).
  - **What does the image field contain?** In CourseHUB the API returned a **full URL** (`Storage::url(...)`), so `:src="course.image"` was enough. In ÖBB it was **only a file name**, so you needed `'/images/' + image`.
- [ ] Open the frontend in the browser, then DevTools (F12) → **Network**. The requests should go to the Laravel backend (`backend.vm1.test`). The root `.env` is mounted into the frontend container, and its `VITE_BACKEND_URL` decides where `api` talks to. Don't "fix" a setup that already returns data. If you do change `.env`, restart the frontend container.
  - Answers are wrapped: `response.data.data`. In this base the JSON server wraps them too, but it has no CORS, so the browser can't use it (section 0).
  - Laravel ids are numbers. JSON-server ids are strings (`"1"`).
  - So **open one endpoint and look at the response before writing any fetch.**
- [ ] **Check whether `/api` is already in the base URL.** Look at `VITE_BACKEND_URL` in the root `.env` **and** at `baseURL` in `src/utils/http.mjs`. In your projects it varied:

  | Project | Where `/api` was | So you wrote |
  |---|---|---|
  | ÖBB, CourseHUB | nowhere | `api.get('/api/courses')` |
  | ASDA | end of `VITE_BACKEND_URL` (`.../api`) | `api.get('/items')` |
  | Kaufland | in `http.mjs` (`VITE_BACKEND_URL + '/api'`) | `api.get('/products')` |

  If you get it wrong, the request goes to `/api/api/items` and returns 404. Check the Network tab after your first fetch.
- [ ] If there are frontend tests (`frontend/tests/*.spec.js`), **read them first**. They show exactly which names and texts are checked.
- [ ] Bookmark the local docs, which may be your only references during the test: `vue.vm1.test`, `vue-router.vm1.test`, `tailwind.vm1.test`, `mdn.vm1.test`.

### Routing check (2 minutes, decides how you make pages)

Open `frontend/src/router/index.js`:

- **If it imports `vue-router/auto-routes`** (the base project does): routing is **file-based**. A file in `src/pages/` is a route:

  | File | URL |
  |---|---|
  | `pages/index.vue` | `/` |
  | `pages/timetable.vue` | `/timetable` |
  | `pages/trains/[id].vue` | `/trains/:id` |
  | `pages/courses/[id].vue` | `/courses/:id` |

  Set the route name and page title with a block at the **bottom** of the page file:

  ```vue
  <route lang="yaml">
  name: courses.show
  meta:
    title: Termék
  </route>
  ```

  `SetTitleGuard` builds the tab title from `meta.title`. If you see `undefined | ...` in the tab, `meta.title` is missing.

- **If you prefer manual routes** (what you did in ÖBB), replace the import with your own array. Only do this if it's allowed or nothing is said about it:

  ```js
  export const routes = [
    { path: '/', name: 'home', component: () => import('@/pages/index.vue'), meta: { title: 'Startseite' } },
    { path: '/trains/:id', name: 'trains.show', component: () => import('@/pages/trains.vue'), meta: { title: 'Reisedetails' } }
  ]
  ```

  ⚠️ Don't mix the two. In ÖBB, `pages/trains.vue` only worked because the routes were manual. With file-based routing it has to be `pages/trains/[id].vue`.

### Cheap setup points: do these right away (1–2 minutes each)

- [ ] Page language: `<html lang="de">` (or `hu`/`en`) in `frontend/index.html`.
- [ ] Colors in `src/assets/main.css` under `:root`: `--primary: #e2002a; --primary-foreground: #ffffff;`
- [ ] Copy images to `frontend/public/images/` (then reference them as `/images/x.png`, without `public`).
- [ ] Copy the ready-made components from `sources` into `src/components/` (and `icons/`).
- [ ] Add CSS snippets the PDF gives you to `main.css` (for example `screen-reader-text` in `@layer utilities`).
- [ ] Route names and titles (see above).
- [ ] Helper file if asked (`src/utils/helper.js`, see section 5).

### Then the main work, in this order

1. **Small "leaf" components** that use no other component: Button, Bubble, Box, Input, Card. They are short and independent, and each is worth several points.
2. **Components built from those:** TeaserCard, HeroItem, Hero, TeaserSection, Navbar, PlannerForm.
3. **Pages:** list page (fetch + `v-for`), then form or search page, then detail page (`useRoute`).
4. Mostly go in PDF order (it is already roughly in this order), but **skip and return** on anything stuck.

### Last ~10 minutes

- [ ] Click through **every route**. No page may be blank.
- [ ] Console (F12) must have **no red errors**. Also fix yellow warnings like `missing required prop` and `v-for ... key`. Those usually mean a lost point.
- [ ] Check the Network tab: each fetch returns 200, and the requested URLs and query strings match the PDF.
- [ ] Shrink the window to phone width and check the `md:` behavior (burger menu, column counts).
- [ ] Run the tests if there are any (see section 8).
- [ ] Make sure every file is saved and there are no leftover `console.log`s with typos that break things.

---

## 3. PDF wording → code (the dictionary)

| PDF says (HU) | Meaning | Write |
|---|---|---|
| **állapot** (`x` néven) | state | `const x = ref(...)` |
| alapértelmezett értéke legyen hamis/igaz | default false/true | `ref(false)` / `ref(true)` |
| objektumot tartalmazzon (állapot) | object state | `ref({ name: '', level: '' })` or `reactive({...})` |
| **számított állapot** | computed | `const x = computed(() => ...)` |
| **a szülőelemtől kapja meg / vegyen át** | prop | `defineProps({ ... })` |
| **kötelező / kötelezően megadandó** | required prop | `required: true` |
| **opcionális / nem kötelező** | optional prop | `required: false` |
| alapértelmezett értéke legyen X | default value | `default: 'X'` |
| szöveg / szám / tömb / objektum / logikai típusú | prop type | `String` / `Number` / `Array` / `Object` / `Boolean` |
| **a tartalmát a szülőelem adja meg / slot** | slot | `<slot />` |
| **emit-elje az X eseményt** | emit an event | `const emit = defineEmits(['x'])` + `emit('x')` |
| **kapjon v-model-t** | component v-model | `const model = defineModel()` |
| **két irányban összekötve** | two-way binding | `v-model="search.name"` |
| **az oldal betöltődésekor** fusson le | on page load | `onMounted(() => fn())` |
| ha megváltozik az id az útvonalban | route param changes | `watch(() => route.params.id, ...)` |
| **kérje le a /api/... végpontról** | fetch | `const r = await api.get('/api/...')` |
| query paraméterben | query string | `?x=..` or `api.get(url, { params: { x } })` |
| útvonalból nyerje ki | route param | `useRoute().params.id` |
| **generáljon / mindegyikhez egy-egy** | loop | `v-for="item in items" :key="item.id"` |
| ügyeljen a kulcsokra / beazonosítható | key | `:key="item.id"` |
| **csak akkor jelenjen meg, ha** | conditional | `v-if` / `v-else-if` / `v-else` |
| csak akkor legyen látható, ha... | conditional | **`v-if` as well.** `v-show` still renders the children, so `course.name` crashes while `course` is `null`. Only use `v-show` if the PDF names it. |
| ha a felhasználó rákattint | click | `@click="..."` |
| ha a mousedown / focusout esemény megtörténik | other events | `@mousedown="..."` / `@focusout="..."` |
| az űrlap elküldésére, prevent modifier | submit | `@submit.prevent="handleSubmit"` |
| linkké alakítsa / irányítson át X oldalra | router link | `<RouterLink :to="{ name: 'x' }">` |
| attribútumként kapja meg | bind attribute | `:type="type"`, `:src="..."` |
| alternatív szöveg | alt | `:alt="course.name"` |
| főcím / alcím / 3-as szintű címsor | heading | `h1` / `h2` / `h3` |
| bekezdés / felsorolás / listaelem | paragraph / list | `p` / `ul` / `li` |
| nagy kezdőbetűs | capitalize first letter | `ucfirst(x)` (helper) |
| két tizedesre kerekítve | 2 decimals | `Number(x).toFixed(2)` |
| kis- és nagybetű ne számítson | case-insensitive | `.toLowerCase().includes(...)` |
| bárhol a stringben tartalmazza | substring | `.includes(...)` |
| ...-val kezdődik | starts with | `.startsWith(...)` |
| **a táblázat alapján** adja vissza a színt / nevet | lookup table | an object `{ Christmas: '...', Halloween: '...' }` + `obj[key] ?? ''` (see 4.19) |
| tárolja el egy objektumban | object constant | `const categories = { 'soda syrup': 'Sirup do nápojov', ... }` |
| statikus tömbként | plain array, no `ref` | `const events = ['Tickled Pink', 'Halloween', 'Christmas']` |
| az elem értéke angol, szövege szlovák | option from an object | `v-for="(label, key) in categories" :key="key" :value="key"` → `{{ label }}` (see 4.20) |
| áthúzva / `<s>` elemben | strikethrough | `<s>£ {{ price }}</s>` or `class="line-through"` |
| akció százalékos értéke | percent | `-{{ Math.round(discount * 100) }}%` |
| képlet: price - (price * discount) | computed with formula | `computed(() => (price - price * discount).toFixed(2))` (see 4.21) |
| euróban (€) / £ (másolható) | currency text | `{{ x }} €` / `£ {{ x }}`. Copy the symbol from the PDF. |
| **kattintásra irányítsa át** az index oldalra (existing element) | wrap in a link | replace the wrapping `div` with `<RouterLink :to="{ name: 'index' }" class="...same classes...">` |

### Tailwind wording

| PDF says | Tailwind |
|---|---|
| **N egység** (margin, padding, gap, width) | the number directly: `p-4`, `mt-12`, `gap-4`, `max-w-340` (1 unit = 0.25rem) |
| N **pixel** | arbitrary value: `mb-[5px]` |
| N % | `w-[95%]` |
| **belső margó** / belső kitöltés | padding: `p-`, `px-` (horizontal), `py-` (vertical), `pt-` (top) |
| **külső margó** | margin: `m-`, `mx-auto` (centered), `my-`, `mb-` |
| középre igazítva (block) | `mx-auto` |
| szöveg középre / balra | `text-center` / `text-left` |
| **alapértelmezetten** / kis eszközökön X, közepes eszköztől Y | `X md:Y` (mobile first! the plain class is the smallest screen) |
| **kis méretű eszköztől** (felfelé) | `sm:` (≥640px). ⚠️ Not the same as "kis eszközökön", which is the plain class! |
| közepes / nagy / extra nagy eszköztől | `md:` (≥768) / `lg:` (≥1024) / `xl:` (≥1280) |
| e.g. kistől 3, közepestől 4, nagytól 6 oszlop | `grid gap-4 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6` |
| függőlegesen / vízszintesen N egység margó | `my-N` / `mx-N` (padding: `py-N` / `px-N`) |
| 0.75rem / 0.25rem | 1 unit = 0.25rem, so `px-3` / `py-1` |
| teljes szélességű | `w-full` |
| alapértelmezetten elrejtve, közepestől megjelenjen | `hidden md:block` |
| közepestől felfelé rejtse el | `md:hidden` |
| grid, 1 oszlop, közepestől 4 | `grid grid-cols-1 md:grid-cols-4` |
| tegye ki a rács teljes sorát | `col-span-full` |
| flex elrendezés / flex-grow 1 | `flex` / `grow` |
| félkövér / közepes vastagság | `font-bold` / `font-medium` |
| betűméret XS, SM, LG, 2XL... | `text-xs`, `text-sm`, `text-lg`, `text-2xl` |
| teljesen lekerekített / lekerekített sarkok | `rounded-full` / `rounded` |
| szegély színnel | `border border-gray-600` |
| háttérszín / szövegszín | `bg-orange-50` / `text-orange-800` |
| primary színű | `bg-primary`, `text-primary`, `outline-primary` (from the CSS variable) |
| ha ráviszi a kurzort | `hover:...` |
| ha fókuszba kerül | `focus:...` (e.g. `focus:outline focus:outline-primary`) |
| 50%-ban áttetsző gray-800 | `/50`: `text-shadow-gray-800/50` |
| túlcsorduló elemek elrejtése vízszintesen | `overflow-x-hidden` |
| sorközi elem | `inline` |
| elforgatva | `rotate-90`, `-rotate-90` |
| háttérkép, csak X tengelyen ismétlődjön | `bg-[url('/images/x.png')] bg-repeat-x` |

Unsure about a class? Search `tailwind.vm1.test`. The PDF often hands you the exact classes in a box. Then just **copy them**.

---

## 4. Copy-paste patterns (all from your working ÖBB, CourseHUB, ASDA and Kaufland code)

### 4.1 Props (required, optional, default)

```vue
<script setup>
// HeroItem: "image néven, szöveg típusú, kötelező"
defineProps({
  image: { type: String, required: true }
})

// Button: "opcionális stringek, type alapértelmezett submit, variant alapértelmezett primary"
const props = defineProps({
  type:    { type: String, required: false, default: 'submit' },
  variant: { type: String, required: false, default: 'primary' }
})
</script>
```

In the template you use the prop directly: `{{ image }}`. In the script you need `props.type`.

### 4.2 Slot (the parent gives the content)

```vue
<template>
  <span class="rounded-full px-3 py-1 text-xs font-medium bg-orange-50 text-orange-800">
    <slot />
  </span>
</template>
```

Parent: `<Bubble>{{ ucfirst(course.level) }}</Bubble>`

### 4.3 Classes from an object (Button variants)

```vue
<script setup>
const props = defineProps({
  type:    { type: String, default: 'submit' },
  variant: { type: String, default: 'primary' }
})

const classes = {
  primary: 'bg-primary text-white hover:bg-white hover:text-primary hover:outline outline-primary',
  empty:   'bg-white text-black'
}
</script>

<template>
  <button :type="type" :class="['px-4 py-3 cursor-pointer', classes[variant]]">
    <slot />
  </button>
</template>
```

### 4.4 Computed class (Box type, Navbar open/closed)

```js
// Box: always the base classes, extra by type
const boxClass = computed(() => {
  const base = 'px-6 py-6 text-base font-semibold rounded-sm w-full text-center'
  if (props.type === 'info')    return `${base} bg-sky-200 border border-blue-700 text-blue-700`
  if (props.type === 'warning') return `${base} bg-orange-200 border border-orange-700 text-orange-700`
  return base
})

// Navbar: toggle state + computed class
const open = ref(false)
const openClass = computed(() => open.value ? 'max-h-300' : 'max-h-12 md:max-h-auto')
```

```vue
<!-- "add it to the existing classes on the nav" -> array syntax keeps the static ones -->
<nav :class="['flex p-3 flex-col ... overflow-y-hidden', openClass]">
  <BurgerIcon class="md:hidden cursor-pointer" @click="open = !open" />
</nav>
```

Conditional single class (TicketCard outline): `:class="{ 'outline-2': selectedTicket === 0 }"`

### 4.5 Emitting events (Stepper, TicketCard)

```vue
<script setup>
const emit = defineEmits(['previous', 'next'])
</script>

<template>
  <StepperButton @click="emit('previous')">...</StepperButton>
  <slot />
  <StepperButton @click="emit('next')">...</StepperButton>
</template>
```

Parent listens: `<Stepper @previous="handlePrevious" @next="handleNext">` or `<TicketCard @selected="selectedTicket = 0" />`

### 4.6 v-model on your own component (`defineModel`)

Simple value (Input):

```vue
<script setup>
const model = defineModel()
</script>

<template>
  <input v-model="model" class="text-lg p-4 border border-gray-600 rounded focus:outline focus:outline-primary" />
</template>
```

Object (PlannerForm gets the whole `search` object):

```js
const model = defineModel({ type: Object, required: true })
// in the template: v-model="model.from", @mousedown="model.from = origin"
// in the script:   model.value.from
```

Parent: `<PlannerForm v-model="search" @submit.prevent="handleSubmit" />`

### 4.7 Fallthrough attributes (why `class`, `@submit` and `@focusout` work on components)

If a component has **one root element**, anything you put on the component that isn't a prop or emit goes onto that root element:

- `<TeaserCard class="md:col-span-3" />` puts the class on the Card's root `div`.
- `<PlannerForm @submit.prevent="handleSubmit" />` puts the listener on the `<form>` inside.
- `<Input required autocomplete="off" :min="today" @focusout="getDestinations(model.from)" />` puts all of it on the real `<input>`.

That's why many PDF tasks say "the component should get class X", and you just write `class="X"` on it.

### 4.8 Fetching data on page load

```js
import { ref, onMounted } from 'vue'
import { api } from '@utils/http.mjs'   // axios with baseURL = VITE_BACKEND_URL

const heroContents = ref([])

const getHeroContents = async () => {
  try {
    const response = await api.get('/api/hero-contents')
    heroContents.value = response.data.data   // axios .data + Laravel "data" key
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => {
  getHeroContents()
})
```

With a loading flag (CourseHUB: "az adatok **sikeres** betöltése után állítsa hamisra"):

```js
const courses = ref([])
const loading = ref(true)

async function loadCourses() {
  try {
    const response = await api.get('/api/courses')
    courses.value = response.data.data
    loading.value = false          // only after success, as the PDF words it
  } catch (error) {
    console.error(error)
  }
}
onMounted(loadCourses)
```

If the PDF only says "after loading", without "successful", you can put `loading.value = false` in a `finally { }` block instead. That also runs when the request fails.

Query string: `api.get('/api/teasers?section=new_from_obb')` or `api.get('/api/timetable/stations/destinations', { params: { origin } })`.

Path params containing `/`: `search.value.from.replaceAll('/', '%2F')` (do it if the PDF asks).

### 4.9 Loading / empty / list chain (very common, worth several points)

```vue
<p v-if="loading">Page loading...</p>
<Box v-else-if="filteredItems.length === 0" type="warning">No course found.</Box>
<div v-else class="courses grid grid-cols-1 md:grid-cols-4 gap-4">
  <CourseCard v-for="course in filteredItems" :key="course.id" :course="course" />
</div>
```

To group several elements under one `v-if` without an extra div, use `<template v-if="...">`.

### 4.10 Search form with a computed filter

```js
const levels = ['beginner', 'intermediate', 'advanced']   // static array, no ref needed
const search = reactive({ name: '', level: '' })

const filteredItems = computed(() =>
  courses.value.filter(course => {
    const matchesName  = course.name.toLowerCase().includes(search.name.toLowerCase().trim())
    const matchesLevel = !search.level || course.level === search.level
    return matchesName && matchesLevel
  })
)
```

```vue
<input id="name" v-model="search.name" ... />
<select id="level" v-model="search.level" ...>
  <option value="">All level</option>
  <option v-for="level in levels" :key="level" :value="level">{{ ucfirst(level) }}</option>
</select>
```

Note: `reactive` uses `search.name`, while `ref({...})` uses `search.value.name` in the script. Both are `search.name` in the template.

### 4.11 Autocomplete list (ÖBB PlannerForm)

```vue
<ul v-if="filteredOrigins.length > 0" class="...given classes...">
  <li v-for="origin in filteredOrigins" :key="origin" class="cursor-pointer" @mousedown="model.from = origin">
    {{ origin }}
  </li>
</ul>
```

`mousedown` instead of `click` matters: it fires **before** the input loses focus, so the list doesn't disappear first.

### 4.12 Detail page: route param, and reload when it changes

```js
import { useRoute } from 'vue-router'

const route = useRoute()
const course = ref(null)
const loading = ref(true)

async function loadCourse(id) {
  loading.value = true
  try {
    const response = await api.get(`/api/courses/${id}`)
    course.value = response.data.data
    loading.value = false          // "ha a kérés sikeresen végbement"
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => loadCourse(route.params.id))

// "ha megváltozik az id az útvonalban, töltse újra"
watch(() => route.params.id, (newId) => {
  if (newId) loadCourse(newId)
})
```

Template: wrap everything in `<div v-if="!loading">` (or `v-else-if="course"`), otherwise `course.name` crashes while `course` is still `null`.

Optional chaining helps too: `{{ course?.name }}`.

### 4.13 Links

```vue
<RouterLink :to="{ name: 'courses.show', params: { id: course.id } }">View course &rarr;</RouterLink>
<RouterLink :to="{ name: 'home' }"><img src="/images/oebb-logo.svg" alt="ÖBB" /></RouterLink>
<RouterLink to="/">Back to courses</RouterLink>
```

Menu array with objects you can pass to `RouterLink` (ÖBB Navbar):

```js
const navItems = [
  { title: 'Tickets & Ermäßigungen', page: { name: 'home' } },
  { title: 'Fahrplan',               page: { name: 'timetable.index' } }
]
```

```vue
<RouterLink v-for="(item, index) in navItems" :key="index" :to="item.page" class="text-center grow">
  {{ item.title }}
</RouterLink>
```

BaseHeader `links` (CourseHUB): `{ to: '/', label: 'Home' }, { to: '/courses/1', label: 'Laravel' }`

### 4.14 Images

```vue
<!-- file in public/images -->
<img src="/images/vehicles.png" alt="..." />
<img :src="'/images/' + image" :alt="alt" class="w-full" />

<!-- API already returns a full URL (CourseHUB) -->
<img :src="course.image" :alt="course.name" />

<!-- as a background via style (HeroItem: replace ??? in the template) -->
<div :style="`background: linear-gradient(...), center / cover url('/images/${image}')`">
```

### 4.15 Render only if the value exists, or if a prefix matches

```vue
<TicketCard v-if="train.first_class_price != null" title="1. Klasse" :price="train.first_class_price" />
```

`!= null` (two `=`) is true for both `null` and `undefined`, which is exactly what "nem null/undefined" means.

```js
const prefixes = ['RJ', 'RJX', 'NJ', 'EN', 'IC', 'IR', 'EC', 'ICE', 'D']
const canReserveSeat = computed(() =>
  prefixes.some(p => train.value?.train_number?.startsWith(p))
)
```

### 4.16 Splitting one list into two loops (`slice`)

```vue
<TeaserCard v-for="item in newFromObbTeasers.slice(0, 2)" :key="item.id" :item="item" class="md:col-span-3" />
<TeaserCard v-for="item in newFromObbTeasers.slice(2)"    :key="item.id" :item="item" class="md:col-span-2" />
```

### 4.17 Date and time defaults

```js
const today = new Date().toISOString().substring(0, 10)   // "2026-11-20" (yyyy-mm-dd)
const now   = new Date().toLocaleTimeString('hu-HU', { hour: '2-digit', minute: '2-digit' })   // "14:05"

const search = ref({ from: '', to: '', date: today, time: now })
```

`<Input type="date" :min="today" required />` gives you "kötelező, legalább a mai nap".

### 4.18 Animation or CSS that the PDF gives you

Paste it into `<style scoped>` in the component, then use it from a class:

```vue
<style scoped>
@keyframes hero-carousel { /* ...given keyframes... */ }
.hero-carousel-animate { animation: hero-carousel 40s infinite; }
</style>
```

Keep the `animation:` rule in the **same** `<style scoped>` block as the `@keyframes`. Vue renames scoped keyframes, so a Tailwind class like `animate-[hero-carousel_40s_infinite]` would not find them.

### 4.19 Class (or text) from a table: lookup object + computed (ASDA Badge, colorClasses, Kaufland categoryText)

Whenever the PDF gives a **table** "value → classes" or "value → name", turn the table into an object and look it up inside a `computed`:

```vue
<script setup>
import { computed } from 'vue'

const props = defineProps({
  color: { type: String, default: '' }     // "string, alapértelmezett üres string"
})

const colorClass = computed(() => {
  const colors = {
    Christmas: 'bg-green-800 text-white',
    Halloween: 'bg-black text-white',
    'Tickled Pink': 'bg-pink-300 text-pink-900'   // key with a space → quotes
  }
  return colors[props.color] ?? ''
})
</script>

<template>
  <!-- static classes + computed classes together -->
  <span class="badge p-1 rounded" :class="colorClass">
    <slot />
  </span>
</template>
```

The same on a detail page, where the data is still `null` at first:

```js
const colorClasses = computed(() => {
  const colors = { Christmas: 'text-red-600 border-red-600', Halloween: 'text-orange-600 border-orange-600' }
  return item.value ? (colors[item.value.event] ?? '') : ''
})
```

Kaufland's `categoryText` in ProductCard (the version that earns the point):

```js
const props = defineProps({ product: { type: Object, required: true } })

const categories = {
  'non-perishables': 'Trvanlivé potraviny',
  'soda syrup': 'Sirup do nápojov',
  chocolate: 'Čokoláda'
  // ...
}

const categoryText = computed(() => categories[props.product.category] ?? props.product.category)
```

Template: `<p class="mb-2">{{ categoryText }}</p>`

### 4.20 Select options from an object (value = English key, text = translated name)

```vue
<select id="category" v-model="search.category">
  <option value="">Všetky kategória</option>
  <option v-for="(label, key) in categories" :key="key" :value="key">{{ label }}</option>
</select>
```

`v-for` over an **object** gives `(value, key)`, in that order. Over an array it's just `v-for="event in events"`.

### 4.21 Price components (ASDA PriceTag, Kaufland Price)

Optional number prop with a `null` default, and the old price struck through only if there is a discount:

```vue
<script setup>
defineProps({
  price:          { type: Number, required: true },
  discount_price: { type: Number, default: null }
})
</script>

<template>
  <span class="text-xl">
    <template v-if="discount_price">
      <s>£ {{ price }}</s> £ {{ discount_price }}
    </template>
    <template v-else>£ {{ price }}</template>
  </span>
</template>
```

Parent: `<PriceTag :price="item.price" :discount_price="item.discount_price" />`. The prop name is exactly as the PDF writes it, snake_case included.

A price calculated by a formula:

```js
const props = defineProps({
  price:    { type: Number, required: true },
  discount: { type: Number, default: 0 }
})

const discountedPrice = computed(() => (props.price - props.price * (props.discount ?? 0)).toFixed(2))
```

`<span>{{ discountedPrice }} €</span>`

⚠️ If the API sends prices as strings (`"12.50"`), a `type: Number` prop gives a console warning. Then use `type: [Number, String]` and `Number(props.price)`.

On the detail page, "shown only if discounted, struck-through price + percent in a Badge":

```vue
<p v-if="product.discount">
  <span class="line-through">{{ product.price.toFixed(2) }} €</span>
  <Badge>-{{ Math.round(product.discount * 100) }}%</Badge>
</p>
<p v-if="product.free_shipping">Doprava zdarma</p>
```

### 4.22 Turning an existing element into a link (Kaufland Navbar)

"Ha a képre vagy a szövegre kattint, irányítsa át az index oldalra": swap the wrapping element for a `RouterLink`, and keep its classes:

```vue
<!-- before: <div class="flex gap-2"> ... </div> -->
<RouterLink :to="{ name: 'index' }" class="flex gap-2">
  <KauflandIcon class="fill-primary w-16" />
  <h1 class="...">Online<br>trhovisko</h1>
</RouterLink>
```

### 4.23 Tailwind inside `<style scoped>` (`@apply` needs `@reference`)

In Tailwind 4, `@apply` in a component's style block only works if you first point it at the main CSS:

```vue
<style scoped>
@reference "@assets/main.css";

.card {
  @apply border border-green-600 shadow-md flex flex-col items-center gap-2 p-4;
}
</style>
```

Without the `@reference` line you get a compile error, and the page goes blank. When the PDF says "give the element these classes", put them straight in `class="..."` instead. That's what gets checked.

### 4.24 The loading paragraph and the content div (detail pages)

All three newer PDFs word it the same way: "the paragraph is only shown while loading" and "the first div is only visible once loaded":

```vue
<p v-if="loading">Loading items...</p>
<div v-if="!loading" class="item grid md:grid-cols-2 gap-4 ..." :class="colorClasses">
  ...
</div>
```

Two separate `v-if`s (or `v-if` / `v-else`) both work. Keep the exact text from the PDF or template (`Loading items...`, `Načítava sa...`).

---

## 5. Helper functions (CourseHUB `utils/helper.js`)

```js
export function ucfirst(str) {
  if (!str) return ''
  return str.charAt(0).toUpperCase() + str.slice(1)
}

// 7860 -> "2h 11m", 2040 -> "34m"
export function formatDuration(seconds) {
  const hours = Math.floor(seconds / 3600)
  const minutes = Math.floor((seconds % 3600) / 60)
  return hours > 0 ? `${hours}h ${minutes}m` : `${minutes}m`
}
```

Import them where you use them: `import { ucfirst, formatDuration } from '@utils/helper.js'`. A function used in the template must be imported in that component's `<script setup>`.

---

## 6. The newer test shape (CourseHUB, ASDA, Kaufland): one skeleton, three tests

All three newer tasks are the **same test with different names**. Learn the skeleton once, and only the names change:

| Part | CourseHUB | ASDA | Kaufland |
|---|---|---|---|
| Slot-only component | Bubble | Badge (+ `color` prop, table → 4.19) | Badge |
| Class-from-prop component | Box (`type`: info/warning) | Badge (`color`) | – |
| Price | `toFixed(2)` in the card | PriceTag (`price`, `discount_price = null`, `<s>`) | Price (`price`, `discount = 0`, formula) |
| Card (`X` object prop, required) | CourseCard (`course`) | ItemCard (`item`) | ProductCard (`product`, + computed `categoryText`) |
| List state + fetch | `courses`, `loading`, `loadCourses` | `items`, `getItems` | `products`, `getProducts` |
| Static filter list | `levels` array | `events` array | `categories` **object** |
| Search object | `{ name, level }` | `{ name, event }` | `{ name, category }` |
| Filtered computed | `filteredItems` | `filteredItems` | `filteredProducts` |
| Grid columns | 1 → `md:4` | 1 → `md:3` → `xl:5`, gap 6 | `sm:3` → `md:4` → `lg:6` |
| Empty text | Box warning "No course found." | "No items found." | "Nenašli sa žiadne produkty" |
| Detail state + fetch | `course`, `loading`, `loadCourse` (+ `watch`) | `item`, `loading`, `colorClasses`, `getItems` | `product`, `loading`, `getProduct` |

### 6.1 List page skeleton (`pages/index.vue`)

Rename `products` / `getProducts` / `category` to whatever the PDF says:

```vue
<script setup>
import { ref, computed, onMounted } from 'vue'
import { api } from '@utils/http.mjs'
import BaseLayout from '@layouts/BaseLayout.vue'
import ProductCard from '@components/ProductCard.vue'

const products = ref([])                                   // "állapot products néven"
const categories = { cheese: 'Syr', coffee: 'Káva' }       // or a static array
const search = ref({ name: '', category: '' })             // "objektum name és category kulccsal"

const getProducts = async () => {                          // "függvény getProducts néven"
  const response = await api.get('/api/products')          // ⚠️ check the /api prefix (section 2)
  products.value = response.data.data
}

onMounted(() => {                                          // "betöltődésekor fusson le"
  getProducts()
})

const filteredProducts = computed(() =>
  products.value.filter(p => {
    const matchesName = p.name.toLowerCase().includes(search.value.name.toLowerCase())
    const matchesCategory = search.value.category === '' || p.category === search.value.category
    return matchesName && matchesCategory
  })
)
</script>

<template>
  <BaseLayout>
    <!-- paste the given forras/index.html here, keep its classes, then add: -->
    <h2 class="text-3xl font-bold my-4">...</h2>
    <div class="grid sm:grid-cols-2 gap-4">
      <input id="name" v-model="search.name" ... />
      <select id="category" v-model="search.category" ...>
        <option value="">...</option>
        <option v-for="(label, key) in categories" :key="key" :value="key">{{ label }}</option>
      </select>
    </div>
    <div class="grid gap-4 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6">
      <p v-if="filteredProducts.length === 0" class="col-span-full text-center">Nenašli sa žiadne produkty</p>
      <ProductCard v-for="product in filteredProducts" :key="product.id" :product="product" />
    </div>
  </BaseLayout>
</template>

<route lang="yaml">
name: index
meta:
  title: ...
</route>
```

The cards need no `v-else`: an empty list renders nothing anyway. Don't put `v-if` and `v-for` on the same element. If you really need both, wrap the loop in `<template v-else>`.

### 6.2 Detail page skeleton (`pages/products/[id].vue`)

```vue
<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { api } from '@utils/http.mjs'
import BaseLayout from '@layouts/BaseLayout.vue'

const route = useRoute()
const product = ref(null)          // "állapot product néven"
const loading = ref(true)          // "kiinduló értéke igaz"

const getProduct = async () => {
  const response = await api.get(`/api/products/${route.params.id}`)   // id from the route
  product.value = response.data.data
  loading.value = false            // "ha a kérés sikeresen végbement"
}

onMounted(() => {
  getProduct()
})
</script>

<template>
  <BaseLayout>
    <p v-if="loading" class="text-center">Načítava sa...</p>
    <div v-if="!loading" class="grid md:grid-cols-3 gap-4">
      <img :src="`/images/${product.image}`" :alt="product.name" class="..." />
      <h2 class="text-3xl font-bold my-4">{{ product.name }}</h2>
      <!-- v-if per paragraph, Badge / Price components, RouterLink back to index -->
    </div>
  </BaseLayout>
</template>

<route lang="yaml">
name: products.show
meta:
  title: Produkt
</route>
```

If the PDF says "reload when the id in the route changes", add the `watch` from 4.12.

### 6.3 Card skeleton (`components/ProductCard.vue`)

```vue
<script setup>
import { RouterLink } from 'vue-router'
import Price from '@components/Price.vue'

defineProps({
  product: { type: Object, required: true }    // "kötelezően vegyen át egy product nevű objektumot"
})
</script>

<template>
  <!-- paste forras/ProductCard.html and replace the sample texts with bindings -->
  <div class="border rounded-xl p-4 flex flex-col">
    <img :src="`/images/${product.image}`" :alt="product.name" class="..." />
    <h3 class="...">{{ product.name }}</h3>
    <p class="text-lg font-black mb-2"><Price :price="product.price" :discount="product.discount ?? 0" /></p>
    <RouterLink :to="{ name: 'products.show', params: { id: product.id } }" class="...given classes...">
      Viac informácií
    </RouterLink>
  </div>
</template>
```

Where are the images? The PDF tells you ("a képek az items mappában" → `/items/${item.image}`; "public/images" → `/images/${...}`). If the API already returns a full URL (CourseHUB), use it as is.

### 6.4 How this maps onto the ÖBB retake

ÖBB is this skeleton **plus** five extra things. These are where the class lost the most points, so practise them the most:

1. **Emits**: Stepper `previous`/`next`, TicketCard `selected` (4.5).
2. **`defineModel`**: Input, and PlannerForm with an object model (4.6).
3. **Fallthrough attributes**: `class`, `@submit.prevent` and `@focusout` written on a component (4.7).
4. **Several fetches on one page**: the hero plus 3 teaser sections, each with its own state and function (4.8).
5. **Loading / searched / results / empty chain** on the timetable page (4.9 and the `handleSubmit` order in section 7).

---

## 7. Worked example: the ÖBB frontend (tasks 17–92) as a points plan

This is the test you have to rewrite and the hardest of all of them. If you can do this one, the retake will be fine. Each group shows which pattern from section 4 to use.

| Tasks | What | Pattern | Watch out |
|---|---|---|---|
| 17 | German page language | `<html lang="de">` | 1 free point |
| 18 | 3 routes + titles | section 2, routing check | names `home`, `timetable.index`, `trains.show` |
| 19, 23 | `--primary`, `screen-reader-text` | edit `main.css` | free points |
| 20–21 | Navbar from template | 4.4, 4.13 | array → `RouterLink` with `v-for`; `open` ref + `openClass` computed; Burger `md:hidden` |
| 22, 24 | `main` + hidden `h1` | Tailwind | `max-w-340 mx-auto pt-12`, `class="screen-reader-text"` |
| 25–27 | `heroContents` + `getHeroContents()` + `onMounted` | 4.8 | exact names |
| 28–31 | Hero component | 4.1, 4.18 | `items` Array required; `overflow-x-hidden` → `flex` + animation 40s infinite |
| 32–35 | HeroItem | 4.1, 4.2, 4.14 | `image` String required; `???` → `url('/images/${image}')`; `slot`; `h3`/`p` responsive classes |
| 36–39 | Button | 4.3 | defaults `submit` / `primary`; `classes` object; `hidden md:block w-full max-w-95` |
| 40–44 | TeaserSection | 4.1, 4.2 | `title` optional; `h2 v-if="title"` + `col-span-full` |
| 45–48 | Card | 4.14 | `img v-if="image"`, `class="w-full"`; `div` with `p-4 bg-white` + slot |
| 49–52 | TeaserCard | 4.1 | wraps `Card`; given classes on `p` |
| 53–54 | 3 teaser states + functions, 2 sections | 4.8, 4.16 | `slice(0, 2)` / `slice(2)`; class on the component (4.7) |
| 55–57 | Copy Stepper etc., emits | 4.5 | `defineEmits(['previous', 'next'])` |
| 58–60 | `search` state, Navbar, main bg | 4.17 | `bg-[url(...)] bg-repeat-x pt-12` |
| 61–64 | PlannerForm: model, today, origins/destinations fetch | 4.6, 4.8 | `defineModel`; `getDestinations(origin)` with query param |
| 65–66 | Input | 4.6 | `defineModel()` + classes |
| 67–71 | Template → own components, autocomplete | 4.7, 4.10, 4.11 | `required autocomplete="off"`, `:min="today"`, `@focusout`, `@mousedown` |
| 72–76 | `getJourneys`, `searched`, `isLoadingTimetable`, `handleSubmit` | 4.8 | `replaceAll('/', '%2F')`; `?time=`; order: true → clear → await → false |
| 77–79 | Result area | 4.9 | `!searched` → UserAccountCard + image; loading → LoadingIcon; list → TrainCard; empty → Alert |
| 80 | White body only on this page | `onMounted`: `document.body.style.backgroundColor = '#fff'` | reset in `onUnmounted` |
| 81 | TicketCard emits `selected` | 4.5 | |
| 82–89 | Train page: `train`, `getTrain`, `selectedTicket = -1`, `isLoading = true` | 4.12 | `useRoute()` for the id |
| 90–92 | Heading, TrainInfo, 4 TicketCards | 4.15, 4.4 | `v-if="price != null"`; `:class="{ 'outline-2': selectedTicket === 0 }"`; `@selected="selectedTicket = 0"` |

The handleSubmit order, written out (task 76):

```js
const handleSubmit = async () => {
  searched.value = true
  isLoadingTimetable.value = true
  journeys.value = []
  try {
    await getJourneys()
  } finally {
    isLoadingTimetable.value = false
  }
}
```

---

## 8. Commands you might need

All of these run **in the VM's terminal**, from the project folder that contains `compose.yaml` (e.g. `cd ~/Projects/vezeteknev-keresztnev-feladat`). They were checked on the fullstack2627 base in your VM.

### 8.1 Starting things

| What | `just` | Plain `docker compose` |
|---|---|---|
| **First start of a fresh base** (creates `.env`, installs frontend packages, starts everything, generates the app key, migrates) | `just init` | – |
| … and also seed the database | `just init --seed` | – |
| Start everything (after the first time) | `just start` | `docker compose up -d` |
| **Backend only** (backend + webserver + db, then composer install, key, migrate) | `just backend` | `docker compose up -d backend webserver` |
| Backend only, with a fresh migrate + seed | `just backend --seed` | – |
| **Frontend only** (+ the proxy) | `just frontend` | `docker compose up -d frontend proxy` |
| Database + phpMyAdmin only | `just db` | `docker compose up -d db phpmyadmin` |
| JSON server only | `just jsonserver` | `docker compose up -d jsonserver proxy` |
| Restart one service (e.g. after editing `.env`) | `just restart frontend` | `docker compose restart frontend` |
| Stop / stop and remove the containers | `just stop` / `just down` | `docker compose stop` / `docker compose down` |
| What is running? | – | `docker compose ps` |

⚠️ Only one project can run at a time, because they all use port 80 (traefik). Before starting another project, stop the old one in its own folder with `docker compose stop`.

### 8.2 Getting a shell inside a container (`fish`)

Both the backend and frontend images have **fish** (sh and ash too; there is no bash):

```bash
docker compose exec backend fish      # prompt is in /www   (Laravel: php artisan ..., composer ...)
docker compose exec frontend fish     # prompt is in /app   (pnpm ...)
docker compose exec db bash           # MySQL container: mysql -u root -p
```

Leave the shell with `exit` (or Ctrl+D).

Inside the **backend** shell:

```bash
php artisan route:list                  # are the /api routes there?
php artisan migrate:fresh --seed        # rebuild the database
php artisan install:api                 # creates routes/api.php (the base has no api.php yet!)
php artisan make:controller ProductController
php artisan test                        # backend tests
```

Inside the **frontend** shell:

```bash
pnpm vitest run          # frontend tests, once
pnpm build               # compiles every .vue file: a quick check for syntax errors
pnpm add <package>       # only if the task asks for a new package
```

### 8.3 One-off commands without opening a shell

```bash
docker compose exec backend php artisan route:list
docker compose exec frontend pnpm vitest run
```

### 8.4 Logs (blank page, 500 error)

```bash
docker compose logs -f frontend      # Vite compile errors with file + line
docker compose logs -f backend       # PHP errors
```

Also `backend/storage/logs/laravel.log`.

### 8.5 Addresses (open them in the VM's browser)

| Address | What |
|---|---|
| `http://frontend.vm1.test` | your Vue app |
| `http://backend.vm1.test/api/...` | the Laravel API (check the JSON here) |
| `http://swagger.vm1.test` | API description |
| `http://pma.vm1.test` | phpMyAdmin |
| `http://jsonserver.vm1.test` | JSON server (not usable from the Vue app: no CORS) |
| `http://vue.vm1.test`, `tailwind.vm1.test`, `vue-router.vm1.test`, `mdn.vm1.test` | offline docs |

---

## 9. Quick fixes

| Symptom | Likely cause | Fix |
|---|---|---|
| Blank page | syntax error in a `.vue` file, or a wrong import path | `docker compose logs frontend` or the browser console; check `@components/X.vue` spelling and case |
| `Cannot read properties of null (reading 'name')` | template uses data before the fetch finished | `v-if="!loading"` / `v-if="course"` / `course?.name` |
| Data never shows | used `response.data` instead of `response.data.data` (or the reverse) | `console.log(response.data)` once and look |
| Requests go to a server with no data | `VITE_BACKEND_URL` points to the wrong server | point the root `.env` at the backend the teacher prepared, restart frontend |
| Console: `blocked by CORS policy`, page stuck on "loading" | the frontend calls the JSON server (no CORS), or a backend route outside `/api/*` | use `http://${DOMAIN_BACKEND}` and `/api/...` paths |
| `default: 0` prop is still `null` | a prop default only applies when the value is **undefined**, not `null` | pass `:discount="product.discount ?? 0"` |
| 404 on the API | wrong path, a missing `/api`, or a doubled `/api/api/` | look at the URL in the Network tab; check `VITE_BACKEND_URL` and `http.mjs` (section 2) |
| Blank page after adding `<style scoped>` with `@apply` | missing `@reference "@assets/main.css";` | add it as the first line of the style block (4.23) |
| Warning `Invalid prop: type check failed ... Expected Number, got String` | API sends numbers as strings | `type: [Number, String]` and `Number(x)` |
| Lookup shows nothing for `'Tickled Pink'` | object key with a space written without quotes, or a different case | `'Tickled Pink': '...'`; copy the keys exactly from the data |
| Image broken | wrong folder or prefix | `public/images/x.png` → `/images/x.png`; check what the API returns |
| Tab title `undefined \| ...` | route has no `meta.title` | add the `<route>` block or `meta` |
| Component v-model doesn't update the parent | used `defineProps` instead of `defineModel` | `const model = defineModel()` |
| Class on a component does nothing | component has several root elements | wrap the template in one root `div` |
| Warning `Missing required prop` | forgot to pass the prop, or a typo in its name | `:items="heroContents"` (with the `:`!) |
| Prop is the string `"heroContents"` | missing `:` | `:items=`, not `items=` |
| Function "is not defined" in the template | not imported or not defined in `<script setup>` | import it |
| Tailwind class has no effect | class name typo, or a dynamic string Tailwind can't see | write full class names (`'md:col-span-3'`, never `'md:col-span-' + n`) |

---

## 10. Summary for tonight

1. **Write the section 6 skeleton (list page + detail page + card) from memory once**, without looking. Then change the names to ASDA's and check it against 6's table. That covers about 60–70% of the points in CourseHUB, ASDA and Kaufland.
2. Practise the **ÖBB extras** (6.4): emits, `defineModel`, fallthrough `class`/`@submit.prevent`, several fetches on one page, the `handleSubmit` order.
3. Learn the **new small patterns**: table → lookup object in a `computed` (4.19), `<option>` from an object (4.20), price component with `null`/`0` default and `<s>` (4.21), wrapping an element in a `RouterLink` (4.22).
4. Skim the dictionary in section 3, especially the breakpoints: plain class = default, `sm:` = "kis eszköztől", `md:` / `lg:` / `xl:`.
5. Tomorrow:
   - First check the `/api` prefix and the response shape.
   - Then the setup points, then the small components, then the pages.
   - Skip anything you're stuck on, and never leave a page broken.
   - "számított állapot X néven" always means `const X = computed(...)`.
