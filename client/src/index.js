const express = require('express');
const pool = require('./db');
const app = express();
const port = 3000;

app.use(express.json());


app.get('/', (req, res) => {
    res.send('API server is running!');
});


app.get('/todos', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM todos');
        res.json(result.rows);
    } catch (err) {
        console.error(err);
        res.status(500).send('Database error');
    }
});


app.get('/todos/:id', async (req, res) => {
    const { id } = req.params;
    try {
        const result = await pool.query('SELECT * FROM todos WHERE id = $1', [id]);
        if (result.rows.length === 0) return res.status(404).send('Todo not found');
        res.json(result.rows[0]);
    } catch (err) {
        console.error(err);
        res.status(500).send('Database error');
    }
});


app.post('/todos', async (req, res) => {
    const { title, completed } = req.body;
    try {
        const result = await pool.query(
            'INSERT INTO todos (title, completed) VALUES ($1, $2) RETURNING *',
            [title, completed || false]
        );
        res.status(201).json(result.rows[0]);
    } catch (err) {
        console.error(err);
        res.status(500).send('Database error');
    }
});


app.put('/todos/:id', async (req, res) => {
    const { id } = req.params;
    const { title, completed } = req.body;
    try {
        const result = await pool.query(
            'UPDATE todos SET title = $1, completed = $2 WHERE id = $3 RETURNING *',
            [title, completed, id]
        );
        if (result.rows.length === 0) return res.status(404).send('Todo not found');
        res.json(result.rows[0]);
    } catch (err) {
        console.error(err);
        res.status(500).send('Database error');
    }
});


app.delete('/todos/:id', async (req, res) => {
    const { id } = req.params;
    try {
        const result = await pool.query('DELETE FROM todos WHERE id = $1 RETURNING *', [id]);
        if (result.rows.length === 0) return res.status(404).send('Todo not found');
        res.json({ message: 'Todo deleted', todo: result.rows[0] });
    } catch (err) {
        console.error(err);
        res.status(500).send('Database error');
    }
});

app.listen(port, () => {
    console.log(`Server running at http://127.0.0.1:${port}`);
});
