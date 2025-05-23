# MazePuzzle SQL Practice

## Overview
The MazePuzzle is designed to help users practice SQL 'ORDER BY' and 'WHERE' clauses. Each attempt presents a randomly selected puzzle variant, requiring different query logic and preventing memorization of a single solution.

## How it Works
- On each attempt, one of several puzzle variants is randomly selected.
- The user is shown a description of the puzzle and must write a SQL query to solve it.
- The system checks the query result for correctness based on the current variant.
- Feedback is provided for correct or incorrect answers, and the number of attempts is tracked.

## Puzzle Variants
Below are the current variants (examples):

1. **Pattern 1, Order by Sequence Ascending**
   - _Description_: Select all mazes with pattern 1, ordered by sequence ascending.
   - _Example SQL_: `SELECT * FROM doolhoven WHERE patroon = 1 ORDER BY regelnummer ASC;`

2. **Pattern 2, Order by Sequence Descending**
   - _Description_: Select all mazes with pattern 2, ordered by sequence descending.
   - _Example SQL_: `SELECT * FROM doolhoven WHERE patroon = 2 ORDER BY regelnummer DESC;`

3. **Contents Contains '@', Order by Pattern Ascending**
   - _Description_: Select all mazes where 'inhoud' contains '@', ordered by pattern ascending.
   - _Example SQL_: `SELECT * FROM doolhoven WHERE inhoud LIKE '%@%' ORDER BY patroon ASC;`

4. **Sequence > 3, Order by Contents Descending**
   - _Description_: Select all mazes with sequence > 3, ordered by contents descending.
   - _Example SQL_: `SELECT * FROM doolhoven WHERE regelnummer > 3 ORDER BY inhoud DESC;`

5. **Pattern 4 and Contents Ends with '#', Order by Sequence Ascending**
   - _Description_: Select all mazes with pattern 4 and contents ending with '#', ordered by sequence ascending.
   - _Example SQL_: `SELECT * FROM doolhoven WHERE patroon = 4 AND inhoud LIKE '%#' ORDER BY regelnummer ASC;`

## Extensibility
- New variants can be added by extending the variant list in `MazeViewModel`.
- The system is designed to support future SQL topics and puzzle types.

## User Experience
- The UI clearly shows which SQL clauses are needed.
- Users receive immediate feedback and can try again if incorrect.

---
For further ideas or to propose new puzzle variants, please open an issue or discuss with the maintainers.
