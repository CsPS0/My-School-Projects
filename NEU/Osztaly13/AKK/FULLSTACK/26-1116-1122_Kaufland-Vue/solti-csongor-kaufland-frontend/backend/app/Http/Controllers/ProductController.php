<?php

namespace App\Http\Controllers;

use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ProductController extends Controller
{
    public function index(): JsonResponse
    {
        return response()->json([
            'data' => DB::table('products')->get(),
        ]);
    }

    public function show(int $id): JsonResponse
    {
        return response()->json([
            'data' => DB::table('products')->where('id', $id)->first(),
        ]);
    }

    public function cheaps(): JsonResponse
    {
        return response()->json([
            'data' => DB::table('products')->orderBy('price')->get(),
        ]);
    }

    public function sum(): JsonResponse
    {
        return response()->json([
            'data' => round(DB::table('products')->sum('price'), 2),
        ]);
    }

    public function recommended(): JsonResponse
    {
        return response()->json([
            'data' => DB::table('products')
                ->whereNotNull('discount')
                ->where('price', '<', 20)
                ->pluck('name', 'id'),
        ]);
    }

    public function discounted(Request $request): JsonResponse
    {
        $query = DB::table('products')->whereNotNull('discount');

        if ($request->query('category')) {
            $query->where('category', $request->query('category'));
        }

        return response()->json([
            'data' => $query->get(),
        ]);
    }
}
