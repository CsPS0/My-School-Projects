<?php

use App\Http\Controllers\ProductController;
use Illuminate\Support\Facades\Route;

Route::get('products', [ProductController::class, 'index'])->name('products.index');
Route::get('products/cheaps', [ProductController::class, 'cheaps'])->name('products.cheaps');
Route::get('products/sum', [ProductController::class, 'sum'])->name('products.sum');
Route::get('products/recommended', [ProductController::class, 'recommended'])->name('products.recommended');
Route::get('products/discounted', [ProductController::class, 'discounted'])->name('products.discounted');
Route::get('products/{id}', [ProductController::class, 'show'])->whereNumber('id')->name('products.show');
