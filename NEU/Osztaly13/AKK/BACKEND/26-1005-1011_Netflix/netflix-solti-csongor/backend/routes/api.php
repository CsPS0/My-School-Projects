<?php

use App\Http\Controllers\ToplistController;
use Illuminate\Support\Facades\Route;

Route::prefix('toplist')->name('toplist.')->group(function () {
    Route::get('/', [ToplistController::class, 'index'])->name('index');
    Route::get('/categories', [ToplistController::class, 'categories'])->name('categories');
    Route::get('/films', [ToplistController::class, 'films'])->name('films');
    Route::get('/tvs', [ToplistController::class, 'tvs'])->name('tvs');
    Route::get('/popular', [ToplistController::class, 'popular'])->name('popular');
    Route::get('/week/{week}', [ToplistController::class, 'week'])
        ->name('week')
        ->where('week', '^((19[0-9]{2}|20[0-9]{2})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01]))$');
    Route::get('/top1/{week}', [ToplistController::class, 'top1'])
        ->name('top1')
        ->where('week', '^((19[0-9]{2}|20[0-9]{2})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01]))$');
});
