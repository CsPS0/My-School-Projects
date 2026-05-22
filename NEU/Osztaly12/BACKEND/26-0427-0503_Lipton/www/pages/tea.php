<?php
$tea = $teas[$id] ?? null;
?>
<div class="max-w-4xl mx-auto flex flex-col md:flex-row items-start space-y-8 md:space-y-0 md:space-x-12 mt-12">
    <div class="md:w-1/2 flex justify-center">
        <img src="<?php echo $tea->image_path; ?>" alt="<?php echo htmlspecialchars($tea->name); ?>" class="max-w-full h-auto">
    </div>
    <div class="md:w-1/2">
        <ul class="text-xl space-y-4 text-gray-800">
            <li><span class="font-bold">Márka:</span> <?php echo htmlspecialchars($tea->brand); ?></li>
            <li><span class="font-bold">Típus:</span> <?php echo htmlspecialchars($tea->range); ?></li>
            <li><span class="font-bold">Kiszerelés:</span> <?php echo htmlspecialchars($tea->format); ?></li>
            <li><span class="font-bold">Mennyiség:</span> <?php echo $tea->qty . ' ' . $tea->unit; ?></li>
            <li><span class="font-bold">Ár:</span> <?php echo $tea->getFormattedPrice('HUF'); ?></li>
        </ul>
    </div>
</div>
