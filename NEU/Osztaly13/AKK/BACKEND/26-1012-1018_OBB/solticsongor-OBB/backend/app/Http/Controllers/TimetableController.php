<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class TimetableController extends Controller
{
    public function origins()
    {
        $origins = DB::table('trains')
            ->distinct()
            ->orderBy('origin', 'asc')
            ->pluck('origin');

        return response()->json([
            'data' => $origins,
        ]);
    }

    public function destinations(Request $request)
    {
        $query = DB::table('trains')
            ->distinct()
            ->orderBy('destination', 'asc');

        if ($request->has('origin')) {
            $query->where('origin', $request->query('origin'));
        }

        $destinations = $query->pluck('destination');

        return response()->json([
            'data' => $destinations,
        ]);
    }

    public function planner(Request $request, string $from, string $to)
    {
        $query = DB::table('trains')
            ->select([
                'id',
                'train_number',
                'origin',
                'destination',
                'departure_time',
                'arrival_time',
                'second_class_price',
            ])
            ->where('origin', 'like', "%{$from}%")
            ->where('destination', 'like', "%{$to}%");

        if ($request->has('time')) {
            $time = $request->query('time');
            if ($request->query('time-type') === 'arrival') {
                $query->where('arrival_time', '<=', $time);
            } else {
                $query->where('departure_time', '>=', $time);
            }
        }

        $trains = $query
            ->orderBy('is_overnight', 'asc')
            ->orderBy('arrival_time', 'asc')
            ->orderBy('departure_time', 'desc')
            ->orderBy('second_class_price', 'asc')
            ->get();

        return response()->json([
            'data' => $trains,
        ]);
    }
}
