<?php include __DIR__ . '/../components/formComplex.php'; ?>

<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
    <?php foreach ($filteredTeas as $tea): ?>
        <div class="bg-white border border-gray-200 rounded-lg p-6 shadow-sm flex flex-col items-center">
            <div class="w-full h-48 mb-6 flex items-center justify-center">
                <img src="<?php echo $tea->image_path; ?>" alt="<?php echo htmlspecialchars($tea->name); ?>" class="max-w-full max-h-full object-contain">
            </div>
            
            <h2 class="text-xl text-center mb-8 h-16 flex items-center text-gray-800"><?php echo htmlspecialchars($tea->name); ?></h2>
            
            <div class="w-full flex justify-between items-center mt-auto">
                <a href="index.php?page=tea&id=<?php echo $tea->id; ?>" class="bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700 transition">Részletek</a>
                <span class="bg-gray-500 text-white px-4 py-2 rounded text-sm">
                    Ár: <?php echo $tea->getFormattedPrice('HUF'); ?>
                </span>
            </div>
        </div>
    <?php endforeach; ?>
</div>
