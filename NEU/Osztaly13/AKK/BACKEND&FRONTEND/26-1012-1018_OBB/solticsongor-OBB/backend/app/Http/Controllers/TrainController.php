<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\DB;

class TrainController extends Controller
{
    public function show(int $train)
    {
        $trainData = DB::table('trains')->where('id', $train)->first();

        if (!$trainData) {
            return response()->json([
                'message' => 'Train not found'
            ], 404);
        }

        return response()->json([
            'data' => $trainData,
        ]);
    }
}
