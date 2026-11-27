<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class TransactionResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            "id"=> $this->id,
            "transactions_at" => $this->transactions_at,
            "note" => $this->note,
            "amount" => $this->amount,
            "sender_account_id" => $this->sender_account_id,
            "recipient_account_id" => $this->recipient_account_id
        ];
    }
}
