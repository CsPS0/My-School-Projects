<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\ManufacturerController;
use App\Http\Controllers\ChocolateController;

Route::get('/user', function (Request $request) { return $request->user(); })->middleware('auth:sanctum');

Route::get('/manufacturers', [ManufacturerController::class, 'index'])->name('manufacturers.index');
Route::get('/manufacturers/{id}', [ManufacturerController::class, 'show'])->whereNumber('id')->name('manufacturers.show');
Route::get('/chocolates', [ChocolateController::class, 'index'])->name('chocolates.index');
Route::get('/chocolates/{id}', [ChocolateController::class, 'show'])->whereNumber('id')->name('chocolates.show');
