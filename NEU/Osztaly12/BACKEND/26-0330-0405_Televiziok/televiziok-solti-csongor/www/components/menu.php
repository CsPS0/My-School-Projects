<nav>
    <ul class="flex space-x-4">
        <?php foreach ($menuItems as $key => $value) : ?>
            <li>
                <a href="?page=<?= $key ?>&terminus=<?= $terminusFilter ?>&interconnected=<?= $interconnectedFilter ?>&since=<?= $sinceFilter ?>" class="hover:underline <?= $page === $key ? 'font-bold' : '' ?>"> <?= $value ?></a>
            </li>
        <?php endforeach; ?>
    </ul>
</nav>
