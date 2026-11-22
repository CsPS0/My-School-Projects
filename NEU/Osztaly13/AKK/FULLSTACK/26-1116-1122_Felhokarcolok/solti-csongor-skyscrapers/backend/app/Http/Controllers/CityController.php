<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreCityRequest;
use App\Http\Requests\UpdateCityRequest;
use App\Http\Resources\CityResource;
use App\Models\City;

class CityController extends Controller
{
    public function index()
    {
        return CityResource::collection(City::with('skyscrapers')->get());
    }

    public function store(StoreCityRequest $request)
    {
        $data = $request->validated();
        $city = City::create($data);
        return new CityResource($city->load('skyscrapers'));
    }

    public function show(City $city)
    {
        return new CityResource($city->load('skyscrapers'));
    }

    public function update(UpdateCityRequest $request, City $city)
    {
        $data = $request->validated();
        $city->update($data);
        return new CityResource($city->load('skyscrapers'));
    }

    public function destroy(City $city)
    {
        return $city->delete() ? response()->noContent() : abort(500);
    }
}
