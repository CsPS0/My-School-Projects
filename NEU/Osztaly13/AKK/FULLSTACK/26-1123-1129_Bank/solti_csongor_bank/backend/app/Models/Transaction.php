<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Transaction extends Model
{
    public function senderAccount(): BelongsTo {
        return $this->belongsTo(Account::class, "sender_account_id");
    }

    public function recipientAccount(): BelongsTo {
        return $this->belongsTo(Account::class, "recipient_account_id");
    }
}
