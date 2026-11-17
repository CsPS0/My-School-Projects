<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class TeaserController extends Controller
{
    public function index(Request $request)
    {
        $query = DB::table('teasers');

        if ($request->has('section')) {
            $query->where('section', $request->query('section'))
                ->orderBy('order', 'asc');
        }

        return response()->json([
            'data' => $query->get(),
        ]);
    }
}
