<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Attributes\Fillable;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

#[Fillable('country_code','name')]
class City extends Model
{
    public $timestamps = false;

    public function skyscrapers(): HasMany {
        return $this->hasMany(Skyscraper::class);
    }
}
