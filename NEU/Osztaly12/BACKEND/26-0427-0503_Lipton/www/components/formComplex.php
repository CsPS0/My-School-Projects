<?php
use Acme\Product\Tea;
?>
<form action="index.php" method="GET" class="mb-12 flex flex-col md:flex-row items-end justify-center space-y-4 md:space-y-0 md:space-x-4">
    <input type="hidden" name="page" value="<?php echo $_GET['page'] ?? 'cards'; ?>">
    
    <div class="flex flex-col">
        <label for="range" class="block text-gray-700 mb-1 text-sm">Típus</label>
        <select name="range" id="range" class="p-2 border border-gray-300 rounded bg-white w-48">
            <option value="">Összes</option>
            <?php foreach (Tea::ranges() as $range): ?>
                <option value="<?php echo $range; ?>" <?php echo (isset($_GET['range']) && $_GET['range'] === $range) ? 'selected' : ''; ?>>
                    <?php echo $range; ?>
                </option>
            <?php endforeach; ?>
        </select>
    </div>
    
    <div class="flex flex-col">
        <label for="qty_min" class="block text-gray-700 mb-1 text-sm">Mennyiség (minimum)</label>
        <input type="number" name="qty_min" id="qty_min" value="<?php echo htmlspecialchars($_GET['qty_min'] ?? '0'); ?>" class="p-2 border border-gray-300 rounded bg-white w-48">
    </div>
    
    <div class="flex flex-col">
        <label for="qty_max" class="block text-gray-700 mb-1 text-sm">Mennyiség (max)</label>
        <input type="number" name="qty_max" id="qty_max" value="<?php echo htmlspecialchars($_GET['qty_max'] ?? '1000'); ?>" class="p-2 border border-gray-300 rounded bg-white w-48">
    </div>
    
    <button type="submit" class="btn-minta-green px-12 py-2 rounded transition duration-200">Submit</button>
</form>
