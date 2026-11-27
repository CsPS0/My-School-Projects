<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class ClientResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            "id"=> $this->id,
            "name"=> $this->name,
            "birth_date" => $this->birth_date,
            "birth_place" => $this->birth_place,
            "ssn_last_4_digits" => $this->ssn_last_4_digits,
            "accounts" => AccountResource::collection($this->whenLoaded("accounts")),
        ];
    }
}
