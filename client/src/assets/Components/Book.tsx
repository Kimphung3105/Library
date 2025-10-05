import React, { useEffect, useState } from "react";
export class Library {
    title: string | undefined;
    id: React.Key | null | undefined;
    description: any;
    priority: any;

    type Book = {
        id: number;
        title: string;
        author: string;
    };

    const Book: React.FC = () => {
        const [books, setBooks] = useState<Book[]>([]);
        const [loading, setLoading] = useState(true);

        useEffect(() => {
            const fetchBooks = async () => {
                try {
                    const res = await fetch("http://localhost:3001/api/books");
                    const data = await res.json();
                    setBooks(data);
                } catch (error) {
                    console.error("Failed to fetch books", error);
                } finally {
                    setLoading(false);
                }
            };

            fetchBooks();
        }, []);

        if (loading) return <p>Loading books...</p>;

        return (
            <div className="p-4">
                <h2 className="text-xl font-bold mb-4">Books</h2>
                <ul className="space-y-2">
                    {books.map((book) => (
                        <li key={book.id} className="p-3 rounded-lg shadow bg-white">
                            <p className="font-semibold">{book.title}</p>
                            <p className="text-sm text-gray-600">by {book.author}</p>
                        </li>
                    ))}
                </ul>
            </div>
        );
    };

    export default Book;
}