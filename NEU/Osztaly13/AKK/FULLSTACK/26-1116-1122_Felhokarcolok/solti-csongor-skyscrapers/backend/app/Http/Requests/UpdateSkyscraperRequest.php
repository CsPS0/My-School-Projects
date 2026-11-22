<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateSkyscraperRequest extends FormRequest
{
    public function authorize(): bool
    {
        return true;
    }

    public function rules(): array
    {
        return [
            'name' => ['required', 'string', 'min:2', 'max:50'],
            'city_id' => ['required', 'integer', 'exists:cities,id'],
            'height' => ['required', 'numeric', 'between:140,1000'],
            'stories' => ['nullable', 'integer', 'between:25,300'],
            'finished' => ['nullable', 'integer', 'between:1900,3000'],
        ];
    }
}
