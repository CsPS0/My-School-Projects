<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('skyscrapers', function (Blueprint $table) {
            $table->id();
            $table->string('name', 50);
            $table->foreignId('city_id')->constrained()->cascadeOnDelete();
            $table->float('height');
            $table->integer('stories')->nullable();
            $table->integer('finished')->nullable();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('skyscrapers');
    }
};
