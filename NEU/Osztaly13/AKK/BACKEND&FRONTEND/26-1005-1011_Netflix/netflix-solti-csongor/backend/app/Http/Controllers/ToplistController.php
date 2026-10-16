<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ToplistController extends Controller
{
    public function index()
    {
        $items = DB::table('toplist')->get();

        return response()->json([
            'data' => $items,
        ]);
    }

    public function categories()
    {
        $categories = DB::table('toplist')
            ->distinct()
            ->pluck('category');

        return response()->json([
            'data' => $categories,
        ]);
    }

    public function films()
    {
        $items = DB::table('toplist')
            ->where('category', 'like', 'Films%')
            ->get();

        return response()->json([
            'data' => $items,
        ]);
    }

    public function tvs()
    {
        $items = DB::table('toplist')
            ->where('category', 'like', 'TV%')
            ->get();

        return response()->json([
            'data' => $items,
        ]);
    }

    public function popular()
    {
        $items = DB::table('toplist')
            ->where('cumulative_weeks_in_top_ten', '>=', 23)
            ->orWhere('weekly_hours_viewed', '>=', 158680000)
            ->orderByDesc('weekly_hours_viewed')
            ->get();

        return response()->json([
            'data' => $items,
        ]);
    }

    public function week(Request $request, $week)
    {
        $query = DB::table('toplist')->where('week', $week);

        $orderBy = $request->query('order_by');
        if ($orderBy === 'category') {
            $query->orderBy('category', 'asc');
        } elseif ($orderBy === 'weekly_rank') {
            $query->orderBy('weekly_rank', 'asc');
        } elseif ($orderBy === 'weekly_hours_viewed') {
            $query->orderBy('weekly_hours_viewed', 'desc');
        }

        $items = $query->get();

        return response()->json([
            'data' => [
                'week' => $week,
                'items' => $items,
            ],
        ]);
    }

    public function top1($week)
    {
        $title = DB::table('toplist')
            ->where('week', $week)
            ->where('category', 'TV (English)')
            ->where('weekly_rank', 1)
            ->value('show_title');

        return response()->json([
            'data' => [
                'week' => $week,
                'title' => $title,
            ],
        ]);
    }
}
