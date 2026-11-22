<?php

use App\Http\Controllers\CityController;
use App\Http\Controllers\SkyscraperController;
use Illuminate\Support\Facades\Route;

Route::get('skyscrapers', [SkyscraperController::class, 'index'])->name('skyscraper.index');
Route::get('skyscrapers/{skyscraper}', [SkyscraperController::class, 'show'])->whereNumber('skyscraper')->name('skyscraper.show');
Route::post('skyscrapers', [SkyscraperController::class, 'store'])->name('skyscraper.store');
Route::put('skyscrapers/{skyscraper}', [SkyscraperController::class, 'update'])->whereNumber('skyscraper')->name('skyscraper.update');
Route::delete('skyscrapers/{skyscraper}', [SkyscraperController::class, 'destroy'])->whereNumber('skyscraper')->name('skyscraper.destroy');

Route::get('cities', [CityController::class, 'index'])->name('city.index');
Route::get('cities/{city}', [CityController::class, 'show'])->whereNumber('city')->name('city.show');
Route::post('cities', [CityController::class, 'store'])->name('city.store');
Route::put('cities/{city}', [CityController::class, 'update'])->whereNumber('city')->name('city.update');
Route::delete('cities/{city}', [CityController::class, 'destroy'])->whereNumber('city')->name('city.destroy');
