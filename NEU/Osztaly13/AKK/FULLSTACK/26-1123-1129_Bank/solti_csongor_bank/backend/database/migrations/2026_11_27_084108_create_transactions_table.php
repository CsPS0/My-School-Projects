<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('transactions', function (Blueprint $table) {
            $table->id();
            $table->dateTime("transactions_at");
            $table->string("note", 255)->nullable()->default(null);
            $table->decimal("amount");
            $table->foreignId("sender_account_id")->constrained("accounts", "id");
            $table->foreignId("recipient_account_id")->constrained("accounts", 'id');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('transactions');
    }
};
