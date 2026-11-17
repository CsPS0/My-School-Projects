<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class ManufacturerSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('manufacturers')->insert([
            [
                'id' => 1,
                'name' => 'SweetDelights',
                'url' => 'sweetdelights.com',
                'founded' => 1985
            ],
            [
                'id' => 2,
                'name' => 'ChocoLuxe',
                'url' => 'chocoluxe.net',
                'founded' => 1992
            ],
            [
                'id' => 3,
                'name' => 'CocoaHeaven',
                'url' => null,
                'founded' => 2001
            ],
            [
                'id' => 4,
                'name' => 'VelvetCocoa',
                'url' => 'velvetcocoa.org',
                'founded' => 1978
            ],
            [
                'id' => 5,
                'name' => 'DarkBite',
                'url' => 'darkbite.io',
                'founded' => 2010
            ]
        ]);
    }
}
