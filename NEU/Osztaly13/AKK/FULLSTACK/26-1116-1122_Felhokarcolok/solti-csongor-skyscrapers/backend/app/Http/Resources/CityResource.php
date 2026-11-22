<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class CityResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            'id' => $this->id,
            'country_code' => $this->country_code,
            'name' => $this->name,
            'skyscrapers' => SkyscraperResource::collection($this->whenLoaded('skyscrapers')),
        ];
    }
}
