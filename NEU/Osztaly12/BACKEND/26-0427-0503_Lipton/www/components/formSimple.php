<form action="index.php" method="GET" class="mb-12 flex flex-col items-center">
    <input type="hidden" name="page" value="<?php echo $_GET['page'] ?? 'table'; ?>">
    
    <div class="flex flex-col items-center mb-4">
        <label for="name" class="block text-gray-700 mb-2">Név</label>
        <input type="text" name="name" id="name" value="<?php echo htmlspecialchars($_GET['name'] ?? ''); ?>" class="w-64 md:w-96 p-2 border border-gray-300 rounded bg-white">
    </div>
    
    <button type="submit" class="btn-minta-green px-12 py-2 rounded text-lg transition duration-200">Keresés</button>
</form>
