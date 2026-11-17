<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class OebbSeeder extends Seeder
{
    public function run(): void
    {
        $path = file_exists(base_path('../../sources/oebb.sql'))
            ? base_path('../../sources/oebb.sql')
            : base_path('../sources/oebb.sql');

        if (!file_exists($path)) {
            return;
        }

        $sql = file_get_contents($path);
        $queries = explode(";", $sql);
        foreach ($queries as $query) {
            if (str_contains($query, 'INSERT INTO')) {
                $pos = strpos($query, 'INSERT INTO');
                $insert = trim(substr($query, $pos));
                DB::unprepared($insert . ';');
            }
        }
    }
}
