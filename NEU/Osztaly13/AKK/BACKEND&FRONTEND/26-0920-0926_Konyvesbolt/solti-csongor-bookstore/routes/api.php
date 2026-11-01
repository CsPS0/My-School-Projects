<?php

use App\Http\Controllers\BookController;
use Illuminate\Support\Facades\Route;

Route::prefix('books')->name('books.')->group(function () {
    Route::get('/', [BookController::class, 'index'])->name('index');
    Route::get('/most-expensive', [BookController::class, 'mostExpensive'])->name('mostExpensive');
    Route::get('/random', [BookController::class, 'random'])->name('random');
    Route::get('/popular', [BookController::class, 'popular'])->name('popular');
    Route::get('/genre/{genre}', [BookController::class, 'genre'])->name('genre');
    Route::get('/{id}', [BookController::class, 'show'])->name('show')->whereNumber('id');
});
