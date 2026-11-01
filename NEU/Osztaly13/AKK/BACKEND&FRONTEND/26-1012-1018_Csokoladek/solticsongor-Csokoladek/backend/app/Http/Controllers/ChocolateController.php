<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ChocolateController extends Controller
{
    public function index()
    {
        $chocolates = DB::table('chocolates')->get();

        return response()->json([
            'data' => $chocolates
        ], 200);
    }

    public function show(int $id)
    {
        $chocolate = DB::table('chocolates')->where('id', $id)->first();

        if (!$chocolate) {
            return response()->json([
                'message' => 'A csokoládé nem található'
            ], 404);
        }

        return response()->json([
            'data' => $chocolate
        ], 200);
    }
}
