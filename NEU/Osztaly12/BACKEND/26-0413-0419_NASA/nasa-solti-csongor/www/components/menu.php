<nav class="bg-slate-950 px-8 py-4 flex flex-wrap justify-between items-center border-b-2 border-slate-700 gap-4">
    <div class="flex items-center gap-4">
        <img src="./images/nasa_logo.png" alt="NASA Logo" class="h-11 w-auto object-contain" onerror="this.style.display='none'">
        <h1 class="m-0 text-2xl font-bold tracking-wide">Űrprogramok</h1>
    </div>

    <ul class="flex flex-wrap justify-center sm:justify-end gap-3 m-0 p-0 list-none w-full sm:w-auto mt-2 sm:mt-0">
        <li>
            <a href="index.php?view=grid" class="<?= $view === 'grid' ? 'bg-slate-800 border-slate-700' : 'bg-transparent border-transparent' ?> inline-block text-slate-100 no-underline px-4 py-2 rounded-md border hover:bg-blue-600 transition-colors duration-300 whitespace-nowrap">Rács nézet</a>
        </li>
        <li>
            <a href="index.php?view=table" class="<?= $view === 'table' ? 'bg-slate-800 border-slate-700' : 'bg-transparent border-transparent' ?> inline-block text-slate-100 no-underline px-4 py-2 rounded-md border hover:bg-blue-600 transition-colors duration-300 whitespace-nowrap">Táblázatos nézet</a>
        </li>
    </ul>
</nav>
