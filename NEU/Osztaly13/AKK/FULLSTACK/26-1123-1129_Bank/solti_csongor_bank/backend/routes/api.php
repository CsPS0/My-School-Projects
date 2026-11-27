<?php

use App\Http\Controllers\AccountController;
use App\Http\Controllers\ClientController;
use App\Http\Controllers\TransactionController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

Route::get([
    "clients" => ClientController::class,
    "accounts" => AccountController::class
]);

Route::get(
    "transactions", TransactionController::class
)
->except(["update", "destroy"]); //->only(["index", "show", "store"]);