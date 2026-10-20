<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('hero_contents', function (Blueprint $table) {
            $table->id();
            $table->string('image_url', 50);
            $table->string('title', 100);
            $table->string('description', 200);
            $table->string('button_text', 100);
        });

        Schema::create('teasers', function (Blueprint $table) {
            $table->id();
            $table->string('image_url', 50);
            $table->string('title', 100);
            $table->string('description', 200)->nullable();
            $table->string('section', 50);
            $table->integer('order');
        });

        Schema::create('trains', function (Blueprint $table) {
            $table->id();
            $table->string('train_number', 255);
            $table->string('origin', 50);
            $table->string('destination', 50);
            $table->time('departure_time');
            $table->time('arrival_time');
            $table->double('second_class_price');
            $table->double('first_class_price')->nullable();
            $table->double('business_class_price')->nullable();
            $table->boolean('is_overnight')->default(false);
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('trains');
        Schema::dropIfExists('teasers');
        Schema::dropIfExists('hero_contents');
    }
};
