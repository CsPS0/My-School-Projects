<?php

namespace App\Http\Controllers;

use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ItemController extends Controller
{
    public function index(): JsonResponse
    {
        return response()->json([
            'data' => DB::table('items')->get(),
        ]);
    }

    public function show(int $id): JsonResponse
    {
        return response()->json([
            'data' => DB::table('items')->where('id', $id)->first(),
        ]);
    }

    public function expensive(): JsonResponse
    {
        return response()->json([
            'data' => DB::table('items')->orderByDesc('price')->first(),
        ]);
    }

    public function sum(): JsonResponse
    {
        return response()->json([
            'data' => DB::table('items')->sum('price'),
        ]);
    }

    public function recommended(): JsonResponse
    {
        return response()->json([
            'data' => DB::table('items')
                ->whereNotNull('discount_price')
                ->where('price', '>', 6)
                ->pluck('name', 'id'),
        ]);
    }

    public function event(Request $request, string $event): JsonResponse
    {
        $query = DB::table('items')->where('event', $event);

        if ($request->query('discounted') === 'yes') {
            $query->whereNotNull('discount_price');
        }

        return response()->json([
            'data' => $query->get(),
        ]);
    }
}
