<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('accounts', function (Blueprint $table) {
            $table->id();
            $table->enum("type", ["checking", "traditional", "savings", "family savigns"]);
            $table->string("name", 25)->nullable();
            $table->decimal("balance")->nullable()->default(null);
            $table->foreignId("client_id")->constrained("clients", "id");
            $table->timestamps();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('accounts');
    }
};
