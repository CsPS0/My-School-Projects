<div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
    <?php if (empty($missions)): ?>
        <p class="col-span-full text-center text-slate-400 py-10">Nincs találat a megadott feltételekkel.</p>
    <?php else: ?>
        <?php foreach ($missions as $m): ?>
            <div class="bg-slate-800 rounded-xl overflow-hidden shadow-lg border border-slate-700 transition-transform duration-300 hover:-translate-y-1 hover:shadow-xl group">
                <div class="w-full h-48 bg-slate-950 overflow-hidden border-b border-slate-700">
                    <img src="./images/<?= strtolower($m->program) ?>.png" alt="<?= $m->name ?>" class="w-full h-full object-cover object-center transition-transform duration-500 group-hover:scale-110" onerror="this.src='./images/placeholder.webp'">
                </div>
                
                <div class="p-4 flex justify-between items-center border-b border-slate-700">
                    <h3 class="m-0 text-xl font-bold text-slate-100"><?= $m->name ?></h3>
                    <span class="bg-slate-900 px-2 py-1 rounded text-xs font-semibold uppercase tracking-wider border border-slate-600 text-slate-300">
                        <?= $m->program ?>
                    </span>
                </div>
                
                <div class="p-5 space-y-2 text-slate-300 text-sm">
                    <p><strong class="text-slate-100">Dátum:</strong> <?= $m->launchDate ?></p>
                    <p><strong class="text-slate-100">Legénység:</strong> <?= $m->crewSize ?> fő</p>
                    <p><strong class="text-slate-100">Státusz:</strong> 
                        <?php
                            $statusClass = match($m->status) {
                                'Sikeres' => 'text-emerald-400',
                                'Sikertelen' => 'text-red-400',
                                default => 'text-yellow-400',
                            };
                        ?>
                        <span class="<?= $statusClass ?>"><?= $m->status ?></span>
                    </p>
                    <p class="mt-4 text-lg font-bold text-blue-400 pt-2 border-t border-slate-700">
                        <?= $m->getFormattedBudget() ?>
                    </p>
                </div>
            </div>
        <?php endforeach; ?>
    <?php endif; ?>
</div>
