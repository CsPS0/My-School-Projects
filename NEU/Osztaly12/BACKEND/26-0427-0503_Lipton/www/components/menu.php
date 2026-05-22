<?php
$currentPage = $_GET['page'] ?? 'home';
?>
<nav class="bg-minta-yellow p-4 shadow-sm">
    <div class="container mx-auto flex items-center justify-between">
        <div class="flex items-center space-x-8">
            <a href="index.php" class="text-2xl">Teák</a>
            <div id="nav-menu" class="hidden md:flex space-x-6">
                <a href="index.php" class="<?php echo $currentPage === 'home' ? 'font-bold underline' : 'hover:text-gray-700'; ?>">Főoldal</a>
                <a href="index.php?page=table" class="<?php echo $currentPage === 'table' ? 'font-bold underline' : 'hover:text-gray-700'; ?>">Táblázat</a>
                <a href="index.php?page=cards" class="<?php echo $currentPage === 'cards' ? 'font-bold underline' : 'hover:text-gray-700'; ?>">Cards</a>
            </div>
        </div>
        <button id="burger-btn" class="md:hidden p-2 border border-gray-400 rounded">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16m-7 6h7"></path></svg>
        </button>
    </div>
    <div id="mobile-menu" class="hidden md:hidden mt-4 flex flex-col space-y-4 pb-4">
        <a href="index.php" class="<?php echo $currentPage === 'home' ? 'font-bold underline' : 'hover:text-gray-700'; ?>">Főoldal</a>
        <a href="index.php?page=table" class="<?php echo $currentPage === 'table' ? 'font-bold underline' : 'hover:text-gray-700'; ?>">Táblázat</a>
        <a href="index.php?page=cards" class="<?php echo $currentPage === 'cards' ? 'font-bold underline' : 'hover:text-gray-700'; ?>">Cards</a>
    </div>
</nav>

<script>
    document.getElementById('burger-btn').addEventListener('click', function() {
        var menu = document.getElementById('mobile-menu');
        menu.classList.toggle('hidden');
    });
</script>
