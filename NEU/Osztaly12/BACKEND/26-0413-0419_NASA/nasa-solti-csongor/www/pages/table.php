<div class="overflow-x-auto bg-slate-800 rounded-xl shadow-lg border border-slate-700">
    <?php if (empty($missions)): ?>
        <p class="p-6 text-slate-400">Nincs találat a megadott feltételekkel.</p>
    <?php else: ?>
        <table class="w-full border-collapse text-left whitespace-nowrap">
            <thead>
                <tr class="bg-slate-950 border-b-2 border-slate-700 text-slate-300 text-sm uppercase tracking-wider">
                    <th class="p-4 font-semibold">ID</th>
                    <th class="p-4 font-semibold">Program</th>
                    <th class="p-4 font-semibold">Küldetés neve</th>
                    <th class="p-4 font-semibold">Dátum</th>
                    <th class="p-4 font-semibold">Legénység</th>
                    <th class="p-4 font-semibold">Státusz</th>
                    <th class="p-4 font-semibold">Költségvetés</th>
                </tr>
            </thead>
            <tbody class="divide-y divide-slate-700">
                <?php foreach ($missions as $m): 
                    $statusClasses = match($m->status) {
                        'Sikeres' => 'bg-emerald-500/20 text-emerald-400 border border-emerald-500/30',
                        'Sikertelen' => 'bg-red-500/20 text-red-400 border border-red-500/30',
                        default => 'bg-yellow-500/20 text-yellow-400 border border-yellow-500/30',
                    };
                ?>
                    <tr class="hover:bg-slate-700/50 transition-colors duration-200">
                        <td class="p-4 text-slate-400"><?= $m->id ?></td>
                        <td class="p-4 text-slate-100 font-bold"><?= $m->program ?></td>
                        <td class="p-4 text-slate-200"><?= $m->name ?></td>
                        <td class="p-4 text-slate-300"><?= $m->launchDate ?></td>
                        <td class="p-4 text-slate-300"><?= $m->crewSize ?> fő</td>
                        <td class="p-4">
                            <span class="px-2 py-1 rounded-full text-xs font-medium <?= $statusClasses ?>">
                                <?= $m->status ?>
                            </span>
                        </td>
                        <td class="p-4 text-blue-400 font-medium"><?= $m->getFormattedBudget() ?></td>
                    </tr>
                <?php endforeach; ?>
            </tbody>
        </table>
    <?php endif; ?>
</div>
