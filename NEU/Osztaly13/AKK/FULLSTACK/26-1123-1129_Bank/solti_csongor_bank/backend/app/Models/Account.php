<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Account extends Model
{
    public function client(): BelongsTo {
        return $this->belongsTo(Client::class);
    }

    public function sentTransaction(): HasMany {
        return $this->hasMany(Transaction::class, "sender_account_id");
    }
    
    public function receiveTransaction(): HasMany {
        return $this->hasMany(Transaction::class, "recipient_account_id");
    }
}