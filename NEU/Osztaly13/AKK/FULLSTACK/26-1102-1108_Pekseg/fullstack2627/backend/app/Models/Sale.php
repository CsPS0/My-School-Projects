<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Sale extends Model
{
    use HasFactory;

    public $timestamps = false;

    protected $fillable = [
        'date',
        'location_id',
        'revenue',
    ];

    public function location()
    {
        return $this->belongsTo(Location::class);
    }
}
