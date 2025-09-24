// server/api/index.js
const express = require("express");
const app = express();
const port = 3001;
const books = [
    { id: 1, title: "Clean Code", author: "Robert C. Martin" },
    { id: 2, title: "You Don't Know JS", author: "Kyle Simpson" },
    { id: 3, title: "The Pragmatic Programmer", author: "Andy Hunt & Dave Thomas" },
];

app.get("/api/books", (req, res) => {
    res.json(books);
});

app.listen(port, () => {
    console.log(`Server running on http://localhost:${port}`);
});
