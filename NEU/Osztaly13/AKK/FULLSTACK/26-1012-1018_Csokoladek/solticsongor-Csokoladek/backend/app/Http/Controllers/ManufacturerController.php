<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ManufacturerController extends Controller
{
    public function index()
    {
        $manufacturers = DB::table('manufacturers')->get();

        return response()->json([
            'data' => $manufacturers
        ], 200);
    }

    public function show(int $id)
    {
        $manufacturer = DB::table('manufacturers')->where('id', $id)->first();

        if (!$manufacturer) {
            return response()->json([
                'message' => 'A gyártó nem található'
            ], 404);
        }

        return response()->json([
            'data' => $manufacturer
        ], 200);
    }
}
