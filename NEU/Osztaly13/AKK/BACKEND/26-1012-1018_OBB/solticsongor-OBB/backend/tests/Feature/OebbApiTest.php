<?php

namespace Tests\Feature;

use Illuminate\Support\Facades\DB;
use Tests\TestCase;

class OebbApiTest extends TestCase
{
    protected function setUp(): void
    {
        parent::setUp();
        // Ensure tables exist in sqlite memory for testing
        $this->artisan('migrate');
        $this->artisan('db:seed', ['--class' => 'OebbSeeder']);
    }

    public function test_hero_contents_endpoint_returns_data(): void
    {
        $response = $this->getJson('/api/hero-contents');

        $response->assertStatus(200)
            ->assertJsonStructure([
                'data' => [
                    '*' => ['id', 'image_url', 'title', 'description', 'button_text']
                ]
            ]);

        $this->assertCount(6, $response->json('data'));
    }

    public function test_teasers_endpoint_returns_all_and_filtered_by_section(): void
    {
        $allResponse = $this->getJson('/api/teasers');
        $allResponse->assertStatus(200);
        $this->assertCount(23, $allResponse->json('data'));

        $filteredResponse = $this->getJson('/api/teasers?section=new_from_obb');
        $filteredResponse->assertStatus(200);
        $data = $filteredResponse->json('data');
        $this->assertCount(5, $data);
        $orders = array_column($data, 'order');
        $sortedOrders = $orders;
        sort($sortedOrders);
        $this->assertEquals($sortedOrders, $orders);
    }

    public function test_trains_endpoint_returns_train_and_handles_404(): void
    {
        $response = $this->getJson('/api/trains/1');
        $response->assertStatus(200)
            ->assertJsonPath('data.id', 1)
            ->assertJsonPath('data.train_number', 'EN 463');

        $notFound = $this->getJson('/api/trains/99999');
        $notFound->assertStatus(404);

        $badParam = $this->getJson('/api/trains/not-a-number');
        $badParam->assertStatus(404);
    }

    public function test_timetable_origins_endpoint(): void
    {
        $response = $this->getJson('/api/timetable/stations/origins');
        $response->assertStatus(200);
        $data = $response->json('data');
        $this->assertIsArray($data);
        $this->assertContains('Wien Hbf', $data);
    }

    public function test_timetable_destinations_endpoint(): void
    {
        $response = $this->getJson('/api/timetable/stations/destinations');
        $response->assertStatus(200);
        $data = $response->json('data');
        $this->assertIsArray($data);
        $this->assertContains('Budapest-Keleti', $data);

        $filtered = $this->getJson('/api/timetable/stations/destinations?origin=Wien Hbf');
        $filtered->assertStatus(200);
        $filteredData = $filtered->json('data');
        $this->assertContains('Budapest-Keleti', $filteredData);
    }

    public function test_timetable_planner_endpoint(): void
    {
        $response = $this->getJson('/api/timetable/planner/Wien/Budapest');
        $response->assertStatus(200)
            ->assertJsonStructure([
                'data' => [
                    '*' => [
                        'id',
                        'train_number',
                        'origin',
                        'destination',
                        'departure_time',
                        'arrival_time',
                        'second_class_price',
                    ]
                ]
            ]);

        $data = $response->json('data');
        $this->assertNotEmpty($data);

        // Test with time filter (departure)
        $timeResponse = $this->getJson('/api/timetable/planner/Wien/Budapest?time=12:00:00');
        $timeResponse->assertStatus(200);
        foreach ($timeResponse->json('data') as $train) {
            $this->assertGreaterThanOrEqual('12:00:00', $train['departure_time']);
        }

        // Test with time-type=arrival
        $arrivalResponse = $this->getJson('/api/timetable/planner/Wien/Budapest?time=15:00:00&time-type=arrival');
        $arrivalResponse->assertStatus(200);
        foreach ($arrivalResponse->json('data') as $train) {
            $this->assertLessThanOrEqual('15:00:00', $train['arrival_time']);
        }
    }
}
