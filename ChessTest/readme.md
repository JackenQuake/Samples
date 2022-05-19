I believe I have done the task as required.

Solution for Knight might not be the most efficient - I used List to store possible attack cells and then removed ones occupied by obstacles. If efficiency is extremely important, a single byte is enough to store all 8 possible cells with bit masks applied to mark occupied. However, that would be much worse in readability and maintainability, and I heard that modern approach favors maintainability above extreme optimization :)  However, should optimization be necessary, it is possible.

For Rook and Bishop, however, I used slightly less obvious approach, with some coordinate transformations to make them essentially the same (as Bishop is just 45 degrees rotation of Rook). I think this actually makes code more clear than a heap of "ifs" with direct approach.

I also added some unit tests:
- some tests on standart 8x8 board (1 for Rook, 1 for Bishop and 2 for Knight, as there were none initially)
- tests for all figures on 1x1 board to make sure it does not cause any errors
- tests for all figures on very large board (10000x10000)

It should not be a problem for my code, as it is completely irrelevant to board size and linear in number of obstacles, so should be efficient enough.

And there were some errors in supplied unit tests (code was slightly different from specifications in readme.md, and it was incorrect), I fixed it according to readme.md.

Finally, I had problems running tests with netcoreapp2.1 target - it was too old for my Visual Studio. So I changed target to .Net 5.0 in project properties, and then all tests passed.
