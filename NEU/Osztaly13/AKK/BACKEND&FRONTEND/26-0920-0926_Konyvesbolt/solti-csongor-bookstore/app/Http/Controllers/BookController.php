<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Collection;

class BookController extends Controller
{
    /**
     * Return books from books.txt as a Laravel Collection.
     */
    public function getBooks(): Collection
    {
        $path = storage_path('app/books.txt');
        if (!file_exists($path)) {
            $path = base_path('../forras/books.txt');
        }
        if (!file_exists($path)) {
            $path = base_path('books.txt');
        }

        $raw = file_get_contents($path);
        $data = eval('return ' . $raw . ';');

        return collect($data);
    }

    /**
     * GET /api/books
     * List all books, optionally filtered by pages_min and pages_max, sorted by price ASC.
     */
    public function index(Request $request)
    {
        $books = $this->getBooks();

        if ($request->has('pages_min')) {
            $min = (int) $request->query('pages_min');
            $books = $books->filter(fn($b) => $b['pages'] >= $min);
        }

        if ($request->has('pages_max')) {
            $max = (int) $request->query('pages_max');
            $books = $books->filter(fn($b) => $b['pages'] <= $max);
        }

        $books = $books->sortBy('price')->values();

        return response()->json([
            'data' => $books,
        ]);
    }

    /**
     * GET /api/books/most-expensive
     * Return the book with the highest price.
     */
    public function mostExpensive()
    {
        $book = $this->getBooks()->sortByDesc('price')->first();

        return response()->json([
            'data' => $book,
        ]);
    }

    /**
     * GET /api/books/random
     * Return a single random book.
     */
    public function random()
    {
        $book = $this->getBooks()->random();

        return response()->json([
            'data' => $book,
        ]);
    }

    /**
     * GET /api/books/popular
     * Return books with rating >= 4.5.
     */
    public function popular()
    {
        $books = $this->getBooks()
            ->filter(fn($b) => $b['rating'] >= 4.5)
            ->values();

        return response()->json([
            'data' => $books,
        ]);
    }

    /**
     * GET /api/books/genre/{genre}
     * Return books of a specific genre, ordered by year (asc/desc, default desc).
     */
    public function genre(Request $request, string $genre)
    {
        $order = strtolower($request->query('order', 'desc'));

        $books = $this->getBooks()->filter(function ($b) use ($genre) {
            return strtolower($b['genre']) === strtolower($genre);
        });

        if ($order === 'asc') {
            $books = $books->sortBy('year');
        } else {
            $books = $books->sortByDesc('year');
        }

        return response()->json([
            'data' => $books->values(),
        ]);
    }

    /**
     * GET /api/books/{id}
     * Return a specific book by ID, or 404 if not found.
     */
    public function show(string $id)
    {
        $numericId = (int) $id;
        $book = $this->getBooks()->firstWhere('id', $numericId);

        if (!$book) {
            return response()->json([
                'message' => 'A könyv nem található.',
            ], 404);
        }

        return response()->json([
            'data' => $book,
        ]);
    }
}
