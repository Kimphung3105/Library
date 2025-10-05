import React, { useEffect, useState } from "react";
import "./books.css";
import { type BookDto, LibraryClient } from "../../generated-client";

const libraryClient = new LibraryClient("http://localhost:5273/api");

const Library: React.FC = () => {
    const [books, setBooks] = useState<BookDto[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fetchBooks() {
            try {
                const data = await libraryClient.getBooks();
                console.log("Fetched books:", data);
                setBooks(data || []);
            } catch (error) {
                console.error("Error fetching books:", error);
            } finally {
                setLoading(false);
            }
        }

        fetchBooks();
    }, []);

    if (loading) {
        return <p>Loading books...</p>;
    }

    if (books.length === 0) {
        return <p>No books found in the library.</p>;
    }

    return (
        <main>
            <section className="library">
                <div className="container">
                    <h2 className="library-title">Library</h2>

                    <div className="search-box">
                        <input type="text" placeholder="Search Book..." />
                        <button className="search-btn">
                            <img src="./images/search_icon.svg" alt="search" />
                        </button>
                    </div>

                    <h2 className="library-title">Books</h2>
                    <ul className="library-list">
                        {books.map((book) => (
                            <li key={book.id} className="library-item">
                                <div className="book">
                                    <div className="book-image">
                                        <img
                                            src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSNHV0HnZFjnPVeDJnh_jxfSaaxOJaazyc6Kw&s"
                                            alt={book.title}
                                        />
                                    </div>
                                    <div className="book-info">
                                        <h3 className="book-title">{book.title}</h3>
                                        <p className="book-author">
                                            Author: {book.authorsIds?.join(", ")}
                                        </p>
                                        <strong>Pages: {book.pages}</strong>
                                    </div>
                                </div>
                            </li>
                        ))}
                    </ul>
                </div>
            </section>
        </main>
    );
};

export default Library;
