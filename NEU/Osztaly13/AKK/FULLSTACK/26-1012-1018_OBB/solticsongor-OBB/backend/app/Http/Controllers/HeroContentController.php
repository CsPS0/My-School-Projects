<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\DB;

class HeroContentController extends Controller
{
    public function index()
    {
        $items = DB::table('hero_contents')->get();

        return response()->json([
            'data' => $items,
        ]);
    }
}
