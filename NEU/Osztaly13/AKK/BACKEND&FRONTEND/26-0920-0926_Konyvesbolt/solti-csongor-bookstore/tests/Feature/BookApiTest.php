<?php

namespace Tests\Feature;

use Tests\TestCase;

class BookApiTest extends TestCase
{
    public function test_can_list_all_books()
    {
        $response = $this->getJson('/api/books');

        $response->assertStatus(200)
            ->assertJsonStructure([
                'data' => [
                    '*' => ['id', 'title', 'author', 'genre', 'price', 'year', 'pages', 'rating']
                ]
            ]);
    }

    public function test_can_filter_books_by_pages()
    {
        $response = $this->getJson('/api/books?pages_min=300&pages_max=500');

        $response->assertStatus(200);
        $data = $response->json('data');
        foreach ($data as $book) {
            $this->assertGreaterThanOrEqual(300, $book['pages']);
            $this->assertLessThanOrEqual(500, $book['pages']);
        }
    }

    public function test_can_get_single_book()
    {
        $response = $this->getJson('/api/books/1');

        $response->assertStatus(200)
            ->assertJsonPath('data.id', 1);
    }

    public function test_returns_404_for_non_existent_book()
    {
        $response = $this->getJson('/api/books/99999');

        $response->assertStatus(404);
    }

    public function test_can_get_most_expensive_book()
    {
        $response = $this->getJson('/api/books/most-expensive');

        $response->assertStatus(200)
            ->assertJsonStructure(['data' => ['id', 'title', 'price']]);
    }

    public function test_can_get_popular_books()
    {
        $response = $this->getJson('/api/books/popular');

        $response->assertStatus(200);
        $data = $response->json('data');
        foreach ($data as $book) {
            $this->assertGreaterThanOrEqual(4.5, $book['rating']);
        }
    }

    public function test_can_filter_by_genre()
    {
        $response = $this->getJson('/api/books/genre/fantasy?order=asc');

        $response->assertStatus(200);
        $data = $response->json('data');
        foreach ($data as $book) {
            $this->assertEquals('fantasy', strtolower($book['genre']));
        }
    }
}
