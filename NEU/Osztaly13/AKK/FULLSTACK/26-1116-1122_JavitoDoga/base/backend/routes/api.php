<?php

use App\Http\Controllers\GameController;
use Illuminate\Support\Facades\Route;

// Route::get('/user', function (Request $request) {
//     return $request->user();
// })->middleware('auth:sanctum');

Route::apiResource("games",GameController::class)->whereNumber("id")->only("index","show");