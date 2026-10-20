<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\HeroContentController;
use App\Http\Controllers\TeaserController;
use App\Http\Controllers\TrainController;
use App\Http\Controllers\TimetableController;

Route::get('/hero-contents', [HeroContentController::class, 'index'])
    ->name('hero-contents.index');

Route::get('/teasers', [TeaserController::class, 'index'])
    ->name('teasers.index');

Route::get('/trains/{train}', [TrainController::class, 'show'])
    ->name('trains.show')
    ->where('train', '^[0-9]+$');

Route::get('/timetable/stations/origins', [TimetableController::class, 'origins'])
    ->name('timetable.origins');

Route::get('/timetable/stations/destinations', [TimetableController::class, 'destinations'])
    ->name('timetable.destinations');

Route::get('/timetable/planner/{from}/{to}', [TimetableController::class, 'planner'])
    ->name('timetable.planner')
    ->where([
        'from' => '^[\p{L}\s\.\-\/]+$',
        'to' => '^[\p{L}\s\.\-\/]+$',
    ]);