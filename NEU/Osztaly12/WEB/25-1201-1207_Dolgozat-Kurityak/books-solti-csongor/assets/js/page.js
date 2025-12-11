"use strict";

const booksContainer = document.getElementById("books");

const classes = {
    hard: "hard-cover",
    soft: "paperback",
    series: "series",
    adaptation: "adaptation",
}

function createBadge({type, content}) {
    const badge = document.createElement("span")
    badge.classList.add("badge", classes[type])
    badge.textContent = content
    return badge
}

function createBadgeRow(book) {
    const badgeRow = document.createElement("div")
    badgeRow.classList.add("badge-row")

    if (book.hardcover) {
        badgeRow.appendChild(createBadge({type: "hard", content: "Keménytáblás"}))
    }
    else {
        badgeRow.appendChild(createBadge({type: "soft", content: "Puhatáblás"}))
    }

    if (book.series) {
        badgeRow.append(createBadge({type: "series", content: `${book.series} ${book.sequence}. könyv`,}))
    }

    return badgeRow
}

function createBook(book) {
    const bookElement = document.createElement("div")
    bookElement.classList.add("book")

    const img = document.createElement("img")
    img.src = `./assets/img/covers/${book.cover}`
    img.alt = book.title

    const bookBody = document.createElement("div")
    bookBody.classList.add("book-body")

    const title = document.createElement("h2")
    title.textContent = book.title

    const list = document.createElement("ul")
    const author = document.createElement("li")
    author.textContent = `Szerző: ${book.author}`
    const publisher = document.createElement("li")
    publisher.textContent = `Kiadó: ${book.publisher}`
    const category = document.createElement("li")
    category.textContent = `Kategória: ${book.category}`
    list.append(author, publisher, category)

    const price = document.createElement("p");
    price.textContent = book.price.toLocaleString("hu-HU", {
        style: "currency",
        currency: "HUF",
        maximumFractionDigits: 0,
    })

    bookBody.appendChild(title);
    bookBody.appendChild(list);
    bookBody.appendChild(price);
    const badgeRow = createBadgeRow(book);
    bookBody.appendChild(badgeRow);

    bookElement.appendChild(img);
    bookElement.appendChild(bookBody);
    return bookElement;
}

function renderBooks(bookArray) {
    if (bookArray.length == 0) {
        booksContainer.textContent = "Nincs találat..."
    }
    else {
        booksContainer.textContent = ""
        for (let book of bookArray) {
            booksContainer.appendChild(createBook(book))
        }
    }
}
renderBooks(books);

function filterBooks(event) {
    event.preventDefault()

    const titleFilter = document.getElementById("filter_title").value.toLowerCase()
    const minPriceFilter = parseInt(document.getElementById("filter_min").value) || 0
    const maxPriceFilter = parseInt(document.getElementById("filter_max").value) || Infinity

    const filtered = books.filter(function(book) {
        const title = book.title.toLowerCase()
        const price = book.price
        return (title.includes(titleFilter) && price >= minPriceFilter && price <= maxPriceFilter)
    })

    renderBooks(filtered)
}

const filterForm = document.getElementById("books_filter")
filterForm.addEventListener("submit", filterBooks)

const resetButton = document.getElementById("reset_filters")
resetButton.addEventListener("click", () => {
    filterForm.reset()
    renderBooks(books)
})