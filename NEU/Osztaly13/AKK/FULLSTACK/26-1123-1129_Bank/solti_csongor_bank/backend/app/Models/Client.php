<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Attributes\Fillable;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

#[Fillable("name", "birth_date", "birth_place", "snn_last_4_digits")]
class Client extends Model
{
    public function accounts(): HasMany {
        return $this->hasMany(Account::class);
    }
}
