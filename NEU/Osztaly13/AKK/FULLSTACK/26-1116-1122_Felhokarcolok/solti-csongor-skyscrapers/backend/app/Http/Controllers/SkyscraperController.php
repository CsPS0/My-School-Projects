<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreSkyscraperRequest;
use App\Http\Requests\UpdateSkyscraperRequest;
use App\Http\Resources\SkyscraperResource;
use App\Models\Skyscraper;

class SkyscraperController extends Controller
{
    public function index()
    {
        return SkyscraperResource::collection(Skyscraper::with('city')->get());
    }

    public function store(StoreSkyscraperRequest $request)
    {
        $data = $request->validated();
        $skyscraper = Skyscraper::create($data);
        return new SkyscraperResource($skyscraper->load('city'));
    }

    public function show(Skyscraper $skyscraper)
    {
        return new SkyscraperResource($skyscraper->load('city'));
    }

    public function update(UpdateSkyscraperRequest $request, Skyscraper $skyscraper)
    {
        $data = $request->validated();
        $skyscraper->update($data);
        return new SkyscraperResource($skyscraper->load('city'));
    }

    public function destroy(Skyscraper $skyscraper)
    {
        return $skyscraper->delete() ? response()->noContent() : abort(500);
    }
}
