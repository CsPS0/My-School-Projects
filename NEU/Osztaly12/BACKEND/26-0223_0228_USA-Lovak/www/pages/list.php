<ul>
    <?php foreach ($horses as $horse): ?>
    <li class="my-2 pl-2 border-l-2 border-blue-200 text-blue-700">
        <?php echo "$horse->breed ($horse->state - $horse->year)"; ?>
    </li>
    <?php endforeach; ?>
</ul>
