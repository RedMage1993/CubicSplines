# CubicSplines
I wrote a C# class to handle finding the inverse of a special symmetric tridiagonal matrix used for finding the coefficients of 3rd degree piece-wise polynomials that pass through control points to form a smooth curve. I used the method for finding the inversion of a similar matrix at https://en.wikipedia.org/wiki/Tridiagonal_matrix#Inversion and applied it here. This is useful to me for quickly generating varying curves; I can make objects in a game move along an exciting path on the screen. This was originally supposed to be a homework assignment where the inversion of the special matrix was to be hardcoded for a predetermined number of control points. I wanted it to be variable. My algorithm can be improved by taking advantage of the fact that the inversion of the matrix is symmetric (I currently calculate for all i, j).
