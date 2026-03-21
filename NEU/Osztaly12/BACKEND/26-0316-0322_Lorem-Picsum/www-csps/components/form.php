<form action="index.php" method="GET">
    <label for="id">Kép kiválasztása
        <select name="id" id="id">
            <?php foreach ($images as $img_id => $name): ?>
                <option value="<?php echo $img_id; ?>" <?php echo $id == $img_id ? 'selected' : ''; ?>>
                    <?php echo $name; ?>
                </option>
            <?php endforeach; ?>
        </select>
    </label>

    <label for="width">Szélesség (px)
        <input type="number" name="width" id="width" min="100" value="<?php echo $width; ?>">
    </label>

    <label for="height">Magasság (px)
        <input type="number" name="height" id="height" min="100" value="<?php echo $height; ?>">
    </label>

    <fieldset>
        <legend>Elmosás mértéke</legend>
        <?php for ($i = 0; $i <= 3; $i++): ?>
            <label>
                <input type="radio" name="blur" value="<?php echo $i; ?>" <?php echo $blur == $i ? 'checked' : ''; ?>>
                <?php echo $i; ?>
            </label>
        <?php endfor; ?>
    </fieldset>

    <label for="grayscale">
        <input type="checkbox" name="grayscale" id="grayscale" <?php echo $grayscale ? 'checked' : ''; ?>>
        Szürkeárnyalatos
    </label>

    <button type="submit">Generálás</button>
</form>
