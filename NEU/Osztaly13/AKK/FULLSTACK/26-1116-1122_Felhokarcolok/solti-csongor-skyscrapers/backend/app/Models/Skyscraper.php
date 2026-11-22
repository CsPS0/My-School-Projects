<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Attributes\Fillable;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

#[Fillable('name','city_id','height','stories','finished')]
class Skyscraper extends Model
{
    public $timestamps = false;

    public function city(): BelongsTo {
        return $this->belongsTo(City::class);
    }
}
